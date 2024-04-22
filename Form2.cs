using System;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal partial class Form2 : Form
    {
        public static readonly int No = 0;
        public static readonly int Yes = 1;
        public static readonly int Confirm = 2;
        public static readonly int ConfirmDiffTool = 3;

        public int Result { get; set; }

        private int initialWidth = 0;
        private int initialHeight = 0;

        protected override void WndProc(ref Message m)
        {
            const int WM_DPICHANGED = 0x02E0;
            if (m.Msg == WM_DPICHANGED)
            {
                float f = NativeMethods.GetDpiForSystem();
                this.Width = (int)Math.Round(initialWidth * (this.DeviceDpi / f));
                this.Height = (int)Math.Round(initialHeight * (this.DeviceDpi / f));

                return;
            };
            base.WndProc(ref m);
        }

        public Form2(string filePath, bool isDarkMode)
        {
            InitializeComponent();

            initialWidth = this.Width;
            initialHeight = this.Height;

            Program.SortTabIndex(this);
            Program.ChangeDarkMode(this, isDarkMode);

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

        private void button5_Click(object sender, EventArgs e)
        {
            Result = ConfirmDiffTool;
            this.Close();
        }
    }
}
