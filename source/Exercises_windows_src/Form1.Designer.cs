namespace Exercises
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.radSelect3 = new System.Windows.Forms.RadioButton();
            this.radSelect2 = new System.Windows.Forms.RadioButton();
            this.radSelect1 = new System.Windows.Forms.RadioButton();
            this.txtDictValue = new System.Windows.Forms.TextBox();
            this.labSelect = new System.Windows.Forms.Label();
            this.chkShowChinese = new System.Windows.Forms.CheckBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.radSelect4 = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.labMessage1 = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.levelBar = new System.Windows.Forms.ComboBox();
            this.radTest = new System.Windows.Forms.RadioButton();
            this.radStudy = new System.Windows.Forms.RadioButton();
            this.cmbClassName = new System.Windows.Forms.ComboBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWeb = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radSelect3
            // 
            this.radSelect3.Font = new System.Drawing.Font("Arial", 12F);
            this.radSelect3.Location = new System.Drawing.Point(4, 76);
            this.radSelect3.Margin = new System.Windows.Forms.Padding(4);
            this.radSelect3.Name = "radSelect3";
            this.radSelect3.Size = new System.Drawing.Size(400, 30);
            this.radSelect3.TabIndex = 3;
            this.radSelect3.Visible = false;
            // 
            // radSelect2
            // 
            this.radSelect2.Font = new System.Drawing.Font("Arial", 12F);
            this.radSelect2.Location = new System.Drawing.Point(4, 40);
            this.radSelect2.Margin = new System.Windows.Forms.Padding(4);
            this.radSelect2.Name = "radSelect2";
            this.radSelect2.Size = new System.Drawing.Size(400, 30);
            this.radSelect2.TabIndex = 2;
            this.radSelect2.Visible = false;
            // 
            // radSelect1
            // 
            this.radSelect1.Checked = true;
            this.radSelect1.Font = new System.Drawing.Font("Arial", 12F);
            this.radSelect1.Location = new System.Drawing.Point(4, 4);
            this.radSelect1.Margin = new System.Windows.Forms.Padding(4);
            this.radSelect1.Name = "radSelect1";
            this.radSelect1.Size = new System.Drawing.Size(400, 30);
            this.radSelect1.TabIndex = 1;
            this.radSelect1.TabStop = true;
            this.radSelect1.Visible = false;
            // 
            // txtDictValue
            // 
            this.txtDictValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDictValue.Font = new System.Drawing.Font("Arial", 10.8F);
            this.txtDictValue.Location = new System.Drawing.Point(7, 287);
            this.txtDictValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtDictValue.Multiline = true;
            this.txtDictValue.Name = "txtDictValue";
            this.txtDictValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDictValue.Size = new System.Drawing.Size(714, 172);
            this.txtDictValue.TabIndex = 11;
            this.txtDictValue.Visible = false;
            // 
            // labSelect
            // 
            this.labSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labSelect.Font = new System.Drawing.Font("Arial", 12F);
            this.labSelect.Location = new System.Drawing.Point(11, 63);
            this.labSelect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSelect.Name = "labSelect";
            this.labSelect.Size = new System.Drawing.Size(706, 77);
            this.labSelect.TabIndex = 41;
            // 
            // chkShowChinese
            // 
            this.chkShowChinese.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowChinese.Checked = true;
            this.chkShowChinese.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowChinese.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkShowChinese.Location = new System.Drawing.Point(307, 35);
            this.chkShowChinese.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowChinese.Name = "chkShowChinese";
            this.chkShowChinese.Size = new System.Drawing.Size(120, 24);
            this.chkShowChinese.TabIndex = 6;
            this.chkShowChinese.Text = "顺带查字典";
            this.chkShowChinese.ThreeState = true;
            this.chkShowChinese.Visible = false;
            this.chkShowChinese.CheckedChanged += new System.EventHandler(this.chkShowChinese_CheckStateChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnPreview.Location = new System.Drawing.Point(482, 31);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(69, 28);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "前一个";
            this.btnPreview.Visible = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPrew_Click);
            // 
            // btnResult
            // 
            this.btnResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResult.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnResult.Location = new System.Drawing.Point(426, 31);
            this.btnResult.Margin = new System.Windows.Forms.Padding(4);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(56, 28);
            this.btnResult.TabIndex = 5;
            this.btnResult.Text = "答案";
            this.btnResult.Visible = false;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // radSelect4
            // 
            this.radSelect4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSelect4.Location = new System.Drawing.Point(4, 112);
            this.radSelect4.Margin = new System.Windows.Forms.Padding(4);
            this.radSelect4.Name = "radSelect4";
            this.radSelect4.Size = new System.Drawing.Size(400, 30);
            this.radSelect4.TabIndex = 4;
            this.radSelect4.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnNext.Location = new System.Drawing.Point(551, 31);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(64, 28);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "下一个";
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labMessage1
            // 
            this.labMessage1.Location = new System.Drawing.Point(6, 31);
            this.labMessage1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage1.Name = "labMessage1";
            this.labMessage1.Size = new System.Drawing.Size(289, 22);
            this.labMessage1.TabIndex = 16;
            // 
            // txtLevel
            // 
            this.txtLevel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.txtLevel.Location = new System.Drawing.Point(461, 2);
            this.txtLevel.Margin = new System.Windows.Forms.Padding(4);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(23, 24);
            this.txtLevel.TabIndex = 12;
            this.txtLevel.TextChanged += new System.EventHandler(this.txtLevel_TextChanged);
            // 
            // levelBar
            // 
            this.levelBar.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.levelBar.Location = new System.Drawing.Point(319, 2);
            this.levelBar.Margin = new System.Windows.Forms.Padding(4);
            this.levelBar.Name = "levelBar";
            this.levelBar.Size = new System.Drawing.Size(42, 23);
            this.levelBar.TabIndex = 11;
            this.levelBar.SelectedIndexChanged += new System.EventHandler(this.levelBar_SelectedIndexChanged);
            // 
            // radTest
            // 
            this.radTest.Font = new System.Drawing.Font("Tahoma", 8F);
            this.radTest.Location = new System.Drawing.Point(617, -1);
            this.radTest.Margin = new System.Windows.Forms.Padding(4);
            this.radTest.Name = "radTest";
            this.radTest.Size = new System.Drawing.Size(100, 30);
            this.radTest.TabIndex = 15;
            this.radTest.Text = "测验模式";
            this.radTest.Click += new System.EventHandler(this.radio_Click);
            // 
            // radStudy
            // 
            this.radStudy.Checked = true;
            this.radStudy.Font = new System.Drawing.Font("Tahoma", 8F);
            this.radStudy.Location = new System.Drawing.Point(510, -1);
            this.radStudy.Margin = new System.Windows.Forms.Padding(4);
            this.radStudy.Name = "radStudy";
            this.radStudy.Size = new System.Drawing.Size(99, 30);
            this.radStudy.TabIndex = 13;
            this.radStudy.TabStop = true;
            this.radStudy.Text = "学习模式";
            this.radStudy.Click += new System.EventHandler(this.radio_Click);
            // 
            // cmbClassName
            // 
            this.cmbClassName.Location = new System.Drawing.Point(3, 2);
            this.cmbClassName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(192, 23);
            this.cmbClassName.TabIndex = 1;
            this.cmbClassName.SelectedIndexChanged += new System.EventHandler(this.lstClassName_SelectedIndexChanged);
            // 
            // btnGoto
            // 
            this.btnGoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoto.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnGoto.Location = new System.Drawing.Point(615, 31);
            this.btnGoto.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(53, 28);
            this.btnGoto.TabIndex = 9;
            this.btnGoto.Text = "跳转";
            this.btnGoto.Visible = false;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.radSelect1);
            this.panel1.Controls.Add(this.radSelect2);
            this.panel1.Controls.Add(this.radSelect3);
            this.panel1.Controls.Add(this.radSelect4);
            this.panel1.Location = new System.Drawing.Point(6, 140);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 146);
            this.panel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "错误次数筛选";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 42;
            this.label2.Text = "本词错次数";
            // 
            // btnWeb
            // 
            this.btnWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWeb.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnWeb.Location = new System.Drawing.Point(667, 31);
            this.btnWeb.Margin = new System.Windows.Forms.Padding(4);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(54, 28);
            this.btnWeb.TabIndex = 9;
            this.btnWeb.Text = "Web";
            this.btnWeb.Visible = false;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(729, 463);
            this.Controls.Add(this.levelBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDictValue);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radTest);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.btnGoto);
            this.Controls.Add(this.radStudy);
            this.Controls.Add(this.labMessage1);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.cmbClassName);
            this.Controls.Add(this.labSelect);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.chkShowChinese);
            this.Controls.Add(this.txtLevel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "练习";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radSelect3;
        private System.Windows.Forms.RadioButton radSelect2;
        private System.Windows.Forms.RadioButton radSelect1;
        private System.Windows.Forms.TextBox txtDictValue;
        private System.Windows.Forms.Label labSelect;
        private System.Windows.Forms.CheckBox chkShowChinese;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.RadioButton radSelect4;
        private System.Windows.Forms.Label labMessage1;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.ComboBox levelBar;
        private System.Windows.Forms.RadioButton radStudy;
        private System.Windows.Forms.RadioButton radTest;
        private System.Windows.Forms.ComboBox cmbClassName;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWeb;
    }
}

