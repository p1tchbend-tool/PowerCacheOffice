using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form3 : Form
    {
        public string SelectedFile {  get; set; }

        public Form3(List<string> recentFiles, bool isDarkMode)
        {
            SelectedFile = string.Empty;
            InitializeComponent();

            var launchView1 = new LaunchView();
            launchView1.Size = new Size(832, 200);
            launchView1.Location = new Point(10, 11);
            tabPage1.Controls.Add(launchView1);

            var launchView2 = new LaunchView();
            launchView2.Size = new Size(832, 200);
            launchView2.Location = new Point(10, 11);
            tabPage2.Controls.Add(launchView2);

            var launchView3 = new LaunchView();
            launchView3.Size = new Size(832, 200);
            launchView3.Location = new Point(10, 11);
            tabPage3.Controls.Add(launchView3);

            var launchView4 = new LaunchView();
            launchView4.Size = new Size(832, 200);
            launchView4.Location = new Point(10, 11);
            tabPage4.Controls.Add(launchView4);

            var launchView5 = new LaunchView();
            launchView5.Size = new Size(832, 200);
            launchView5.Location = new Point(10, 11);
            tabPage5.Controls.Add(launchView5);

            Program.SortTabIndex(this);
            Program.ChangeDarkMode(this, isDarkMode);
            this.Shown += (s, e) => listBox1.Focus();

            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (var item in recentFiles.AsEnumerable().Reverse())
            {
                listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();

            timer1.Start();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            

            listBox1.MouseMove += (s, eventArgs) =>
            {
                var index = listBox1.IndexFromPoint(eventArgs.Location);
                if (index != ListBox.NoMatches) listBox1.SelectedIndex = index;

                if (listBox1.SelectedItem != null && eventArgs.Button == MouseButtons.Left)
                {
                    string[] files = { listBox1.SelectedItem.ToString() };
                    var dataObject = new DataObject(DataFormats.FileDrop, files);
                    listBox1.DoDragDrop(dataObject, DragDropEffects.Copy);
                };
            };

            listBox1.MouseClick += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                if (!listBox1.ClientRectangle.Contains(eventArgs.Location)) return;

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            var point = tabControl1.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            var rectangle = new Rectangle(point.X, point.Y, 1, 1);
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.GetTabRect(i).IntersectsWith(rectangle))
                {
                    tabControl1.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
