using Dict;
using System;
using System.Collections;
using System.Text;

namespace Exercises
{
    public class DictionaryFinder : FileDictionary
    {
        public DictionaryFinder(string folder, int resultSize) : base(folder, resultSize)
        {
        }        

        public string GetDictData(string searchKeyword)
        {
            try
            {
                int resultId = 0;
                ArrayList list = getStartLikeData(searchKeyword.Trim().ToLower());
                if (list.Count > 1)
                {
                    resultId = 1;
                }

                KeyNode node = (KeyNode)(list[resultId]);
                return nodeToString(node);
            }
            catch (Exception) { }
            return "";
        }

        private string nodeToString(KeyNode node)
        {
            string[] array = null;
            int i = 0;
            array = getData(node).Replace("\r\n", "\n").Split('\n');
            StringBuilder buffer = new StringBuilder();
            for (i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    int find = array[i].IndexOf("／");
                    if (find != -1)
                    {
                        array[i] = array[i].Substring(0, find);
                    }
                    array[i] = array[i].Replace("【", " 【");
                    buffer.Append(array[i]);
                }
                else if (!array[i].StartsWith("◆") && (array[i].IndexOf("／") == -1))
                {
                    buffer.Append("\r\n" + array[i]);                   
                }
                else if (array[i].StartsWith("◆"))
                {
                    
                }
            }
            return buffer.ToString();
        }
    }
}
