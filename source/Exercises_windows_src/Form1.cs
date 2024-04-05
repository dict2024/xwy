using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Exercises
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private RadioButton[] radSelect = new RadioButton[4];
        private int CurrentPlace = 0;
        private string currentClassFolder = "";
        private string currentClassName = "";
        DictionaryFinder finder = null;
        private Lesson CurrentLesson = new Lesson();
        private ArrayList FilterLesson = new ArrayList();
        private Timer timer = new Timer();
        //系统初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化背单词
            this.radSelect[0] = radSelect1;
            this.radSelect[1] = radSelect2;
            this.radSelect[2] = radSelect3;
            this.radSelect[3] = radSelect4;

            timer.Interval = 2000;
            timer.Enabled = false;
            timer.Tick += new EventHandler(timer_Tick);
            this.LoadAllLesson();
        }

        private string getExercisePath()
        {
            return Application.StartupPath + "\\exercises\\";
        }

        private string getRecordPath()
        {
            return Application.StartupPath + "\\records\\";
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            PlaceForm form = new PlaceForm();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    int place = Convert.ToInt32(form.textBox1.Text) - 1;
                    if (place >= this.FilterLesson.Count)
                    {
                        place = this.FilterLesson.Count - 1;
                    }
                    place--;
                    if (place < 0)
                        place = this.FilterLesson.Count;
                    this.CurrentPlace = place;
                    btnNext_Click(null, null);
                }
                catch (Exception)
                {
                }
            }
        }

        #region Control Event


        private void levelBar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Filter(Convert.ToInt32(this.levelBar.SelectedItem.ToString()));
        }

        private void btnPrew_Click(object sender, EventArgs e)
        {
            this.UpdateWordLevel();
            this.SetWord(this.GetPrewWord());
            SetCurrentPlaceMessage();
        }


        public void btnNext_Click(object sender, EventArgs e)
        {
            if (this.radStudy.Checked)
            {
                timer_Tick(null, null);
            }
            else
            {
                this.btnResult_Click(null, null);
                timer.Enabled = true;
            }

        }

        private bool isEndWith(string text)
        {
            for (int i = 0; i < 4; i++)
            {
                String keyword = radSelect[i].Text.Trim();
                if (!keyword.EndsWith(text))
                {
                    return false;
                }
            }
            return true;
        }

        private bool isEndWith(string text, int length)
        {
            for (int i = 0; i < 4; i++)
            {
                String keyword = radSelect[i].Text.Trim();
                if (!keyword.EndsWith(text))
                {
                    return false;
                }
                if (keyword.Length < length)
                {
                    return false;
                }
            }
            return true;
        }

        private int getChangeType()
        {
            int wordType = 0;
            if (isEndWith("だ") || isEndWith("た") || isEndWith("て") || isEndWith("で"))
            {
                wordType = 1;
            }
            else if (isEndWith("く"))
            {
                //形容词
                wordType = 2;
            }
            else if (isEndWith("ない"))
            {
                //否定
                wordType = 3;
            }
            else if (isEndWith("に", 4))
            {
                wordType = 4;
            }
            return wordType;
        }

        string changeWordByType(string keyword, int wordType)
        {
            if (wordType == 1)
            {
                if (keyword.Length > 3)
                {
                    keyword = keyword.Substring(0, keyword.Length - 1);
                    if (keyword.EndsWith("っ"))
                    {
                        keyword = keyword.Substring(0, keyword.Length - 1) + "う";
                    }
                    else if (keyword.EndsWith("い"))
                    {
                        keyword = keyword.Substring(0, keyword.Length - 1) + "く";
                    }
                    else if (keyword.EndsWith("ん"))
                    {
                        keyword = keyword.Substring(0, keyword.Length - 1) + "む";
                    }
                    else
                    {
                        keyword += "る";
                    }
                }
            }
            else if (wordType == 2)
            {
                keyword = keyword.Substring(0, keyword.Length - 1) + "い";
            }
            else if (wordType == 3)
            {
                //否定变化
                if (keyword.Length > 2)
                {
                    char c = keyword[keyword.Length - 3];
                    if (c == 'わ')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "う";
                    }
                    else if (c == 'ら')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "る";
                    }
                    else if (c == 'ま')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "む";
                    }
                    else if (c == 'は')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "ふ";
                    }
                    else if (c == 'な')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "ぬ";
                    }
                    else if (c == 'さ')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "す";
                    }
                    else if (c == 'か')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "く";
                    }
                    else if (c == 'あ')
                    {
                        keyword = keyword.Substring(0, keyword.Length - 3) + "う";
                    }
                    else
                    {
                        keyword = keyword.Substring(0, keyword.Length - 2);
                    }
                }
            }
            else if (wordType == 4)
            {
                keyword = keyword.Substring(0, keyword.Length - 1);
            }
            return keyword;
        }

        public void btnResult_Click(object sender, EventArgs e)
        {
            int correctId = GetCorrectID();
            if (correctId != -1)
            {
                radSelect[correctId].ForeColor = System.Drawing.Color.Red;
            }
            if (!chkShowChinese.Checked)
            {
                return;
            }

            try
            {
                StringBuilder buffer = new StringBuilder();
                int wordType = getChangeType();

                //根据不同的后缀查单词
                for (int i = 0; i < 4; i++)
                {
                    String keyword = radSelect[i].Text.Trim();

                    String str;
                    if(CurrentLesson.lessonType == 1)
                    {
                        //查外部字典
                        if (currentClassFolder.StartsWith("jp"))
                        {
                            keyword = changeWordByType(keyword, wordType);
                            str = finder.GetDictData(keyword).Replace("\r\n", " ").Replace("】", "】 ");
                        }
                        else
                        {
                            str = finder.GetDictData(keyword).Replace("\r\n", " ").Replace("】", "】 ");
                        }
                    } 
                    else
                    {
                        //查本地字典
                        string localResult = (string)CurrentLesson.localDictionary[keyword];
                        str = keyword + " " + localResult;
                        if (i == correctId)
                        {
                            string question = GetCurrentWord().Question;
                            string dictStr = finder.GetDictData(question).Replace("\r\n", " ").Replace("】", "】 ");
                            if (dictStr.StartsWith(keyword))
                            {
                                dictStr = dictStr.Substring(keyword.Length, dictStr.Length - keyword.Length).Trim();
                            }
                            if (dictStr.StartsWith(localResult))
                            {
                                dictStr = dictStr.Substring(localResult.Length, dictStr.Length - localResult.Length).Trim();
                            }
                            str = str + " " + dictStr;
                        }
                    }

                    if (str.Length > 65)
                    {
                        str = str.Substring(0, 60) + "...";
                    }
                    buffer.Append(str + "\r\n");
                }
                this.txtDictValue.Text = buffer.ToString();
            }
            catch (Exception)
            {
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!this.radStudy.Checked) { 
                this.UpdateWordLevel();
            }
            this.SetWord(this.GetNextWord());
            SetCurrentPlaceMessage();
            timer.Enabled = false;
        }

        void chkShowChinese_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkShowChinese.Checked)
            {                
                this.txtDictValue.Text = "";                
                this.txtDictValue.Visible = true;
            }
            else
            {
                this.txtDictValue.Visible = false;
            }
        }

        private int GetSelectID()
        {
            for (int i = 0; i < 4; i++)
            {
                if (radSelect[i].Checked)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetCorrectID()
        {
            Exercise currentWord = this.GetCurrentWord();
            if (currentWord != null)
            {
                int index = currentWord.Result - 1;
                if (index < 0 || index > 3)
                    index = -1;
                return index;
            }
            return -1;
        }

        private void SetNormalColor()
        {
            for (int i = 0; i < 4; i++)
            {
                radSelect[i].ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtLevel_TextChanged(object sender, EventArgs e)
        {
            Exercise currentWord = this.GetCurrentWord();
            if ((currentWord != null) && (this.txtLevel.Text != ""))
            {
                try
                {
                    currentWord.Level = Convert.ToInt32(this.txtLevel.Text);
                }
                catch (Exception)
                {
                }
            }
        }

        private void radio_Click(object sender, EventArgs e)
        {
            this.SetWord(this.GetCurrentWord());
        }
        #endregion



        private void LoadAllLesson()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(getExercisePath());
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                return;
            }
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                try
                {
                    LoadSubLesson(dir.Name);
                }
                catch (Exception)
                {
                }
            }
            //lstClassName.ExpandAll();
        }

        private void LoadSubLesson(string folderName)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(getExercisePath() + folderName);
                FileInfo[] info = dirInfo.GetFiles();
                foreach (FileInfo file in info)
                {
                    try
                    {
                        if (file.Extension.ToString().ToLower() == ".txt")
                        {
                            ClassNode node = new ClassNode();
                            node.folder = folderName;
                            node.fileName = file.Name.Substring(0, file.Name.Length - 4);
                            cmbClassName.Items.Add(node);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void lstClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentClassName != "")
                    SaveWordLevel();
                ClassNode node = (ClassNode)cmbClassName.SelectedItem;
                currentClassFolder = node.folder;
                currentClassName = node.fileName;
                if (currentClassFolder.StartsWith("jp"))
                {
                    finder = new DictionaryFinder(Application.StartupPath + "\\dicts\\jp-cn\\data", 50);
                }
                else if (currentClassFolder.StartsWith("ra"))
                {
                    finder = new DictionaryFinder(Application.StartupPath + "\\dicts\\ra-cn\\data", 50);
                }
                else
                {
                    finder = new DictionaryFinder(Application.StartupPath + "\\dicts\\en-cn\\data", 50);
                }
                LoadLesson();
                SetWord(GetCurrentWord());
            }
            catch (Exception)
            {
            }

            this.btnPreview.Visible = true;
            this.btnNext.Visible = true;
            this.btnResult.Visible = true;
            this.txtDictValue.Visible = true;
            this.chkShowChinese.Visible = true;
            this.btnGoto.Visible = true;
            this.btnWeb.Visible = true;
            this.radSelect1.Focus();
        }

        private void LoadLesson()
        {
            string lessonFileName = getExercisePath() +
                currentClassFolder + "\\" + currentClassName + ".txt";

            string levelFileName = getRecordPath() +
                currentClassFolder + "\\" + currentClassName + ".level";

            Lesson lesson = Lesson.GetLesson(lessonFileName, levelFileName);
            if (lesson == null)
            {
                MessageBox.Show("加载课程失败");
                return;
            }
            this.CurrentLesson = lesson;

            this.FilterLesson.Clear();

            foreach (Exercise item in lesson.exerciseList)
            {
                this.FilterLesson.Add(item);
            }
            this.levelBar.SelectedIndex = 0;
            this.CurrentPlace = 0;
            SetCurrentPlaceMessage();

            SetCurrentPlaceMessage();
            this.radSelect[0].Checked = true;
        }



        private void SaveWordLevel()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(getRecordPath() + currentClassFolder);
            if (!dirInfo.Exists)
                dirInfo.Create();

            string fileName = getRecordPath() +
                currentClassFolder + "\\" + currentClassName + ".level";
            StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
            Exercise word = null;
            for (int i = 0; i < CurrentLesson.exerciseList.Count; i++)
            {
                word = (Exercise)(CurrentLesson.exerciseList[i]);
                writer.WriteLine(word.Level);
            }
            writer.Close();

        }

        private void Filter(int Level)
        {
            this.FilterLesson.Clear();
            foreach (Exercise word in this.CurrentLesson.exerciseList)
            {
                if (word.Level >= Level)
                {
                    this.FilterLesson.Add(word);
                }
            }
            this.CurrentPlace = 0;
            SetCurrentPlaceMessage();
            if (FilterLesson.Count != 0)
            {
                this.SetWord(this.GetCurrentWord());
            }
            else
            {
                MessageBox.Show("该课程学习结束，请选择其他课程", "提示");
            }
        }


        private Exercise GetCurrentWord()
        {
            if (this.FilterLesson.Count == 0)
            {
                return null;
            }
            try
            {
                return (Exercise)this.FilterLesson[this.CurrentPlace];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Exercise GetNextWord()
        {
            if (this.FilterLesson.Count == 0)
            {
                return null;
            }
            this.CurrentPlace++;
            if (this.CurrentPlace >= this.FilterLesson.Count)
            {
                if (!this.radStudy.Checked) 
                { 
                    if (this.levelBar.SelectedIndex < this.levelBar.Items.Count - 1) 
                    { 
                        this.levelBar.SelectedIndex++;
                    }
                }
                this.CurrentPlace = 0;
            }
            if (this.CurrentPlace % 20 == 0)
                if (!this.radStudy.Checked)
                    SaveWordLevel();
            try
            {
                return (Exercise)this.FilterLesson[this.CurrentPlace];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Exercise GetPrewWord()
        {
            if (this.FilterLesson.Count == 0)
            {
                return null;
            }
            this.CurrentPlace--;
            if (this.CurrentPlace < 0)
            {
                this.CurrentPlace = this.FilterLesson.Count - 1;
            }
            try
            {
                return (Exercise)this.FilterLesson[this.CurrentPlace];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SetCurrentPlaceMessage()
        {
            string className = this.currentClassName;
            if (className.Length > 8)
                className = className.Substring(0, 8) + "..";
            if (FilterLesson.Count > 0)
            {
                this.labMessage1.Text = className + ": " + (CurrentPlace + 1) + "/" + FilterLesson.Count;
            }
            else
            {
                this.labMessage1.Text = className + ": 0/0";
            }
        }

        private void SetWord(Exercise word)
        {
            SetNormalColor();
            ArrayList list = null;
            if (word != null)
            {
                this.txtLevel.Text = word.Level.ToString();
               
                this.labSelect.Text = word.Question;
                for (int i = 0; i < 4; i++)
                {
                    radSelect[i].Visible = true;
                    radSelect[i].Text = word.Answers[i];
                }
                this.txtDictValue.Text = "";                             
            }
            else
            {
                this.labSelect.Text = "";
                this.txtDictValue.Text = "";
                for (int i = 0; i < 4; i++)
                {
                    radSelect[i].Visible = false;
                }
            }
        }

        private void UpdateWordLevel()
        {
            Exercise currentWord = this.GetCurrentWord();
            if ((currentWord != null))
            {
                int correctID = GetCorrectID();
                if (correctID != -1)
                {
                    if (correctID != GetSelectID())
                    {
                        currentWord.Level++;
                    }
                }
            }
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            Exercise currentWord = this.GetCurrentWord();
            if (CurrentLesson.lessonType == 0 && currentWord != null)
            {
                string weburi = getWebUri(currentWord.Question);
                System.Diagnostics.Process.Start(weburi);
            }
        }

        string getWebUri(string input)
        {
            string target = "https://translate.yandex.com/?source_lang=en&target_lang=zh&text=";

            if (currentClassFolder.StartsWith("ra"))
            {
                target = "https://translate.yandex.com/?source_lang=ru&target_lang=zh&text=";
            }
            else if (currentClassFolder.StartsWith("jp"))
            {
                target = "https://translate.yandex.com/?source_lang=ja&target_lang=zh&text=";
            }
            else
            {
                target = "https://translate.yandex.com/?source_lang=en&target_lang=zh&text=";
            }

            return target + input;
        }
    }
}