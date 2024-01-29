using System;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form2 : Form
    {
        public static readonly int No = 0;
        public static readonly int Yes = 1;
        public static readonly int Confirm = 2;

        public int Result { get; set; }

        public Form2(string filePath)
        {
            InitializeComponent();
            Result = No;
            label1.Text = "このファイルは更新中に変更された可能性があります。\n上書きしてよろしいですか？\n\n" + filePath;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button4.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = No;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Result = Confirm;
            this.Close();
        }
    }
}
