using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileToData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string getApplicationPath() =>
            Application.StartupPath;
          
        public string getString(long l)
        {
            return l.ToString();
        }

        private Encoding getEncoding() =>
            Encoding.UTF8;
        private int dataSize = 2000;

        string fixDictData(string input)
        {
            string temp = input.Replace("\r\n", "#");
            if (temp.EndsWith("#"))
            {
                temp = temp.Substring(0, temp.Length - 1);
            }
            return temp;
        }
        public void MakeDBFile(ArrayList list, string dictionaryName)
        {
            Node node = null;
            NodeComapre comparer = new NodeComapre();
            list.Sort(comparer);

            StreamWriter writer = null;
            writer = new StreamWriter(this.getApplicationPath() + "/tmpdictionary.ini", false, this.getEncoding());
            for (int i = 0; i < list.Count; i++)
            {
                node = (Node)list[i];
                node.nodeID = i + 1;
                writer.WriteLine(node.inputKeyword + "\t" + node.pingyin + "\t" + node.wordtype + "\t" + fixDictData(node.chinese.ToString()));
            }
            writer.Close();

            ArrayList list2 = new ArrayList();
            SortNode node2 = null;
            for (int j = 0; j < list.Count; j++)
            {
                node = (Node)list[j];
                list2.Add(new SortNode(node.inputKeyword, node.nodeID));
                if (node.pingyin != null && node.pingyin != "")
                {
                    list2.Add(new SortNode(node.pingyin, node.nodeID));
                }
                
            }
            list2.Sort(new SortNodeComapre());

            writer = new StreamWriter(this.getApplicationPath() + "/tmpsort.ini", false, this.getEncoding());
                  
            for (int k = 0; k < list2.Count; k++)
            {
                node2 = (SortNode)list2[k];
                string item = node2.inputKeyword + "\t" + this.getString((long)node2.nodeID);
                writer.WriteLine(item);                
            }
            writer.Close();

            string str3 = "";
            string str4 = "";
            StreamReader reader = null;
            string[] strArray5 = new string[] { this.getApplicationPath(),  @"\", dictionaryName };
            string path = string.Concat(strArray5);

            Directory.CreateDirectory(path);
            int dataIndex = 1;
            int dataCount = 0;
            reader = new StreamReader(this.getApplicationPath() + @"\tmpdictionary.ini", this.getEncoding());
            object[] objArray = new object[] { path, @"\data", dataIndex, ".ini" };
            writer = new StreamWriter(string.Concat(objArray), false, this.getEncoding());
            while ((str3 = reader.ReadLine()) != null)
            {
                if ((dataCount != 0) && ((dataCount % dataSize) == 0))
                {
                    writer.Close();

                    dataIndex++;
                    object[] objArray2 = new object[] { path, @"\data", dataIndex, ".ini" };
                    writer = new StreamWriter(string.Concat(objArray2), false, this.getEncoding());
                }
                writer.WriteLine(str3);
                dataCount++;
            }
            reader.Close();
            writer.Close();

            int sortIndex = 1;
            int sortCount = 0;
            reader = new StreamReader(this.getApplicationPath() + "/tmpsort.ini", this.getEncoding());
            StreamWriter binaryWriter = new StreamWriter(path + @"\binary.ini", false, this.getEncoding());

            object[] objArray3 = new object[] { path, @"\sort", sortIndex, ".ini" };
            StreamWriter sortWriter = new StreamWriter(string.Concat(objArray3), false, this.getEncoding());
            while ((str3 = reader.ReadLine()) != null)
            {
                sortWriter.WriteLine(str3);
                str4 = str3;
                sortCount++;
                if ((sortCount != 0) && ((sortCount % dataSize) == 0))
                {
                    binaryWriter.WriteLine(str3.Split(new char[] { '\t' })[0]);

                    sortWriter.Close();
                    sortIndex++;
                    object[] objArray4 = new object[] { path, @"\sort", sortIndex, ".ini" };
                    sortWriter = new StreamWriter(string.Concat(objArray4), false, this.getEncoding());
                }
            }
            if (str4 != "")
            {
                binaryWriter.WriteLine(str4.Split(new char[] { '\t' })[0]);
            }
            binaryWriter.Close();
            reader.Close();
            sortWriter.Close();

            StreamWriter countWriter = new StreamWriter(path + "\\count.ini", false, this.getEncoding());
            countWriter.WriteLine(sortIndex);
            countWriter.WriteLine(dataIndex);
            countWriter.Close();

            File.Delete(this.getApplicationPath() + @"\tmpdictionary.ini");
            File.Delete(this.getApplicationPath() + @"\tmpsort.ini");
        }


        // Nested Types
        public class Node
        {
            // Fields
            public StringBuilder chinese;
            public string id;
            public string inputKeyword;
            public string pingyin;
            public char kind;
            public int nodeID;
            public string wordtype;

            // Methods
            public Node()
            {
                this.id = "";
                this.inputKeyword = "";
                this.pingyin = "";
                this.wordtype = "";
                this.chinese = new StringBuilder();
            }

            public Node(string inputKeyword, string pingyin, string wordtype, string chinese)
            {
                this.id = "";
                this.inputKeyword = "";
                this.pingyin = "";
                this.wordtype = "";
                this.chinese = new StringBuilder();
                this.inputKeyword = inputKeyword;
                this.pingyin = pingyin;
                this.wordtype = wordtype;
                this.chinese = new StringBuilder(chinese);
            }

            public void clear()
            {
                this.nodeID = 0;
                this.id = "";
                this.inputKeyword = "";
                this.pingyin = "";
                this.kind = 'c';
                this.wordtype = "";
                this.chinese = new StringBuilder();
            }

            public static int compare(string str1, string str2)
            {
                int num = 0;
                while ((num < str1.Length) && (num < str2.Length))
                {
                    int num2 = Convert.ToInt32(str1[num]);
                    int num3 = Convert.ToInt32(str2[num]);
                    if (num2 > num3)
                    {
                        return 1;
                    }
                    if (num2 < num3)
                    {
                        return -1;
                    }
                    num++;
                }
                return (((num != str1.Length) || (num != str2.Length)) ? ((num >= str1.Length) ? -1 : 1) : 0);
            }
        }

        public class NodeComapre : IComparer
        {
            // Methods
            int IComparer.Compare(object x, object y)
            {
                Form1.Node node1 = (Form1.Node)x;
                Form1.Node node2 = (Form1.Node)y;
                int num = Form1.Node.compare(node1.inputKeyword, node2.inputKeyword);
                return ((num == 0) ? Form1.Node.compare(node1.pingyin, node2.pingyin) : num);
            }
        }

        public class SortNode
        {
            // Fields
            public string id;
            public string inputKeyword;
            public int nodeID;


            public SortNode(string inputKeyword, int nodeID)
            {
                this.id = "";
                this.inputKeyword = inputKeyword;
                this.nodeID = nodeID;
            }

            public static int compare(string str1, string str2)
            {
                int num = 0;
                while ((num < str1.Length) && (num < str2.Length))
                {
                    int num2 = Convert.ToInt32(str1[num]);
                    int num3 = Convert.ToInt32(str2[num]);
                    if (num2 > num3)
                    {
                        return 1;
                    }
                    if (num2 < num3)
                    {
                        return -1;
                    }
                    num++;
                }
                return (((num != str1.Length) || (num != str2.Length)) ? ((num >= str1.Length) ? -1 : 1) : 0);
            }

            public override string ToString() =>
                this.inputKeyword;
        }

        public class SortNodeComapre : IComparer
        {
            // Methods
            int IComparer.Compare(object x, object y)
            {
                Form1.SortNode node2 = (Form1.SortNode)y;
                return Form1.Node.compare(((Form1.SortNode)x).inputKeyword, node2.inputKeyword);
            }
        }

        private void fileToData()
        {
            ArrayList list = new ArrayList();
            string input;
            StreamReader reader = new StreamReader("dict.txt", Encoding.UTF8);
            bool flag = true;
            string keyword = "";
            StringBuilder data = new StringBuilder();
            while ((input = reader.ReadLine()) != null)
            {
                if (input.Trim() == "")
                {
                    flag = true;
                    addNode(list, keyword, data.ToString());
                    data = new StringBuilder();
                    continue;
                }

                if (flag)
                {
                    keyword = input.Trim();
                    flag = false;
                }
                else
                {
                    data.Append(input + "#");
                }
            }
            addNode(list, keyword, data.ToString());
            reader.Close();

            Console.WriteLine(list.Count);

            MakeDBFile(list, "data");
        }

        void addNode(ArrayList list, string keyword, string data)
        {
            if (keyword == "" || data == "")
            {
                return;
            }
            string pinyin = "";
            int find = keyword.IndexOf("\t");
            if (find != -1)
            {
                pinyin = keyword.Substring(find + 1, keyword.Length - (find + 1));
                keyword = keyword.Substring(0, find);
            }
            list.Add(new Node(keyword, pinyin, "", data));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("dict.txt"))
            {
                MessageBox.Show("请放入utf-8编码的词库文本文件 dict.txt");
                Application.Exit();
            }
            fileToData();
            MessageBox.Show("转换完成，词库在 data 文件夹中", "转换完成");
            Application.Exit();
        }
    }
}
