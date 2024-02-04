using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form3 : Form
    {
        public string SelectedFile {  get; set; }

        public Form3(List<string> recentFiles)
        {
            SelectedFile = string.Empty;

            InitializeComponent();

            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (var item in recentFiles.AsEnumerable().Reverse())
            {
                listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listBox1.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItem != null)
                {
                    SelectedFile = listBox1.SelectedItem.ToString();
                    this.Close();
                }
            };
        }
    }
}
