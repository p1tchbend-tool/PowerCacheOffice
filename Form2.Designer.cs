namespace PowerCacheOffice
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(388, 130);
            label1.TabIndex = 0;
            label1.Text = "このファイルは更新中に変更された可能性があります。\r\n上書きしてよろしいですか？";
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.Transparent;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button1.ForeColor = System.Drawing.Color.Black;
            button1.Location = new System.Drawing.Point(16, 156);
            button1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(60, 24);
            button1.TabIndex = 13;
            button1.Text = "はい";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button2.ForeColor = System.Drawing.Color.Black;
            button2.Location = new System.Drawing.Point(84, 156);
            button2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(60, 24);
            button2.TabIndex = 14;
            button2.Text = "いいえ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.Color.Transparent;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button3.ForeColor = System.Drawing.Color.Black;
            button3.Location = new System.Drawing.Point(152, 156);
            button3.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(120, 24);
            button3.TabIndex = 15;
            button3.Text = "開いて確認する";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.Transparent;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button4.ForeColor = System.Drawing.Color.Black;
            button4.Location = new System.Drawing.Point(50, 50);
            button4.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(24, 24);
            button4.TabIndex = 2;
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.Transparent;
            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            button5.ForeColor = System.Drawing.Color.Black;
            button5.Location = new System.Drawing.Point(280, 156);
            button5.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(120, 24);
            button5.TabIndex = 16;
            button5.Text = "差分比較する";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
            ClientSize = new System.Drawing.Size(411, 191);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(button4);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Power Cache Office";
            TopMost = true;
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}