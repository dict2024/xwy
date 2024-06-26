using System;

namespace Dict
{
	public class KeyNode
	{
		public string data = "";
		public string key = "";
		public int count = 0;
        public string dictionary = "";
		public KeyNode()
		{
		}
		public KeyNode(string key,int count)
		{
			this.count = count;
			this.key = key;
		}
		public KeyNode(string key,string data,int count)
		{
			this.count = count;
			this.key = key;
			this.data = data;
		}
        public KeyNode(string dictionary, int count,string key)
        {
            this.count = count;
            this.key = key;
            this.dictionary = dictionary;
        }
        public KeyNode(string key, string data, int count, string dictionary)
        {
            this.count = count;
            this.key = key;
            this.data = data;
            this.dictionary = dictionary;
        }
		public override string ToString()
		{
			return this.key;
		}

		public static int compare(string str1,string str2)
		{
			int i1,i2, i;
			for(i=0;i<str1.Length && i<str2.Length;i++)
			{
				i1=Convert.ToInt32(str1[i]);
				i2=Convert.ToInt32(str2[i]);
				if(i1>i2)
					return 1;
				else if(i1<i2)
					return -1;
			}
			if(i==str1.Length && i==str2.Length)
				return 0;
			else if(i<str1.Length)
				return 1;
			else
				return -1;
		}

	}

}
