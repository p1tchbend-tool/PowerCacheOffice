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
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
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
            checkBox4 = new System.Windows.Forms.CheckBox();
            checkBox5 = new System.Windows.Forms.CheckBox();
            contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            label7 = new System.Windows.Forms.Label();
            button8 = new System.Windows.Forms.Button();
            checkBox6 = new System.Windows.Forms.CheckBox();
            label8 = new System.Windows.Forms.Label();
            panel5 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
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
            listBox1.Size = new System.Drawing.Size(672, 112);
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
            textBox1.Size = new System.Drawing.Size(640, 25);
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
            button1.Location = new System.Drawing.Point(656, 43);
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
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem7, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(274, 114);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(273, 22);
            toolStripMenuItem1.Text = "設定画面を表示";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new System.Drawing.Size(273, 22);
            toolStripMenuItem7.Text = "Power Cache Office Launch を表示";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(273, 22);
            toolStripMenuItem2.Text = "再起動";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(273, 22);
            toolStripMenuItem3.Text = "終了";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(273, 22);
            toolStripMenuItem4.Text = "更新の確認";
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.Transparent;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button4.ForeColor = System.Drawing.Color.Black;
            button4.Location = new System.Drawing.Point(472, 25);
            button4.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(205, 24);
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
            button5.Location = new System.Drawing.Point(8, 25);
            button5.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(205, 24);
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
            groupBox1.Size = new System.Drawing.Size(580, 133);
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
            groupBox2.Size = new System.Drawing.Size(690, 205);
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
            button3.Location = new System.Drawing.Point(590, 21);
            button3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(90, 24);
            button3.TabIndex = 15;
            button3.Text = "事前作成";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.Color.White;
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            textBox3.ForeColor = System.Drawing.Color.Black;
            textBox3.Location = new System.Drawing.Point(97, 21);
            textBox3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(483, 25);
            textBox3.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.Black;
            label3.Location = new System.Drawing.Point(8, 24);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(85, 18);
            label3.TabIndex = 17;
            label3.Text = "対象フォルダ:";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(8, 53);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(672, 23);
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
            label4.Location = new System.Drawing.Point(13, 55);
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
            groupBox3.Location = new System.Drawing.Point(12, 453);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(455, 110);
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
            label5.Location = new System.Drawing.Point(218, 62);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(225, 18);
            label5.TabIndex = 8;
            label5.Text = "「Power Cache Office Launch」を表示";
            // 
            // textBox5
            // 
            textBox5.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox5.Location = new System.Drawing.Point(8, 60);
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
            // checkBox4
            // 
            checkBox4.Location = new System.Drawing.Point(6, 22);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(200, 22);
            checkBox4.TabIndex = 22;
            checkBox4.Text = "スタートアップに登録する";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.Location = new System.Drawing.Point(6, 77);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(200, 22);
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
            // label7
            // 
            label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label7.Location = new System.Drawing.Point(602, 22);
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
            button8.Location = new System.Drawing.Point(242, 25);
            button8.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(205, 24);
            button8.TabIndex = 27;
            button8.Text = "バックアップを削除";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // checkBox6
            // 
            checkBox6.Location = new System.Drawing.Point(6, 50);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new System.Drawing.Size(200, 22);
            checkBox6.TabIndex = 28;
            checkBox6.Text = "自動でバックアップを取得する";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // label8
            // 
            label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label8.Location = new System.Drawing.Point(602, 75);
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
            panel5.Location = new System.Drawing.Point(670, 83);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(25, 25);
            panel5.TabIndex = 30;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.Transparent;
            panel4.BackgroundImage = Properties.Resources.moon;
            panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            panel4.Location = new System.Drawing.Point(665, 27);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(30, 30);
            panel4.TabIndex = 25;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(textBox3);
            groupBox4.Controls.Add(button3);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(progressBar1);
            groupBox4.Location = new System.Drawing.Point(12, 362);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(690, 85);
            groupBox4.TabIndex = 31;
            groupBox4.TabStop = false;
            groupBox4.Text = "キャッシュ事前作成";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(button5);
            groupBox5.Controls.Add(button8);
            groupBox5.Controls.Add(button4);
            groupBox5.Location = new System.Drawing.Point(12, 569);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(690, 60);
            groupBox5.TabIndex = 32;
            groupBox5.TabStop = false;
            groupBox5.Text = "クリーンアップ";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(checkBox4);
            groupBox6.Controls.Add(checkBox6);
            groupBox6.Controls.Add(checkBox5);
            groupBox6.Location = new System.Drawing.Point(478, 453);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(224, 110);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            groupBox6.Text = "共通設定";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            ClientSize = new System.Drawing.Size(714, 641);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(panel5);
            Controls.Add(label8);
            Controls.Add(panel4);
            Controls.Add(label7);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
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
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}

