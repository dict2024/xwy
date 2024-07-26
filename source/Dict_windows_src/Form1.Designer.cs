namespace Dict
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.wordDetail = new System.Windows.Forms.TextBox();
            this.wordList = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dictList = new System.Windows.Forms.ComboBox();
            this.wordInput = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(869, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.rc_Click);
            // 
            // wordDetail
            // 
            this.wordDetail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.wordDetail.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordDetail.Location = new System.Drawing.Point(232, 32);
            this.wordDetail.Multiline = true;
            this.wordDetail.Name = "wordDetail";
            this.wordDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wordDetail.Size = new System.Drawing.Size(858, 487);
            this.wordDetail.TabIndex = 10;
            // 
            // wordList
            // 
            this.wordList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.wordList.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordList.FormattingEnabled = true;
            this.wordList.ItemHeight = 21;
            this.wordList.Location = new System.Drawing.Point(2, 32);
            this.wordList.Name = "wordList";
            this.wordList.Size = new System.Drawing.Size(224, 487);
            this.wordList.TabIndex = 9;
            this.wordList.SelectedIndexChanged += new System.EventHandler(this.wordList_SelectedIndexChanged);
            this.wordList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wordList_KeyDown);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(942, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "Yandex";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.web_Click);
            // 
            // dictList
            // 
            this.dictList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dictList.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dictList.FormattingEnabled = true;
            this.dictList.Location = new System.Drawing.Point(2, 2);
            this.dictList.Name = "dictList";
            this.dictList.Size = new System.Drawing.Size(121, 26);
            this.dictList.TabIndex = 12;
            this.dictList.SelectedIndexChanged += new System.EventHandler(this.dictList_SelectedIndexChanged);
            // 
            // wordInput
            // 
            this.wordInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordInput.Font = new System.Drawing.Font("Arial", 10.8F);
            this.wordInput.FormattingEnabled = true;
            this.wordInput.Location = new System.Drawing.Point(128, 1);
            this.wordInput.Name = "wordInput";
            this.wordInput.Size = new System.Drawing.Size(732, 29);
            this.wordInput.TabIndex = 1;
            this.wordInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.wordInput_KeyDown);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1028, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 31);
            this.button3.TabIndex = 4;
            this.button3.Text = "千亿";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.qianyi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 525);
            this.Controls.Add(this.wordInput);
            this.Controls.Add(this.dictList);
            this.Controls.Add(this.wordList);
            this.Controls.Add(this.wordDetail);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "字典/словарь/Dictionary";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox wordDetail;
        private System.Windows.Forms.ListBox wordList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox dictList;
        private System.Windows.Forms.ComboBox wordInput;
        private System.Windows.Forms.Button button3;
    }
}

