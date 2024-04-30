using System;
using System.IO;
using System.Text;
using System.Collections;
namespace Dict
{    
	public class FileDictionary 
	{
		// Fields
		private ArrayList keyList = new ArrayList();
		private int sortFileSize;
		private int dataFileSize;
		private string folder;
		private int resultSize = 20;
        private int dataSize = 2000;

		// Methods
		public FileDictionary(string folder, int resultSize)
		{
			if (!File.Exists(folder + "/binary.ini"))
			{
				return;
			}

			this.folder = folder;
			this.resultSize = resultSize;		
			
            fillKeyListAndSize();

        }

        void fillKeyListAndSize()
        {
            StreamReader reader = new StreamReader(folder + "/binary.ini", System.Text.Encoding.UTF8);
            string input;
            while ((input = reader.ReadLine()) != null)
            {
                if (input != "")
                    keyList.Add(input);
            }
            reader.Close();

            reader = new StreamReader(folder + "/count.ini", System.Text.Encoding.UTF8);
            sortFileSize = Convert.ToInt32(reader.ReadLine());
            dataFileSize = Convert.ToInt32(reader.ReadLine());
            reader.Close();
        }

        private int binarySearch(string keyword)
        {
            int i = 0;
            int j = keyList.Count;
            int k = 0;
            int m = 0;
            while (i < j)
            {
                k = i + (j - i) / 2;
                m = KeyNode.compare((string)keyList[k], keyword);
                if (m == 0)
                {
                    i = k;
                    break;
                }
                else if (m < 0)
                {
                    i = k + 1;
                }
                else
                {
                    j = k;
                }
            }
            return i + 1;
        }

        private static string replaceReturnChar(string str) =>
			str.Replace(@"\n", "\r\n").Replace("#", "\r\n");


        public string getData(int resultID)
		{
			return formatData(getFileDataByID(resultID));
		}

		public string getData(KeyNode node)
		{
			int resultID = node.count;
			resultID--;
			int fileID = resultID / dataSize + 1;
			int lineID = resultID % dataSize + 1;
			int count = 0;
			string input;
			string[] array;
			StreamReader reader = new StreamReader(node.dictionary + "/data" + fileID + ".ini", Encoding.UTF8);
			while ((input = reader.ReadLine()) != null)
			{
				count++;
				if (count == lineID)
				{
					return formatData(input);
				}
			}
			return "";
		}

        private string formatArray(string[] array)
        {
            string input;
            if (array[0].Trim() == "")
            {
                input = array[1];
            }
            else
            {
                input = array[0];
                if (array[1].Trim() != "")
                    input = input + "【" + array[1] + "】";
            }
            return input;
        }
        private string formatData(string data)
        {
            string[] array = data.Split('\t');
            string input = formatArray(array);
            input = input + "\r\n" + replaceReturnChar(array[3]);
            if (array.Length > 4)
            {
                for (int i = 4; i < array.Length; i++)
                {
                    input = input + "\t" + replaceReturnChar(array[i]);
                }
            }
            return input;
        }

        private KeyNode getPreviewKeyNode(string keyword)
        {
            int resultID = 0;
            int id = this.binarySearch(keyword);
            if (id > this.sortFileSize)
            {
                return null;
            }
            string[] strArray = null;
            string key = "";
            string str2 = "";
            StreamReader reader = new StreamReader(this.getSortFileName(id), Encoding.UTF8);
            while (((str2 = reader.ReadLine()) != null) && (KeyNode.compare(str2, keyword) < 0))
            {
                key = str2;
            }
            reader.Close();

            if (key == "")
            {
                id--;
                if (id <= 0)
                {
                    return null;
                }
                reader = new StreamReader(this.getSortFileName(id), Encoding.UTF8);
                while (true)
                {
                    str2 = reader.ReadLine();
                    if (str2 == null)
                    {
                        reader.Close();
                        break;
                    }
                    key = str2;
                }
            }
            strArray = key.Split(new char[] { '\t' });
            resultID = this.getID(strArray[strArray.Length - 1]);
            str2 = this.getFileDataByID(resultID);
            if (str2 == "")
            {
                return null;
            }
            strArray = str2.Split(new char[] { '\t' });
            str2 = formatArray(strArray);
            key = str2;
            return new KeyNode(key, str2 + "\r\n" + replaceReturnChar(strArray[3]), resultID, this.folder);
        }

