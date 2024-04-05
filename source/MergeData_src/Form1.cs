using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static FileToData.Form1;

namespace FileToData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("dict.txt") || !File.Exists("dict2.txt"))
            {
                MessageBox.Show("请放入utf-8编码的词库文本文件 dict.txt 和 dict2.txt");
                Application.Exit();
            }
            mergeData();
            MessageBox.Show("合并完成，词库在 merge.txt 中", "合并完成");
            Application.Exit();
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

        Hashtable getHashFromFile(string fileName)
        {
            Hashtable result = new Hashtable();
            string input;
            StreamReader reader = new StreamReader(fileName, Encoding.UTF8);
            bool flag = true;
            string keyword = "";
            StringBuilder data = new StringBuilder();
            while ((input = reader.ReadLine()) != null)
            {
                input = input.Trim();
                if (input == "")
                {
                    flag = true;
                    result[keyword] = keyword;
                    data = new StringBuilder();
                    continue;
                }

                if (flag)
                {
                    keyword = input;
                    flag = false;
                }
                else
                {
                    data.Append(input + "#");
                }
            }
            result[keyword] = keyword;
            reader.Close();
            return result;
        }
        ArrayList getListFromFile(string fileName)
        {
            ArrayList list = new ArrayList();
            string input;
            StreamReader reader = new StreamReader(fileName, Encoding.UTF8);
            bool flag = true;
            string keyword = "";
            StringBuilder data = new StringBuilder();
            while ((input = reader.ReadLine()) != null)
            {
                input = input.Trim();
                if (input == "")
                {
                    flag = true;
                    if (data.ToString() != "")
                    {
                        addNode(list, keyword, data.ToString());
                    }                    
                    data = new StringBuilder();
                    continue;
                }

                if (flag)
                {
                    keyword = input;
                    flag = false;
                }
                else
                {
                    data.Append(input + "#");
                }
            }
            addNode(list, keyword, data.ToString());
            reader.Close();
            return list;
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
        private void mergeData()
        {
            Hashtable hashtable = getHashFromFile("dict.txt");
            ArrayList originList = getListFromFile("dict.txt");
            ArrayList newList = getListFromFile("dict2.txt");

            foreach (Node node in newList)
            {
                if (!hashtable.ContainsKey(node.inputKeyword))
                {
                    originList.Add(node);
                }
            }

            NodeComapre comparer = new NodeComapre();
            originList.Sort(comparer);
            StreamWriter writer = new StreamWriter(this.getApplicationPath() + "/merge.txt", false, this.getEncoding());
            for (int i = 0; i < originList.Count; i++)
            {
                Node node = (Node)originList[i];
                writer.WriteLine(node.inputKeyword);
                writer.WriteLine(node.chinese.ToString().Replace("#", "\n"));
            }
            writer.Close();
        }
    }
}
