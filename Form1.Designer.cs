namespace PowerCacheOffice
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listBox1 = new System.Windows.Forms.ListBox();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            button1 = new System.Windows.Forms.Button();
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            button2 = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            panel3 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            button3 = new System.Windows.Forms.Button();
            textBox3 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            label4 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            textBox5 = new System.Windows.Forms.TextBox();
            textBox4 = new System.Windows.Forms.TextBox();
            button6 = new System.Windows.Forms.Button();
            checkBox4 = new System.Windows.Forms.CheckBox();
            checkBox5 = new System.Windows.Forms.CheckBox();
            contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            button7 = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            button8 = new System.Windows.Forms.Button();
            checkBox6 = new System.Windows.Forms.CheckBox();
            label8 = new System.Windows.Forms.Label();
            panel5 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = System.Drawing.Color.White;
            listBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            listBox1.ForeColor = System.Drawing.Color.Black;
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 18;
            listBox1.Location = new System.Drawing.Point(8, 76);
            listBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            listBox1.Name = "listBox1";
            listBox1.ScrollAlwaysVisible = true;
            listBox1.Size = new System.Drawing.Size(652, 112);
            listBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.White;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            textBox1.ForeColor = System.Drawing.Color.Black;
            textBox1.Location = new System.Drawing.Point(8, 43);
            textBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(620, 25);
            textBox1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.Black;
            label1.Location = new System.Drawing.Point(5, 21);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(493, 18);
            label1.TabIndex = 2;
            label1.Text = "追加した文字列から始まるパスはキャッシュが作成されます。 例: Z:\\, \\\\192.168.0.100";
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.IncludeSubdirectories = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.Transparent;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button1.ForeColor = System.Drawing.Color.Black;
            button1.Location = new System.Drawing.Point(636, 42);
            button1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(24, 24);
            button1.TabIndex = 4;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Power Cache Office";
            notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            contextMenuStrip1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(137, 92);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem1.Text = "表示";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem2.Text = "再起動";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem3.Text = "終了";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem4.Text = "更新の確認";
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button2.ForeColor = System.Drawing.Color.Black;
            button2.Location = new System.Drawing.Point(522, 500);
            button2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(150, 24);
            button2.TabIndex = 1;
            button2.Text = "キャッシュを検索";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.White;
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            textBox2.ForeColor = System.Drawing.Color.Black;
            textBox2.Location = new System.Drawing.Point(101, 500);
            textBox2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(250, 25);
            textBox2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.Color.Black;
            label2.Location = new System.Drawing.Point(24, 503);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 18);
            label2.TabIndex = 7;
            label2.Text = "キーワード:";
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.Transparent;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button4.ForeColor = System.Drawing.Color.Black;
            button4.Location = new System.Drawing.Point(522, 468);
            button4.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(150, 24);
            button4.TabIndex = 11;
            button4.Text = "キャッシュを削除";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.Transparent;
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button5.ForeColor = System.Drawing.Color.Black;
            button5.Location = new System.Drawing.Point(201, 468);
            button5.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(150, 24);
            button5.TabIndex = 12;
            button5.Text = "インデックスを再作成";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(560, 133);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "関連付け";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new System.Drawing.Point(42, 96);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(361, 22);
            checkBox3.TabIndex = 5;
            checkBox3.Text = "PowerPoint ファイル（ppt, pptx, pptm, odp）を関連付ける";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(42, 60);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(327, 22);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Word ファイル（doc, docx, docm, odt）を関連付ける";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(42, 24);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(316, 22);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Excel ファイル（xls, xlsx, xlsm, ods）を関連付ける";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.Transparent;
            panel3.BackgroundImage = Properties.Resources.p;
            panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel3.Location = new System.Drawing.Point(6, 96);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(30, 30);
            panel3.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.w;
            panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel2.Location = new System.Drawing.Point(6, 60);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(30, 30);
            panel2.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Transparent;
            panel1.BackgroundImage = Properties.Resources.x;
            panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel1.Location = new System.Drawing.Point(6, 24);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(30, 30);
            panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(listBox1);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(button1);
            groupBox2.Location = new System.Drawing.Point(12, 151);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(670, 205);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "キャッシュ対象フォルダ";
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.Transparent;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button3.ForeColor = System.Drawing.Color.Black;
            button3.Location = new System.Drawing.Point(522, 532);
            button3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(150, 24);
            button3.TabIndex = 15;
            button3.Text = "キャッシュを事前作成";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.Color.White;
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            textBox3.ForeColor = System.Drawing.Color.Black;
            textBox3.Location = new System.Drawing.Point(101, 532);
            textBox3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(410, 25);
            textBox3.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.Black;
            label3.Location = new System.Drawing.Point(12, 535);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(85, 18);
            label3.TabIndex = 17;
            label3.Text = "対象フォルダ:";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(20, 565);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(652, 23);
            progressBar1.TabIndex = 18;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            label4.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            label4.Location = new System.Drawing.Point(28, 567);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(402, 19);
            label4.TabIndex = 19;
            label4.Text = "対象フォルダを解析中です……";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label4.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(textBox5);
            groupBox3.Controls.Add(textBox4);
            groupBox3.Location = new System.Drawing.Point(12, 366);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(460, 95);
            groupBox3.TabIndex = 20;
            groupBox3.TabStop = false;
            groupBox3.Text = "ホットキー";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = System.Drawing.Color.Black;
            label6.Location = new System.Drawing.Point(218, 26);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(164, 18);
            label6.TabIndex = 9;
            label6.Text = "クリップボードのパスを開く";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.Color.Black;
            label5.Location = new System.Drawing.Point(218, 57);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(152, 18);
            label5.TabIndex = 8;
            label5.Text = "最近開いたファイルを表示";
            // 
            // textBox5
            // 
            textBox5.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox5.Location = new System.Drawing.Point(8, 55);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new System.Drawing.Size(200, 25);
            textBox5.TabIndex = 2;
            // 
            // textBox4
            // 
            textBox4.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox4.Location = new System.Drawing.Point(8, 24);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new System.Drawing.Size(200, 25);
            textBox4.TabIndex = 1;
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.Color.Transparent;
            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button6.ForeColor = System.Drawing.Color.Black;
            button6.Location = new System.Drawing.Point(20, 468);
            button6.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(170, 24);
            button6.TabIndex = 21;
            button6.Text = "最近開いたファイルを表示";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new System.Drawing.Point(483, 375);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(171, 22);
            checkBox4.TabIndex = 22;
            checkBox4.Text = "スタートアップに登録する";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new System.Drawing.Point(483, 431);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(183, 22);
            checkBox5.TabIndex = 23;
            checkBox5.Text = "再起動時にキャッシュを削除";
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            contextMenuStrip2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem5, toolStripMenuItem6 });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new System.Drawing.Size(149, 48);
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(148, 22);
            toolStripMenuItem5.Text = "パスのコピー";
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new System.Drawing.Size(148, 22);
            toolStripMenuItem6.Text = "削除";
            // 
            // button7
            // 
            button7.BackColor = System.Drawing.Color.Transparent;
            button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button7.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button7.ForeColor = System.Drawing.Color.Black;
            button7.Location = new System.Drawing.Point(361, 500);
            button7.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(150, 24);
            button7.TabIndex = 24;
            button7.Text = "バックアップを検索";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // label7
            // 
            label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label7.Location = new System.Drawing.Point(582, 20);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(100, 40);
            label7.TabIndex = 26;
            label7.Text = " Dark";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button8
            // 
            button8.BackColor = System.Drawing.Color.Transparent;
            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button8.ForeColor = System.Drawing.Color.Black;
            button8.Location = new System.Drawing.Point(361, 468);
            button8.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(150, 24);
            button8.TabIndex = 27;
            button8.Text = "バックアップを削除";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new System.Drawing.Point(483, 403);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new System.Drawing.Size(195, 22);
            checkBox6.TabIndex = 28;
            checkBox6.Text = "自動でバックアップを取得する";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // label8
            // 
            label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label8.Location = new System.Drawing.Point(582, 72);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(100, 40);
            label8.TabIndex = 29;
            label8.Text = " Update";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.Transparent;
            panel5.BackgroundImage = Properties.Resources.update;
            panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel5.Location = new System.Drawing.Point(650, 80);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(25, 25);
            panel5.TabIndex = 30;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.Transparent;
            panel4.BackgroundImage = Properties.Resources.moon;
            panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel4.Location = new System.Drawing.Point(645, 25);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(30, 30);
            panel4.TabIndex = 25;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            ClientSize = new System.Drawing.Size(694, 601);
            Controls.Add(label4);
            Controls.Add(panel5);
            Controls.Add(label8);
            Controls.Add(checkBox6);
            Controls.Add(button8);
            Controls.Add(panel4);
            Controls.Add(label7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button7);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(groupBox3);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(button3);
            Controls.Add(groupBox1);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(groupBox2);
            Controls.Add(progressBar1);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Power Cache Office";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
    }
}

