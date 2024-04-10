using Android.Content;
using System.Text;
using System.Collections;
using Android.Content.Res;
using Android.Graphics;

namespace Dict
{
    [Activity(Label = "俄日英汉", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int currentMode = 0;

        //字典模式
        FileDictionary fileDictionary = null;
        string currentType = "";
        EditText searchText;
		EditText dictResultText;
		ListView wordList;
		Spinner  dictSpinner;

        //测试模式
        int currentPlace = 0;
        int currentLevel = 0;
        string currentClassFolder = "";
        string currentClassName = "";
        DictionaryFinder finder = null;
        Lesson CurrentLesson = new Lesson();
        ArrayList FilterLesson = new ArrayList();
        TextView labQuestion;
        EditText execResultText;
        Spinner levelSpinner;
        Spinner lessonSpinner;
        TextView labMessage;
        CheckBox chkTestMode;
        RadioButton[] radSelect = new RadioButton[4];
		
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            InitializeComponent();
            TabHost1_Click(null, null);
        }

       private void InitializeComponent()
       {
            // 模式选择
            var tabHost1 = FindViewById<TitleButton>(Resource.Id.tabHost1);
            if (tabHost1 != null)
            {
                tabHost1.Click += TabHost1_Click;
            }

            // 测试模式
            chkTestMode = FindViewById<CheckBox>(Resource.Id.test_mode);

            labQuestion = FindViewById<TextView>(Resource.Id.question);
            if (labQuestion != null)
            {
                labQuestion.SetTextColor(Color.Black);
            }
            execResultText = FindViewById<EditText>(Resource.Id.exec_text);
            labMessage = FindViewById<TextView>(Resource.Id.word_count);           

            lessonSpinner = FindViewById<Spinner>(Resource.Id.lesson_spinner);
            if (lessonSpinner != null)
            {
                String[] allLesson = LoadAllLesson();
                lessonSpinner.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, allLesson);
                lessonSpinner.ItemSelected += lstClassName_SelectedIndexChanged;
                lessonSpinner.SetSelection(loadLessonIndex());
            }

            levelSpinner = FindViewById<Spinner>(Resource.Id.level_spinner);
            if (levelSpinner != null)
            {
                levelSpinner.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem,
                    new string[] { "0", "1", "2", "3", "4", "5"});
                levelSpinner.ItemSelected += levelBar_SelectedIndexChanged;
            }

            var beforeButton = FindViewById<Button>(Resource.Id.before);
            if (beforeButton != null)
            {
                beforeButton.Click += btnPrew_Click;
            }

            var nextButton = FindViewById<Button>(Resource.Id.next);
            if (nextButton != null)
            {
                nextButton.Click += btnNext_Click;
            }

            var answerButton = FindViewById<Button>(Resource.Id.show_answer);
            if (answerButton != null)
            {
                answerButton.Click += btnResult_Click;
            }

            var webButton = FindViewById<Button>(Resource.Id.web);
            if (webButton != null)
            {
                webButton.Click += ExerciseWeb_Click;
            }
            radSelect[0] = FindViewById<RadioButton>(Resource.Id.option_1);
            radSelect[1] = FindViewById<RadioButton>(Resource.Id.option_2);
            radSelect[2] = FindViewById<RadioButton>(Resource.Id.option_3);
            radSelect[3] = FindViewById<RadioButton>(Resource.Id.option_4);

