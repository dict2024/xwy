using System.IO;
using System.Collections;
using System.Text;
using System;

namespace Dict
{
    public class Lesson
    {
        public ArrayList exerciseList = new ArrayList();
        public int lessonType = 0; //0:自动生成  1:课程里有4个选项
        public Hashtable localDictionary = new Hashtable();

        static Random exerciseRan = new Random();
        public static Lesson GetLesson(Stream lessonFile, string levelFileName)
        {
            Lesson lesson = new Lesson();

            exerciseRan = new Random(System.DateTime.Now.Millisecond);

            //读取课程
            StreamReader reader = new StreamReader(lessonFile, Encoding.UTF8);
            Exercise word;
            string input;
            int id = 0;
            ArrayList fileDatas = new ArrayList();
            while ((input = reader.ReadLine()) != null)
            {
                if (lesson.lessonType == 0)
                {
                    if (input.IndexOf("1)") != -1 || input.IndexOf("A)") != -1 || input.IndexOf("a)") != -1)
                    {
                        lesson.lessonType = 1;
                    }
                }
                fileDatas.Add(input);
            }
            reader.Close();
            

            try
            {
                if (lesson.lessonType == 1)
                {
                    //测试里有4个选项
                    for (int j = 0; j < fileDatas.Count; j += 4)
                    {
                        id++;
                        word = new Exercise();
                        word.id = id;
                        //读取测试
                        word.Question = (string)fileDatas[j];
                        //读取结果
                        try
                        {
                            string resultLine = (string)fileDatas[j + 2];
                            resultLine = resultLine.Replace("a", "1").Replace("b", "2").Replace("c", "3").Replace("d", "4");
                            resultLine = resultLine.Replace("A", "1").Replace("B", "2").Replace("C", "3").Replace("D", "4");
                            word.Result = Convert.ToInt32(resultLine);
                        }
                        catch (Exception)
                        {
                            word.Result = 0;
                        }
                        //读取选项
                        input = (string)fileDatas[j + 1];
                        input = input.Replace("a)", "1)").Replace("b)", "2)").Replace("c)", "3)").Replace("d)", "4)");
                        input = input.Replace("A)", "1)").Replace("B)", "2)").Replace("C)", "3)").Replace("D)", "4)");
                        input = input.Replace("a.", "1)").Replace("b.", "2)").Replace("c.", "3)").Replace("d.", "4)");
                        input = input.Replace("A.", "1)").Replace("B.", "2)").Replace("C.", "3)").Replace("D.", "4)");
                        int find1 = 0;
                        int find2 = 0;
                        for (int i = 1; i <= 4; i++)
                        {
                            find2 = input.IndexOf(i.ToString() + ")");
                            if (find2 == -1)
                            {
                                break;
                            }
                            if (i > 1)
                            {
                                word.Answers[i - 2] = input.Substring(find1 + 2, find2 - find1 - 2).Trim();
                            }
                            find1 = find2;
                        }
                        if (find1 != -1)
                        {
                            find2 = input.Length;
                            word.Answers[3] = input.Substring(find1 + 2, find2 - find1 - 2);
                        }
                        lesson.exerciseList.Add(word);
                    }
                }
                else
                {
                    ArrayList cnList = new ArrayList();
                    ArrayList foreignList = new ArrayList();
                    fillAnswerPool(fileDatas, cnList, foreignList);

                    for (int i = 0; i < fileDatas.Count; i++)
                    {
                        try
                        {
                            input = (string)fileDatas[i];
                            if (input.Trim() == "")
                            {
                                continue;
                            }
                            //取出注音作为扩展
                            string addtion = "";
                            int pfind = input.LastIndexOf("#");
                            if (pfind != -1)
                            {
                                addtion = input.Substring(pfind + 1, input.Length - pfind - 1);
                                input = input.Substring(0, pfind).Trim();
                            }

                            //取出答案
                            input = input.Replace("\t", " ");
                            int find = input.LastIndexOf(" ");
                            if (find == -1)
                            {
                                continue;
                            }
                            id++;
                            //生成一个测试
                            word = new Exercise();
                            word.id = id;
                            word.Question = input.Substring(0, find);                   
                            string correctAnswer = input.Substring(find + 1, input.Length - (find + 1));
                            //添加本地词典
                            lesson.localDictionary[word.Question] = correctAnswer;
                            lesson.localDictionary[correctAnswer] = word.Question;
                            //设置正确项
                            word.Result = exerciseRan.Next(1, 5);
                            word.Answers[word.Result - 1] = correctAnswer;
                            //生成其他选项
                            ArrayList answers = getRandomAnswer(word.Question, correctAnswer, cnList, foreignList);
                            int index = 2;
                            for (int j = 0; j < 4 && index < answers.Count; j++)
                            {
                                if (j != word.Result - 1)
                                {
                                    word.Answers[j] = (string)answers[index++];
                                }
                            }
                            word.Addtion = addtion;
                            lesson.exerciseList.Add(word);
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp.Message + " " + input);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            //加载学习记录
            if (File.Exists(levelFileName))
            {
                loadWordLevel(levelFileName, lesson.exerciseList);
            }            
            return lesson;
        }



        static bool isHiragana(char c)
        {
            int num = Convert.ToInt32(c);
            return ((num >= 0x3041) && (num <= 0x3094));
        }

        static bool isKatakana(char c)
        {
            int num = Convert.ToInt32(c);
            return (((num < 0x30a1) || (num > 0x30f6)) ? (num == 0x30fc) : true);
        }

        public static bool hasJapanese(string str)
        {
            foreach (char c in str)
            {
                if (isHiragana(c) || isKatakana(c))
                {
                    return true;
                }
            }
            return false;
        }

        static bool isSplitChar(char c) =>
            (Convert.ToInt32(c) < 0x30) || ((c == '、') || ((c == '。') || ((c == '・') || ((c == '…') || ((c == 0xff5e) || ((c == '「') || ((c == '」') || ((c == 0xff65) || ((c == 0xff08) || ((c == 0xff09) || ((c == 0xff0e) || ((c == '_') || (c == ' ')))))))))))));


        static bool isEnglish(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                num = Convert.ToInt32(str[i]);
                if (num > 0x7d0)
                {
                    return false;
                }
            }
            return true;
        }

        static bool isRussia(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                num = Convert.ToInt32(str[i]);
                if (num < 1000 || num > 1111)
                {
                    return false;
                }
            }
            return true;
        }

        static bool isForeign(string str)
        {
            if (hasJapanese(str) || isEnglish(str) || isRussia(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static ArrayList getRandomAnswer(string question, string example, ArrayList cnList, ArrayList foreignList)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add(question);
            resultList.Add(example);

            ArrayList poolList;
            if (isForeign(example))
            {
                poolList = foreignList;
            }
            else
            {
                poolList = cnList;
            }

            ArrayList similarPoolList = new ArrayList();
            foreach (string keyword in poolList)
            {
                if (keyword[keyword.Length - 1] == example[example.Length - 1])
                {
                    similarPoolList.Add(keyword);
                }
            }
            if (similarPoolList.Count < 3)
            {
                foreach (string keyword in poolList)
                {
                    if (keyword[0] == example[0])
                    {
                        similarPoolList.Add(keyword);
                    }
                }
            }

            int loopCount = 0;
            int index;
            Hashtable selectedIndex = new Hashtable();
            while (resultList.Count < 5)
            {
                string selectWord;

                if (loopCount < 20 && similarPoolList.Count > 1)
                {
                    index = exerciseRan.Next(0, similarPoolList.Count);
                    selectWord = (string)similarPoolList[index];
                    similarPoolList.RemoveAt(index);
                }
                else
                {
                    index = exerciseRan.Next(0, poolList.Count);
                    while (selectedIndex.ContainsKey(index))
                    {
                        index = exerciseRan.Next(0, poolList.Count);
                    }
                    selectWord = (string)poolList[index];
                    selectedIndex[index] = index;
                }

                loopCount++;
                if (!resultList.Contains(selectWord))
                {
                    resultList.Add(selectWord);
                }
            }
            return resultList;
        }

        static void fillAnswerPool(ArrayList fileDatas, ArrayList cnList, ArrayList foreignList)
        {
            for (int i = 0; i < fileDatas.Count; i++)
            {
                string input = (string)fileDatas[i];
                if (input.Trim() == "")
                {
                    continue;
                }
                int pfind = input.LastIndexOf("#");
                if (pfind != -1)
                {
                    input = input.Substring(0, pfind).Trim();
                }
                input = input.Replace("\t", " ");
                int find = input.LastIndexOf(" ");
                if (find == -1)
                {
                    continue;
                }
                string data1 = input.Substring(0, find);
                string data2 = input.Substring(find + 1, input.Length - (find + 1));
                if (isForeign(data1))
                {
                    foreignList.Add(data1);
                }
                else
                {
                    cnList.Add(data1);
                }
                if (isForeign(data2))
                {
                    foreignList.Add(data2);
                }
                else
                {
                    cnList.Add(data2);
                }
            }
        }

        static void loadWordLevel(string fileName, ArrayList lessons)
        {
            StreamReader reader = new StreamReader(fileName, Encoding.UTF8);
            string input = null;
            Exercise word = null;
            int i = 0;
            while ((input = reader.ReadLine()) != null)
            {
                try
                {
                    if (!input.StartsWith("*"))
                    {
                        word = (Exercise)(lessons[i++]);
                        word.Level = Convert.ToInt32(input);
                    }
                }
                catch (Exception)
                {
                }
            }
            reader.Close();
        }
    }
}
