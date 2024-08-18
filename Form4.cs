using System;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form4 : Form
    {
        public string PageName { get; set; }

        public Form4(bool isDarkMode, string currentPageName)
        {
            InitializeComponent();

            textBox1.Text = currentPageName;

            Program.SortTabIndex(this);
            Program.ChangeDarkMode(this, isDarkMode);
            if (isDarkMode)
            {
                this.Icon = Properties.Resources.PowerCacheOfficeLaunchWhite;
            }
            else
            {
                this.Icon = Properties.Resources.PowerCacheOfficeLaunchBlack;
            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.PreviewKeyDown += (s, eventArgs) =>
            {
                if (eventArgs.KeyCode == Keys.Enter)
                {
                    PageName = textBox1.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PageName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