            // 字典模式
			searchText = FindViewById<EditText>(Resource.Id.search_text);
            dictSpinner = FindViewById<Spinner>(Resource.Id.spinner);
            if (dictSpinner != null)
            {
                dictSpinner.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem,
                    new string[] { "俄语", "英语", "日汉", "汉日", "语法" });
                dictSpinner.SetSelection(loadMode());
                dictSpinner.ItemSelected += Spinner_ItemSelected;
            }

            var search = FindViewById<Button>(Resource.Id.search);
            if (search != null)
            {
                search.Click += Search_Click;
            }

            var websearch = FindViewById<Button>(Resource.Id.web_search);
            if (websearch != null)
            {
                websearch.Click += Websearch_Click;
            }

            dictResultText = FindViewById<EditText>(Resource.Id.result_text);
            try
            {
                wordList = FindViewById<ListView>(Resource.Id.list);
                if (wordList != null)
                {
                    var items = new string[] {
                        "用英文查俄语的映射表",
                        "aаbьcс AдeеHн mмpрkк Bвoоtт",
                        "xхyуhн vчVчiй uцUцRя wшWщLы",
                        "EэQюqю fфrлnи lпsжZж zзdбgг"
                    };
                    var listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemChecked, items);
                    wordList.Adapter = listAdapter;
                    wordList.ChoiceMode = Android.Widget.ChoiceMode.Single;
                    wordList.ItemClick += WordList_ItemClick;
                }
            }
            catch (Exception ex)
            {
                dictResultText.Text = ex.Message;
            }
            if (fileDictionary == null)
            {
                Spinner_ItemSelected(null, null);
            }
        }

        private void TabHost1_Click(object? sender, EventArgs e)
        {
            var tabHost1 = FindViewById<TitleButton>(Resource.Id.tabHost1);
            this.currentMode = tabHost1.mode;

            var dictLine = FindViewById<LinearLayout>(Resource.Id.input_line2);
            var dictList = FindViewById<ListView>(Resource.Id.list);
            var dictScroll = FindViewById<ScrollView>(Resource.Id.result_scroll);
            var execLine = FindViewById<LinearLayout>(Resource.Id.input_line);
            var execLessonLine = FindViewById<LinearLayout>(Resource.Id.lesson_line);
            var execQuestion = FindViewById<TextView>(Resource.Id.question);
            var execOptions = FindViewById<RadioGroup>(Resource.Id.user_options);
            var execScroll = FindViewById<ScrollView>(Resource.Id.exec_scroll); 

            if (this.currentMode == 0)
            {
                dictLine.Visibility = Android.Views.ViewStates.Visible;
                dictList.Visibility = Android.Views.ViewStates.Visible;
                dictScroll.Visibility = Android.Views.ViewStates.Visible;
                execLine.Visibility = Android.Views.ViewStates.Gone;
                execLessonLine.Visibility = Android.Views.ViewStates.Gone;
                execQuestion.Visibility = Android.Views.ViewStates.Gone;
                execScroll.Visibility = Android.Views.ViewStates.Gone;
                execOptions.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                dictLine.Visibility = Android.Views.ViewStates.Gone;
                dictList.Visibility = Android.Views.ViewStates.Gone;
                dictScroll.Visibility = Android.Views.ViewStates.Gone;
                execLine.Visibility = Android.Views.ViewStates.Visible;
                execLessonLine.Visibility = Android.Views.ViewStates.Visible;
                execQuestion.Visibility = Android.Views.ViewStates.Visible;
                execScroll.Visibility = Android.Views.ViewStates.Visible;
                execOptions.Visibility = Android.Views.ViewStates.Visible;
            }
        }

        private String[] LoadAllLesson()
        {
            AssetManager assets = this.Assets;
            String[] lessonFolders = assets.List("exercises");
            ArrayList lessonList = new ArrayList();
            foreach (string lessonFolder in lessonFolders)
            {
                String[] lessons = assets.List("exercises/" + lessonFolder);
                foreach (string lesson in lessons)
                {
                    string lessonFileName = lessonFolder + "/" + lesson;
                    lessonList.Add(lessonFileName.Substring(0, lessonFileName.Length - 4));
                }
            }
            return (String[])lessonList.ToArray(typeof(string));
        }


        private int loadMode()
        {
            int index = 0;
            string cacheFileName = CacheDir.AbsolutePath + "/dict_config.ini";
            if (File.Exists(cacheFileName))
            {
                try
                {
                    StreamReader reader = new StreamReader(cacheFileName, Encoding.UTF8);
                    index = Convert.ToInt32(reader.ReadToEnd());
                    reader.Close();
                }
                catch (Exception exp)
                {

                }
            }
            return index;
        }

        private void saveMode(int index)
        {
            string cacheFileName = CacheDir.AbsolutePath + "/dict_config.ini";
            StreamWriter writer = new StreamWriter(cacheFileName, false, Encoding.UTF8);
            writer.Write(index.ToString());
            writer.Close();
        }
        private int loadLessonIndex()
        {
            int index = 0;
            string cacheFileName = CacheDir.AbsolutePath + "/exec_config.ini";
            if (File.Exists(cacheFileName))
            {
                try
                {
                    StreamReader reader = new StreamReader(cacheFileName, Encoding.UTF8);
                    index = Convert.ToInt32(reader.ReadToEnd());
                    reader.Close();
                }
                catch (Exception exp)
                {

                }
            }
            return index;
        }

        private void saveLessonIndex(int index)
        {
            string cacheFileName = CacheDir.AbsolutePath + "/exec_config.ini";
            StreamWriter writer = new StreamWriter(cacheFileName, false, Encoding.UTF8);
            writer.Write(index.ToString());
            writer.Close();
        }

        private string getExercisePath()
        {
            return "exercises/";
        }

        private string getRecordPath()
        {
            return CacheDir.AbsolutePath + "/records/";
        }

        private void levelBar_SelectedIndexChanged(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.Filter(Convert.ToInt32(this.levelSpinner.SelectedItem.ToString()));
        }

        private void btnPrew_Click(object? sender, EventArgs e)
        {
            if (this.chkTestMode.Checked)
            {
                this.UpdateWordLevel();
            }
            this.SetWord(this.GetPrewWord());
            SetCurrentPlaceMessage();
        }


        public void btnNext_Click(object? sender, EventArgs e)
        {
            if (!this.chkTestMode.Checked)
            {
                showNextWord();
            }
            else
            {
                this.btnResult_Click(null, null);
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Thread.Sleep(2000);
                    RunOnUiThread(() =>
                    {
                        showNextWord();
                    });
                })).Start();
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

        public void btnResult_Click(object? sender, EventArgs e)
        {
            int correctId = GetCorrectID();
            if (correctId != -1)
            {
                radSelect[correctId].SetTextColor(Color.Red);
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
                    if (CurrentLesson.lessonType == 1)
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
                this.execResultText.Text = buffer.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void showNextWord()
        {
            if (this.chkTestMode.Checked)
            {
                this.UpdateWordLevel();
            }
            this.SetWord(this.GetNextWord());
            SetCurrentPlaceMessage();
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
                radSelect[i].SetTextColor(Color.Black);
            }
        }

        private void radio_Click(object? sender, EventArgs e)
        {
            this.SetWord(this.GetCurrentWord());
        }

        private void lstClassName_SelectedIndexChanged(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                if (currentClassName != "")
                    SaveWordLevel();

                saveLessonIndex((int)lessonSpinner.SelectedItemId);
                string lessonName = lessonSpinner.SelectedItem.ToString();

                string[] array = lessonName.Split(new char[] { '/' });
                currentClassFolder = array[0];
                currentClassName = array[1];
                if (currentClassFolder.StartsWith("jp"))
                {
                    finder = new DictionaryFinder(this.Assets, "dicts/jp-cn/data", 50);
                }
                else if (currentClassFolder.StartsWith("ra"))
                {
                    finder = new DictionaryFinder(this.Assets, "dicts/ra-cn/data", 50);
                }
                else
                {
                    finder = new DictionaryFinder(this.Assets, "dicts/en-cn/data", 50);
                }
                LoadLesson();
                SetWord(GetCurrentWord());
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private void LoadLesson()
        {
            string lessonFileName = getExercisePath() +
                currentClassFolder + "/" + currentClassName + ".txt";
            

            string levelFileName = getRecordPath() +
                currentClassFolder + "/" + currentClassName + ".level";

            Lesson lesson = Lesson.GetLesson(this.Assets.Open(lessonFileName), levelFileName);
            if (lesson == null)
            {
                return;
            }
            this.CurrentLesson = lesson;

            this.FilterLesson.Clear();

            foreach (Exercise item in lesson.exerciseList)
            {
                this.FilterLesson.Add(item);
            }
            
            this.currentPlace = 0;
            this.currentLevel = 0;
            SetCurrentPlaceMessage();
            this.radSelect[0].Checked = true;
			levelSpinner.SetSelection(0);
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
            this.currentPlace = 0;
            SetCurrentPlaceMessage();
            if (FilterLesson.Count != 0)
            {
                this.SetWord(this.GetCurrentWord());
            }
            else
            {
                labQuestion.Text = "该课程学习结束，请选择其他课程";
                execResultText.Text = "该课程学习结束，请选择其他课程";
                SetWord(null);
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
                return (Exercise)this.FilterLesson[this.currentPlace];
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
            this.currentPlace++;
            if (this.currentPlace >= this.FilterLesson.Count)
            {
                if (this.chkTestMode.Checked) 
                { 
                    if (this.currentLevel < 5)
                    {
                        currentLevel++;
                        levelSpinner.SetSelection(currentLevel);
                    }
                }
                this.currentPlace = 0;
            }
            if (this.currentPlace % 20 == 0)
                if (this.chkTestMode.Checked)
                    SaveWordLevel();
            try
            {
                return (Exercise)this.FilterLesson[this.currentPlace];
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
            this.currentPlace--;
            if (this.currentPlace < 0)
            {
                this.currentPlace = this.FilterLesson.Count - 1;
            }
            try
            {
                return (Exercise)this.FilterLesson[this.currentPlace];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SetCurrentPlaceMessage()
        {
            
            if (labMessage == null)
            {
                return;
            }

            if (FilterLesson.Count > 0)
            {
                labMessage.Text = (currentPlace + 1) + "/" + FilterLesson.Count;
            }
            else
            {
                labMessage.Text = "0/0";
            }
        }

        private void SetWord(Exercise word)
        {
            SetNormalColor();
            if (word != null)
            {               
                this.labQuestion.Text = word.Question;
                for (int i = 0; i < 4; i++)
                {
                    radSelect[i].Text = word.Answers[i];
                }
                this.execResultText.Text = "";                             
            }
            else
            {
                this.labQuestion.Text = "";
                this.execResultText.Text = "";
                for (int i = 0; i < 4; i++)
                {
                    radSelect[i].Text = "";
                }
            }
        }

        private void UpdateWordLevel()
        {
            Exercise currentWord = this.GetCurrentWord();
            if (currentWord != null)
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

	    private void ExerciseWeb_Click(object? sender, EventArgs e)
        {
            Exercise currentWord = this.GetCurrentWord();
            if (CurrentLesson.lessonType == 0 && currentWord != null)
            {
                string weburi = getWebUri(currentWord.Question);
                Intent intent = new Intent();
                intent.SetAction(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse(weburi));
                StartActivity(intent);
            }
        }

        private void Websearch_Click(object? sender, EventArgs e)
        {
            string inputText = change(searchText.Text);
            string weburi = getWebUri(inputText);

            Intent intent = new Intent();
            intent.SetAction(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse(weburi));
            StartActivity(intent);
        }

        private void Spinner_ItemSelected(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (dictSpinner !=null && dictSpinner.SelectedItem != null)
            {
                changeDictionary(dictSpinner.SelectedItem.ToString());
                saveMode((int)dictSpinner.SelectedItemId);
            }            
        }

        private void WordList_ItemClick(object? sender, AdapterView.ItemClickEventArgs e)
        {
            if (resultList != null && resultList.Count > e.Position)
            {                
                KeyNode selectNode = (KeyNode)resultList[e.Position];                
                string nextKeyword = selectNode.key;
                int find = selectNode.key.IndexOf("【");
                if (find != -1)
                {
                    nextKeyword = selectNode.key.Substring(0, find);
                }
                searchText.Text = nextKeyword;
                dictResultText.Text = fileDictionary.getData(selectNode);
            }
        }

        void changeDictionary(string dictName)
        {
            if (dictName == "英语")
            {
                fileDictionary = new FileDictionary(this.Assets, "dicts/en-cn/data", 30);
            }
            else if (dictName == "日汉")
            {
                fileDictionary = new FileDictionary(this.Assets, "dicts/jp-cn/data", 30);
            }
            else if (dictName == "汉日")
            {
                fileDictionary = new FileDictionary(this.Assets, "dicts/cn-jp/data", 30);
            }
            else if (dictName == "语法")
            {
                fileDictionary = new FileDictionary(this.Assets, "dicts/grammar/data", 30);
            }
            else
            {
                //俄语
                fileDictionary = new FileDictionary(this.Assets, "dicts/ra-cn/data", 30);
            }            
            currentType = dictName;
        }


		
        ArrayList resultList;
        private void Search_Click(object? sender, EventArgs e)
        {
            string tempStr;
			if (currentType == "语法")
			{
                // 使用输入的查一下
                tempStr = searchText.Text.Trim();
				resultList = fileDictionary.getFuzzLikeData(tempStr);
				if (resultList.Count == 0)
				{
                    // 使用俄语变化的查一下
                    tempStr = toRussia(searchText.Text).Trim();
					resultList = fileDictionary.getFuzzLikeData(tempStr);
					if (resultList.Count == 0)
					{
                        // 使用日语变化的查一下
                        tempStr = toJapanese(searchText.Text).Trim();
						resultList = fileDictionary.getFuzzLikeData(tempStr);
					}
				}
			} 
			else
			{
                //非语法，转换完查一次
                tempStr = change(searchText.Text);
				resultList = fileDictionary.getStartLikeData(tempStr);
			}
			searchText.Text = tempStr;            

            if (resultList.Count > 0)
            {
                ArrayList temp = new ArrayList();                
                for (int i = 0; i < resultList.Count; i++)
                {
                    temp.Add(((KeyNode)resultList[i]).key);
                }
                var items = (string[])temp.ToArray(typeof(string));
                var listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemChecked, items);
                wordList.Adapter = listAdapter;
                int selectIndex = 0;

                if (currentType == "语法")
                {
                    if (resultList.Count > 0)
                    {
                        selectIndex = 0;
                    }
                }
                else
                {
                    if (resultList.Count > 1)
                    {
                        selectIndex = 1;
                    }
                }

                wordList.SetItemChecked(selectIndex, true);
                KeyNode selectNode = (KeyNode)resultList[selectIndex];
                dictResultText.Text = fileDictionary.getData(selectNode);
            }
            else
            {
                ArrayList temp = new ArrayList();
                var items = (string[])temp.ToArray(typeof(string));
                var listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemChecked, items);
                wordList.Adapter = listAdapter;
                dictResultText.Text = "";
            }
        }

        private string change(string en)
        {
            string changed = en;

            if (currentType == "俄语")
            {
                changed = toRussia(changed);
            }
            else if (currentType == "日汉")
            {
                changed = toJapanese(changed);
            }

            return changed;
        }

        bool isRussia(string input)
        {
            foreach(char c in input)
            {
                if (c == ' ')
                {
                    continue;
                }
                int i = Convert.ToInt32(c);
                if (i < 1050 || i > 1150)
                {
                    return false;
                }
            }
            return true;
        }

        bool isEnglish(string input)
        {
            foreach (char c in input)
            {
                if (c == ' ')
                {
                    continue;
                }
                int i = Convert.ToInt32(c);
                if (i > 200)
                {
                    return false;
                }
            }
            return true;
        }

        string getWebUri(string input)
        {
            string target = "https://translate.yandex.com/?source_lang=en&target_lang=zh&text=";

            if (currentMode == 0 && currentType == "俄语")
            {
                target = "https://translate.yandex.com/?source_lang=ru&target_lang=zh&text=";
                if (!(isRussia(input)))
                {
                    target = "https://translate.yandex.com/?source_lang=zh&target_lang=ru&text=";
                }
            }
            else if (currentMode == 0 && currentType == "英语")
            {
                target = "https://translate.yandex.com/?source_lang=en&target_lang=zh&text=";
                if (!(isEnglish(input)))
                {
                    target = "https://translate.yandex.com/?source_lang=zh&target_lang=en&text=";
                }
            }
            else if (currentMode == 0 && currentType == "日汉")
            {
                target = "https://translate.yandex.com/?source_lang=ja&target_lang=zh&text=";
            }
            else if (currentMode == 0 && currentType == "汉日")
            {
                target = "https://translate.yandex.com/?source_lang=zh&target_lang=ja&text=";
            }
            else if (currentMode == 1 && currentClassFolder.StartsWith("ra"))
            {
                target = "https://translate.yandex.com/?source_lang=ru&target_lang=zh&text=";
            }
            else if (currentMode == 1 && currentClassFolder.StartsWith("jp"))
            {
                target = "https://translate.yandex.com/?source_lang=ja&target_lang=zh&text=";
            }
            else if (currentMode == 1 && currentClassFolder.StartsWith("en"))
            {
                target = "https://translate.yandex.com/?source_lang=en&target_lang=zh&text=";
            }

            return target + input;
        }

        Hashtable rcMap = null;
        string toRussia(string en)
        {
            if (rcMap == null)
            {
                rcMap = new Hashtable();

                string input = "adBgAeZznikrmHhospcCtTyfxUuwWbLEQRvVql";
                string map = "абвгдежзийклмнножрссттуфхццшщьыэюяччюп";
                for (int i = 0; i < input.Length; i++)
                {
                    rcMap[input[i]] = map[i];
                }
            }
            string result = "";
            foreach (char c in en)
            {
                if (rcMap.ContainsKey(c))
                {
                    result += rcMap[c];
                }
                else
                {
                    result += c;
                }
            }
            return result.ToString();
        }

        string jcMap = null;
        string toJapanese(string str)
        {
            //处理首字母大写
            if (str.Length > 1)
            {
                char c1 = str[0];
                char c2 = str[1];
                if (c1 >= 'A' && c1 <= 'Z' && c2 >= 'a' && c2 <= 'z')
                {
                    str = str.ToLower();
                }
            }

            //处理日文
            Stack stack = new Stack();
            string sub, tmp;
            int len = str.Length;
            int end = len;
            int i = len - 1;
            while (i >= 0)
            {
                sub = str.Substring(i, end - i);
                if (kanaChar.IndexOf(sub) == -1)
                {
                    if (i + 1 == end)
                    {
                        i--;
                    }
                    tmp = str.Substring(i + 1, end - (i + 1));
                    stack.Push(tmp);
                    end = i + 1;
                }
                else
                {
                    i--;
                }
            }
            string buffer = "";
            tmp = str.Substring(0, end);
            stack.Push(tmp);
            while (stack.Count > 0)
            {
                buffer += changeJapaneseChar((string)(stack.Pop()));
            }
            string input = buffer;

            if (jcMap == null)
            {
                jcMap = "醜丑専专業业東东糸丝両两厳严喪丧個个豊丰臨临為为麗丽挙举義义楽乐習习郷乡" +
                    "書书買买雲云亜亚産产畝亩親亲億亿僕仆従从倉仓儀仪価价衆众優优傘伞偉伟伝传傷伤" +
                    "倫伦偽伪仏佛偵侦側侧倹俭債债値值傾倾仮假償偿児儿関关興兴養养獣兽冊册軍军農农" +
                    "塚冢氷冰沖冲決决況况凍冻準准涼凉減减幾几撃击則则剛刚創创別别剤剂剣剑劇剧勧劝" +
                    "務务動动労劳勢势勲勋昇升華华協协単单売卖衛卫巻卷歴历暦历圧压県县発发変变畳叠" +
                    "隻只葉叶嘆叹後后嚇吓聴听啓启呉吴員员詠咏響响喚唤噴喷団团園园囲围図图円圆聖圣" +
                    "場场壊坏塊块堅坚壇坛墳坟墜坠塁垒墾垦増增殻壳壱壹処处備备複复復复頭头誇夸奪夺" +
                    "奮奋奨奖粧妆婦妇姉姐娯娱孫孙寧宁実实審审憲宪宮宫寛宽賓宾対对尋寻導导層层歳岁" +
                    "島岛巣巢幣币帥帅師师帳帐帯带幹干並并併并広广荘庄慶庆庫库応应廃废開开異异棄弃" +
                    "弔弗張张弾弹強强帰归録录徴征徳德憶忆憂忧懐怀態态総总懇恳悪恶悩恼懸悬驚惊恵惠" +
                    "懲惩慣惯癒愈憤愤願愿戯戏戦战撲扑託托執执拡扩掃扫揚扬護护報报抜拔擬拟擁拥択择" +
                    "掛挂揮挥損损換换拠据挿插捜搜摂摄収收効效敵敌勅敕斎斋無无時时曇昙顕显暁晓暫暂" +
                    "術术機机殺杀雑杂権权極极構构査查標标桟栈棟栋欄栏樹树様样橋桥検检桜樱歓欢歩步" +
                    "気气漢汉汚污湯汤溝沟涙泪潟泻沢泽潔洁濁浊測测済济濃浓渦涡潤润渋涩漬渍漸渐漁渔" +
                    "渓溪満满濫滥浜滨瀬濑滅灭霊灵災灾砲炮錬炼煙烟煩烦焼烧熱热薫熏愛爱犠牺猶犹獄狱" +
                    "猟猎環环現现璽玺電电療疗塩盐監监盤盘矯矫鉱矿礎础確确砕碎禍祸離离種种積积稲稻" +
                    "窮穷窯窑窓窗競竞篤笃築筑簡简類类糧粮粋粹緊紧糾纠紅红繊纤約约級级紀纪緯纬純纯" +
                    "綱纲納纳縦纵紛纷紙纸紋纹紡纺線线紺绀練练組组紳绅細细織织終终紹绍経经結结絵绘" +
                    "給给絡络絶绝絞绞統统絹绢継继績绩緒绪続续縄绳維维綿绵緑绿緩缓締缔編编縁缘縛缚" +
                    "縫缝縮缩繕缮繰缲缶罐網网羅罗罰罚罷罢職职粛肃腸肠膚肤脹胀脅胁勝胜臓脏脳脑騰腾" +
                    "舎舍艦舰芸艺節节範范繭茧薦荐栄荣薬药獲获蛍萤営营蔵藏虜虏慮虑襲袭見见観观規规" +
                    "視视覧览覚觉謄誊計计訂订認认討讨譲让訓训議议記记講讲許许論论訟讼設设訪访証证" +
                    "評评識识詐诈訴诉診诊詞词詔诏訳译試试詩诗詰诘誠诚話话誕诞該该詳详語语誤误誘诱" +
                    "説说請请諸诸諾诺読读課课調调談谈謀谋謁谒諭谕諮谘謝谢謡谣謙谦謹谨譜谱穀谷貝贝" +
                    "貞贞負负貢贡財财責责賢贤敗败貨货質质販贩貧贫購购貯贮貫贯貴贵貸贷貿贸費费賀贺" +
                    "賊贼賄贿賃赁資资賦赋賞赏賜赐賠赔頼赖賛赞贈赠躍跃車车軌轨軒轩転转輪轮軟软軸轴" +
                    "軽轻載载較较輩辈輝辉輸输轄辖辺边遼辽達达遷迁過过運运還还進进遠远違违連连遅迟" +
                    "適适選选遺遗郵邮醸酿酔醉酢醋採采釈释裏里鑑鉴針针釣钓鈍钝鐘钟鋼钢銭钱鉢钵鉄铁" +
                    "鈴铃鉛铅銅铜銑铣銘铭銃铳銀银鋳铸舗铺鎖锁鋭锐錯错錘锤錠锭鍛锻鎮镇鏡镜長长門门" +
                    "閉闭問问閑闲間间聞闻閥阀閣阁閲阅隊队陽阳陰阴陣阵階阶際际陸陆陳陈険险陥陷隠隐" +
                    "隷隶難难彫雕霧雾覇霸韻韵頂顶項项順顺頑顽顧顾頒颁預预領领頻频題题顔颜額额風风" +
                    "飛飞飢饥飯饭飲饮飾饰飽饱飼饲餓饿館馆馬马駄驮駆驱駐驻駅驿験验騎骑騒骚髄髓魚鱼" +
                    "鮮鲜鯨鲸鳥鸟鶏鸡鳴鸣黒黑黙默斉齐歯齿齢龄竜龙亀龟戸户藍蓝夾夹頬颊跡迹佇伫鬱郁" +
                    "呑吞乗乘暢畅儲储嘩哗補补渉涉腫腫雰氛穏稳頓顿鍋锅餅饼嵐岚蘇苏駒驹馴驯馳驰諦谛" +
                    "鍵键囁嗫潰溃駝驼駁驳驥骥驟骤駿骏驃骠騙骗筆笔楊杨";
            }
            len = jcMap.Length;
            for (i = 0; i < len - 1;)
            {
                input = input.Replace(jcMap[i + 1], jcMap[i]);
                i = i + 2;
            }
            return input;
        }

        string kanaChar = "n,a,A,i,I,u,U,e,E,o,O,ka,KA,ki,KI,ku,KU,ke,KE,ko,KO,sa,SA,si,SI,su,SU,se,SE,so,SO,ta,TA,ti,TI,tu,TU,te,TE,to,TO,na,NA,ni,NI,nu,NU,ne,NE,no,NO,ha,HA,hi,HI,hu,HU,fu,FU,he,HE,ho,HO,ma,MA,mi,MI,mu,MU,me,ME,mo,MO,ya,YA,yu,YU,yo,YO,ra,RA,ri,RI,ru,RU,re,RE,ro,RO,wa,WA,nn,NN,ga,GA,gi,GI,gu,GU,ge,GE,go,GO,za,ZA,zi,ZI,ji,JI,zu,ZU,ze,ZE,zo,ZO,da,DA,di,DI,du,DU,de,DE,do,DO,ba,BA,bi,BI,bu,BU,be,BE,bo,BO,pa,PA,pi,PI,pu,PU,pe,PE,po,PO,xa,XA,xi,XI,xu,XU,xe,XE,xo,XO,xka,XKA,xtu,XTU,kya,KYA,kyu,KYU,kyo,KYO,sya,SYA,syu,SYU,syo,SYO,tya,TYA,tyu,TYU,tyo,TYO,nya,NYA,nyu,NYU,nyo,NYO,hya,HYA,hyu,HYU,hyo,HYO,mya,MYA,myu,MYU,myo,MYO,rya,RYA,ryu,RYU,ryo,RYO,gya,GYA,gyu,GYU,gyo,GYO,zya,ZYA,zyu,ZYU,zyo,ZYO,jya,JYA,jyu,JYU,jyo,JYO,dya,DYA,dyu,DYU,dyo,DYO,bya,BYA,byu,BYU,byo,BYO,pya,PYA,pyu,PYU,pyo,PYO,";

        string changeJapaneseChar(string input)
        {
            if (input.IndexOf("PYO") != -1)
                return ("ピョ");
            else if (input.IndexOf("pyo") != -1)
                return ("ぴょ");
            else if (input.IndexOf("PYU") != -1)
                return ("ピュ");
            else if (input.IndexOf("pyu") != -1)
                return ("ぴゅ");
            else if (input.IndexOf("PYA") != -1)
                return ("ピャ");
            else if (input.IndexOf("pya") != -1)
                return ("ぴゃ");
            else if (input.IndexOf("BYO") != -1)
                return ("ビョ");
            else if (input.IndexOf("byo") != -1)
                return ("びょ");
            else if (input.IndexOf("BYU") != -1)
                return ("ビュ");
            else if (input.IndexOf("byu") != -1)
                return ("びゅ");
            else if (input.IndexOf("BYA") != -1)
                return ("ビャ");
            else if (input.IndexOf("bya") != -1)
                return ("びゃ");
            else if (input.IndexOf("DYO") != -1)
                return ("ヂョ");
            else if (input.IndexOf("dyo") != -1)
                return ("ぢょ");
            else if (input.IndexOf("DYU") != -1)
                return ("ヂュ");
            else if (input.IndexOf("dyu") != -1)
                return ("ぢゅ");
            else if (input.IndexOf("DYA") != -1)
                return ("ヂャ");
            else if (input.IndexOf("dya") != -1)
                return ("ぢゃ");
            else if (input.IndexOf("JYO") != -1)
                return ("ジョ");
            else if (input.IndexOf("jyo") != -1)
                return ("じょ");
            else if (input.IndexOf("JYU") != -1)
                return ("ジュ");
            else if (input.IndexOf("jyu") != -1)
                return ("じゅ");
            else if (input.IndexOf("JYA") != -1)
                return ("ジャ");
            else if (input.IndexOf("jya") != -1)
                return ("じゃ");
            else if (input.IndexOf("ZYO") != -1)
                return ("ジョ");
            else if (input.IndexOf("zyo") != -1)
                return ("じょ");
            else if (input.IndexOf("ZYU") != -1)
                return ("ジュ");
            else if (input.IndexOf("zyu") != -1)
                return ("じゅ");
            else if (input.IndexOf("ZYA") != -1)
                return ("ジャ");
            else if (input.IndexOf("zya") != -1)
                return ("じゃ");
            else if (input.IndexOf("GYO") != -1)
                return ("ギョ");
            else if (input.IndexOf("gyo") != -1)
                return ("ぎょ");
            else if (input.IndexOf("GYU") != -1)
                return ("ギュ");
            else if (input.IndexOf("gyu") != -1)
                return ("ぎゅ");
            else if (input.IndexOf("GYA") != -1)
                return ("ギャ");
            else if (input.IndexOf("gya") != -1)
                return ("ぎゃ");
            else if (input.IndexOf("RYO") != -1)
                return ("リョ");
            else if (input.IndexOf("ryo") != -1)
                return ("りょ");
            else if (input.IndexOf("RYU") != -1)
                return ("リュ");
            else if (input.IndexOf("ryu") != -1)
                return ("りゅ");
            else if (input.IndexOf("RYA") != -1)
                return ("リャ");
            else if (input.IndexOf("rya") != -1)
                return ("りゃ");
            else if (input.IndexOf("MYO") != -1)
                return ("ミョ");
            else if (input.IndexOf("myo") != -1)
                return ("みょ");
            else if (input.IndexOf("MYU") != -1)
                return ("ミュ");
            else if (input.IndexOf("myu") != -1)
                return ("みゅ");
            else if (input.IndexOf("MYA") != -1)
                return ("ミャ");
            else if (input.IndexOf("mya") != -1)
                return ("みゃ");
            else if (input.IndexOf("HYO") != -1)
                return ("ヒョ");
            else if (input.IndexOf("hyo") != -1)
                return ("ひょ");
            else if (input.IndexOf("HYU") != -1)
                return ("ヒュ");
            else if (input.IndexOf("hyu") != -1)
                return ("ひゅ");
            else if (input.IndexOf("HYA") != -1)
                return ("ヒャ");
            else if (input.IndexOf("hya") != -1)
                return ("ひゃ");
            else if (input.IndexOf("NYO") != -1)
                return ("ニョ");
            else if (input.IndexOf("nyo") != -1)
                return ("にょ");
            else if (input.IndexOf("NYU") != -1)
                return ("ニュ");
            else if (input.IndexOf("nyu") != -1)
                return ("にゅ");
            else if (input.IndexOf("NYA") != -1)
                return ("ニャ");
            else if (input.IndexOf("nya") != -1)
                return ("にゃ");
            else if (input.IndexOf("TYO") != -1)
                return ("チョ");
            else if (input.IndexOf("tyo") != -1)
                return ("ちょ");
            else if (input.IndexOf("TYU") != -1)
                return ("チュ");
            else if (input.IndexOf("tyu") != -1)
                return ("ちゅ");
            else if (input.IndexOf("TYA") != -1)
                return ("チャ");
            else if (input.IndexOf("tya") != -1)
                return ("ちゃ");
            else if (input.IndexOf("SYO") != -1)
                return ("ショ");
            else if (input.IndexOf("syo") != -1)
                return ("しょ");
            else if (input.IndexOf("SYU") != -1)
                return ("シュ");
            else if (input.IndexOf("syu") != -1)
                return ("しゅ");
            else if (input.IndexOf("SYA") != -1)
                return ("シャ");
            else if (input.IndexOf("sya") != -1)
                return ("しゃ");
            else if (input.IndexOf("KYO") != -1)
                return ("キャ");
            else if (input.IndexOf("kyo") != -1)
                return ("きょ");
            else if (input.IndexOf("KYU") != -1)
                return ("キュ");
            else if (input.IndexOf("kyu") != -1)
                return ("きゅ");
            else if (input.IndexOf("KYA") != -1)
                return ("キャ");
            else if (input.IndexOf("kya") != -1)
                return ("きゃ");
            else if (input.IndexOf("XTU") != -1)
                return ("っ");
            else if (input.IndexOf("xtu") != -1)
                return ("っ");
            else if (input.IndexOf("XKA") != -1)
                return ("ヵ");
            else if (input.IndexOf("xka") != -1)
                return ("ヵ");
            else if (input.IndexOf("XO") != -1)
                return ("ォ");
            else if (input.IndexOf("xo") != -1)
                return ("ぉ");
            else if (input.IndexOf("XE") != -1)
                return ("ェ");
            else if (input.IndexOf("xe") != -1)
                return ("ぇ");
            else if (input.IndexOf("XU") != -1)
                return ("ゥ");
            else if (input.IndexOf("xu") != -1)
                return ("ぅ");
            else if (input.IndexOf("XI") != -1)
                return ("ィ");
            else if (input.IndexOf("xi") != -1)
                return ("ぃ");
            else if (input.IndexOf("XA") != -1)
                return ("ァ");
            else if (input.IndexOf("xa") != -1)
                return ("ぁ");
            else if (input.IndexOf("PO") != -1)
                return ("ポ");
            else if (input.IndexOf("po") != -1)
                return ("ぽ");
            else if (input.IndexOf("PE") != -1)
                return ("ペ");
            else if (input.IndexOf("pe") != -1)
                return ("ぺ");
            else if (input.IndexOf("PU") != -1)
                return ("プ");
            else if (input.IndexOf("pu") != -1)
                return ("ぷ");
            else if (input.IndexOf("PI") != -1)
                return ("ピ");
            else if (input.IndexOf("pi") != -1)
                return ("ぴ");
            else if (input.IndexOf("PA") != -1)
                return ("パ");
            else if (input.IndexOf("pa") != -1)
                return ("ぱ");
            else if (input.IndexOf("BO") != -1)
                return ("ボ");
            else if (input.IndexOf("bo") != -1)
                return ("ぼ");
            else if (input.IndexOf("BE") != -1)
                return ("ベ");
            else if (input.IndexOf("be") != -1)
                return ("べ");
            else if (input.IndexOf("BU") != -1)
                return ("ブ");
            else if (input.IndexOf("bu") != -1)
                return ("ぶ");
            else if (input.IndexOf("BI") != -1)
                return ("ビ");
            else if (input.IndexOf("bi") != -1)
                return ("び");
            else if (input.IndexOf("BA") != -1)
                return ("バ");
            else if (input.IndexOf("ba") != -1)
                return ("ば");
            else if (input.IndexOf("DO") != -1)
                return ("ド");
            else if (input.IndexOf("do") != -1)
                return ("ど");
            else if (input.IndexOf("DE") != -1)
                return ("デ");
            else if (input.IndexOf("de") != -1)
                return ("で");
            else if (input.IndexOf("DU") != -1)
                return ("ヅ");
            else if (input.IndexOf("du") != -1)
                return ("づ");
            else if (input.IndexOf("DI") != -1)
                return ("ヂ");
            else if (input.IndexOf("di") != -1)
                return ("ぢ");
            else if (input.IndexOf("DA") != -1)
                return ("ダ");
            else if (input.IndexOf("da") != -1)
                return ("だ");
            else if (input.IndexOf("ZO") != -1)
                return ("ゾ");
            else if (input.IndexOf("zo") != -1)
                return ("ぞ");
            else if (input.IndexOf("ZE") != -1)
                return ("ゼ");
            else if (input.IndexOf("ze") != -1)
                return ("ぜ");
            else if (input.IndexOf("ZU") != -1)
                return ("ズ");
            else if (input.IndexOf("zu") != -1)
                return ("ず");
            else if (input.IndexOf("JI") != -1)
                return ("ジ");
            else if (input.IndexOf("ji") != -1)
                return ("じ");
            else if (input.IndexOf("ZI") != -1)
                return ("ジ");
            else if (input.IndexOf("zi") != -1)
                return ("じ");
            else if (input.IndexOf("ZA") != -1)
                return ("ザ");
            else if (input.IndexOf("za") != -1)
                return ("ざ");
            else if (input.IndexOf("GO") != -1)
                return ("ゴ");
            else if (input.IndexOf("go") != -1)
                return ("ご");
            else if (input.IndexOf("GE") != -1)
                return ("ゲ");
            else if (input.IndexOf("ge") != -1)
                return ("げ");
            else if (input.IndexOf("GU") != -1)
                return ("グ");
            else if (input.IndexOf("gu") != -1)
                return ("ぐ");
            else if (input.IndexOf("GI") != -1)
                return ("ギ");
            else if (input.IndexOf("gi") != -1)
                return ("ぎ");
            else if (input.IndexOf("GA") != -1)
                return ("ガ");
            else if (input.IndexOf("ga") != -1)
                return ("が");
            else if (input.IndexOf("NN") != -1)
                return ("ン");
            else if (input.IndexOf("nn") != -1)
                return ("ん");
            else if (input.IndexOf("WA") != -1)
                return ("ワ");
            else if (input.IndexOf("wa") != -1)
                return ("わ");
            else if (input.IndexOf("RO") != -1)
                return ("ロ");
            else if (input.IndexOf("ro") != -1)
                return ("ろ");
            else if (input.IndexOf("RE") != -1)
                return ("レ");
            else if (input.IndexOf("re") != -1)
                return ("れ");
            else if (input.IndexOf("RU") != -1)
                return ("ル");
            else if (input.IndexOf("ru") != -1)
                return ("る");
            else if (input.IndexOf("RI") != -1)
                return ("リ");
            else if (input.IndexOf("ri") != -1)
                return ("り");
            else if (input.IndexOf("RA") != -1)
                return ("ラ");
            else if (input.IndexOf("ra") != -1)
                return ("ら");
            else if (input.IndexOf("YO") != -1)
                return ("ヨ");
            else if (input.IndexOf("yo") != -1)
                return ("よ");
            else if (input.IndexOf("YU") != -1)
                return ("ユ");
            else if (input.IndexOf("yu") != -1)
                return ("ゆ");
            else if (input.IndexOf("YA") != -1)
                return ("ヤ");
            else if (input.IndexOf("ya") != -1)
                return ("や");
            else if (input.IndexOf("MO") != -1)
                return ("モ");
            else if (input.IndexOf("mo") != -1)
                return ("も");
            else if (input.IndexOf("ME") != -1)
                return ("メ");
            else if (input.IndexOf("me") != -1)
                return ("め");
            else if (input.IndexOf("MU") != -1)
                return ("ム");
            else if (input.IndexOf("mu") != -1)
                return ("む");
            else if (input.IndexOf("MI") != -1)
                return ("ミ");
            else if (input.IndexOf("mi") != -1)
                return ("み");
            else if (input.IndexOf("MA") != -1)
                return ("マ");
            else if (input.IndexOf("ma") != -1)
                return ("ま");
            else if (input.IndexOf("HO") != -1)
                return ("ホ");
            else if (input.IndexOf("ho") != -1)
                return ("ほ");
            else if (input.IndexOf("HE") != -1)
                return ("ヘ");
            else if (input.IndexOf("he") != -1)
                return ("へ");
            else if (input.IndexOf("FU") != -1)
                return ("フ");
            else if (input.IndexOf("fu") != -1)
                return ("ふ");
            else if (input.IndexOf("HU") != -1)
                return ("フ");
            else if (input.IndexOf("hu") != -1)
                return ("ふ");
            else if (input.IndexOf("HI") != -1)
                return ("ヒ");
            else if (input.IndexOf("hi") != -1)
                return ("ひ");
            else if (input.IndexOf("HA") != -1)
                return ("ハ");
            else if (input.IndexOf("ha") != -1)
                return ("は");
            else if (input.IndexOf("NO") != -1)
                return ("ノ");
            else if (input.IndexOf("no") != -1)
                return ("の");
            else if (input.IndexOf("NE") != -1)
                return ("ネ");
            else if (input.IndexOf("ne") != -1)
                return ("ね");
            else if (input.IndexOf("NU") != -1)
                return ("ヌ");
            else if (input.IndexOf("nu") != -1)
                return ("ぬ");
            else if (input.IndexOf("NI") != -1)
                return ("ニ");
            else if (input.IndexOf("ni") != -1)
                return ("に");
            else if (input.IndexOf("NA") != -1)
                return ("ナ");
            else if (input.IndexOf("na") != -1)
                return ("な");
            else if (input.IndexOf("TO") != -1)
                return ("ト");
            else if (input.IndexOf("to") != -1)
                return ("と");
            else if (input.IndexOf("TE") != -1)
                return ("テ");
            else if (input.IndexOf("te") != -1)
                return ("て");
            else if (input.IndexOf("TU") != -1)
                return ("ツ");
            else if (input.IndexOf("tu") != -1)
                return ("つ");
            else if (input.IndexOf("TI") != -1)
                return ("チ");
            else if (input.IndexOf("ti") != -1)
                return ("ち");
            else if (input.IndexOf("TA") != -1)
                return ("タ");
            else if (input.IndexOf("ta") != -1)
                return ("た");
            else if (input.IndexOf("SO") != -1)
                return ("ソ");
            else if (input.IndexOf("so") != -1)
                return ("そ");
            else if (input.IndexOf("SE") != -1)
                return ("セ");
            else if (input.IndexOf("se") != -1)
                return ("せ");
            else if (input.IndexOf("SU") != -1)
                return ("ス");
            else if (input.IndexOf("su") != -1)
                return ("す");
            else if (input.IndexOf("SI") != -1)
                return ("シ");
            else if (input.IndexOf("si") != -1)
                return ("し");
            else if (input.IndexOf("SA") != -1)
                return ("サ");
            else if (input.IndexOf("sa") != -1)
                return ("さ");
            else if (input.IndexOf("KO") != -1)
                return ("コ");
            else if (input.IndexOf("ko") != -1)
                return ("こ");
            else if (input.IndexOf("KE") != -1)
                return ("ケ");
            else if (input.IndexOf("ke") != -1)
                return ("け");
            else if (input.IndexOf("KU") != -1)
                return ("ク");
            else if (input.IndexOf("ku") != -1)
                return ("く");
            else if (input.IndexOf("KI") != -1)
                return ("キ");
            else if (input.IndexOf("ki") != -1)
                return ("き");
            else if (input.IndexOf("KA") != -1)
                return ("カ");
            else if (input.IndexOf("ka") != -1)
                return ("か");
            else if (input.IndexOf("O") != -1)
                return ("オ");
            else if (input.IndexOf("o") != -1)
                return ("お");
            else if (input.IndexOf("E") != -1)
                return ("エ");
            else if (input.IndexOf("e") != -1)
                return ("え");
            else if (input.IndexOf("U") != -1)
                return ("ウ");
            else if (input.IndexOf("u") != -1)
                return ("う");
            else if (input.IndexOf("I") != -1)
                return ("イ");
            else if (input.IndexOf("i") != -1)
                return ("い");
            else if (input.IndexOf("A") != -1)
                return ("ア");
            else if (input.IndexOf("a") != -1)
                return ("あ");
            else if (input.IndexOf("n") != -1)
                return ("ん");
            else
                return (input);
        }
    }
}