        public ArrayList getStartLikeData(string searchKeyword)
        {
            string str = "";
            string prewKeyword = "";
            int num = 0;
            int num2 = 0;

            int prewId = 0;
            
            //用keyword获取sort文件
            int id = this.binarySearch(searchKeyword);
            if (id > this.sortFileSize)
            {
                return new ArrayList();
            }

            //读取sort文件获取list
            ArrayList idlist = new ArrayList();
            ArrayList tempIDList = new ArrayList();
            string[] strArray;
            bool flag = false;
            StreamReader reader = new StreamReader(this.getSortFileName(id), Encoding.UTF8);
            while ((str = reader.ReadLine()) != null)
            {
                if (flag)
                {
                    strArray = str.Split(new char[] { '\t' });
                    num2 = this.getID(strArray[strArray.Length - 1]);
                    idlist.Add(num2);
                    tempIDList.Add(num2);
                }
                else if (KeyNode.compare(str, searchKeyword) < 0)
                {
                    prewKeyword = str;
                }
                else
                {
                    if (prewKeyword != "")
                    {
                        strArray = prewKeyword.Split(new char[] { '\t' });
                        prewId = this.getID(strArray[strArray.Length - 1]);
                        idlist.Add(prewId);
                        tempIDList.Add(prewId);
                    }

                    strArray = str.Split(new char[] { '\t' });
                    int currentId = this.getID(strArray[strArray.Length - 1]);
                    idlist.Add(currentId);
                    tempIDList.Add(currentId);
                    flag = true;
                }
                if (idlist.Count > this.resultSize)
                {
                    break;
                }
            }        
            reader.Close();

            //一个sort文件不够，就读写下一个sort文件
            if ((idlist.Count < this.resultSize) && (id < this.sortFileSize))
            {
                reader = new StreamReader(this.getSortFileName(++id), Encoding.UTF8);
                while (true)
                {
                    str = reader.ReadLine();
                    if (str != null)
                    {
                        char[] separator = new char[] { '\t' };
                        strArray = str.Split(separator);
                        num2 = this.getID(strArray[strArray.Length - 1]);
                        idlist.Add(num2);
                        tempIDList.Add(num2);
                        if (idlist.Count < this.resultSize)
                        {
                            continue;
                        }
                    }
                    reader.Close();
                    break;
                }
            }

            //生成结果
            ArrayList resultList = new ArrayList();
            Hashtable hashtable = this.getSortListTable(tempIDList);
            if (prewId == 0)
            {
                //不存在前一个node，需要专门找一次
                KeyNode node = this.getPreviewKeyNode(searchKeyword);
                if (node != null)
                {
                    resultList.Add(node);
                }
            }

            for (num = 0; num < idlist.Count; num++)
            {
                if (hashtable[idlist[num]] != null)
                {
                    resultList.Add(new KeyNode(this.folder, (int)idlist[num], hashtable[idlist[num]].ToString()));
                }
            }
            return resultList;
            
        }

        private string getDataFileName(int id)
		{
			return this.folder + "/data" + id + ".ini";
		}

		private string getFileDataByID(int resultID)
        {
            resultID--;
            int fileID = resultID / dataSize + 1;
            int lineID = resultID % dataSize + 1;
			if (fileID > dataFileSize)
				return "";
            int count = 0;
            string input = "";
            StreamReader reader = new StreamReader(getDataFileName(fileID), Encoding.UTF8);
            while ((input = reader.ReadLine()) != null)
            {
                count++;
                if (count == lineID)
                {
                    return input;
                }
            }
            return "";
        }


		private int getID(string str)
        {
            return Convert.ToInt32(str);
        }

        private string getSortFileName(int id)
		{
			return this.folder + "/sort" + id + ".ini";
		}

		private Hashtable getSortListTable(ArrayList tempIDList)
		{
			int key = 0;
			int lineNum = 0;
			int id = 0;
			int lastFileId = 0;
			int keyLineNum = 0;
			string str = "";
			string[] strArray = null;
			StreamReader reader = null;
			tempIDList.Sort();
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < tempIDList.Count; i++)
			{
				key = (int) tempIDList[i];
				id = ((key - 1) / 0x7d0) + 1;
                keyLineNum = ((key - 1) % 0x7d0) + 1;
				if (id == lastFileId)
				{
					while ((str = reader.ReadLine()) != null)
					{
                        lineNum++;
						if (lineNum == keyLineNum)
						{
							strArray = str.Split(new char[] { '\t' });
                            hashtable.Add(key, formatArray(strArray));
                            break;
						}
					}
				}
				else
				{
					if (reader != null)
					{
						reader.Close();
					}
                    lineNum = 0;
					reader = new StreamReader(this.getDataFileName(id), Encoding.UTF8);
					while ((str = reader.ReadLine()) != null)
					{
                        lineNum++;
						if (lineNum == keyLineNum)
						{
							strArray = str.Split(new char[] { '\t' });
                            hashtable.Add(key, formatArray(strArray));
							break;
						}
					}
				}
                lastFileId = id;
			}
			if (reader != null)
			{
				reader.Close();
			}
			return hashtable;
		}
        		
		public ArrayList getFuzzLikeData(string searchKeyword)
		{
			ArrayList list = new ArrayList();
			ArrayList list2 = new ArrayList();
			ArrayList tempIDList = new ArrayList();

            for(int i = 1; i <= sortFileSize; i++)
            {
                StreamReader reader = new StreamReader(getSortFileName(i), Encoding.UTF8);
                string str;
				while ((str = reader.ReadLine()) != null)
				{
                    string[] strArray = str.Split(new char[] { '\t' });
					if ((strArray.Length == 2) && (strArray[0].IndexOf(searchKeyword) != -1))
					{
						list2.Add(this.getID(strArray[1]));
						tempIDList.Add(this.getID(strArray[1]));
					}
				}
                reader.Close();
            }
			Hashtable hashtable = this.getSortListTable(tempIDList);
			try
			{
				for (int i = 0; i < list2.Count; i++)
				{
					object obj2 = hashtable[list2[i]];
					if (obj2 != null)
					{
						list.Add(new KeyNode(this.folder, (int) list2[i], obj2.ToString()));
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}
    }



}