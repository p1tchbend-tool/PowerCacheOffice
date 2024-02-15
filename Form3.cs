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
            listBox1.MouseMove += (s, eventArgs) =>
            {
                var index = listBox1.IndexFromPoint(eventArgs.Location);
                if (index != ListBox.NoMatches) listBox1.SelectedIndex = index;
            };

            listBox1.MouseDown += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(listBox1.PointToScreen(eventArgs.Location));
                }
                else
                {
                    SelectedFile = listBox1.SelectedItem.ToString();
                    this.Close();
                }
            };

            toolStripMenuItem1.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;

                Clipboard.SetText(listBox1.SelectedItem.ToString());
                this.Close();
            };
        }
    }
}
