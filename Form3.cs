using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form3 : Form
    {
        public string SelectedFile {  get; set; }

        private static readonly string powerCacheOfficeLaunchDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\launch");
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        private Point mousePosition = new Point();

        private LaunchView launchView1 = new LaunchView();
        private LaunchView launchView2 = new LaunchView();
        private LaunchView launchView3 = new LaunchView();
        private LaunchView launchView4 = new LaunchView();
        private LaunchView launchView5 = new LaunchView();

        public Form3(List<string> recentFiles, bool isDarkMode)
        {
            SelectedFile = string.Empty;
            if (!Directory.Exists(powerCacheOfficeLaunchDataFolder)) Directory.CreateDirectory(powerCacheOfficeLaunchDataFolder);
            InitializeComponent();

            launchView1.Name = "view1";
            launchView1.Size = new Size(832, 200);
            launchView1.Location = new Point(10, 11);
            launchView1.OnLaunch += (s, e) =>
            {
                SelectedFile = ((LaunchView.LaunchEventArgs)e).Path;
                this.Close();
            };
            launchView1.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage1.Controls.Add(launchView1);

            launchView2.Name = "view2";
            launchView2.Size = new Size(832, 200);
            launchView2.Location = new Point(10, 11);
            launchView2.OnLaunch += (s, e) =>
            {
                SelectedFile = ((LaunchView.LaunchEventArgs)e).Path;
                this.Close();
            };
            launchView2.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage2.Controls.Add(launchView2);

            launchView3.Name = "view3";
            launchView3.Size = new Size(832, 200);
            launchView3.Location = new Point(10, 11);
            launchView3.OnLaunch += (s, e) =>
            {
                SelectedFile = ((LaunchView.LaunchEventArgs)e).Path;
                this.Close();
            };
            launchView3.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage3.Controls.Add(launchView3);

            launchView4.Name = "view4";
            launchView4.Size = new Size(832, 200);
            launchView4.Location = new Point(10, 11);
            launchView4.OnLaunch += (s, e) =>
            {
                SelectedFile = ((LaunchView.LaunchEventArgs)e).Path;
                this.Close();
            };
            launchView4.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage4.Controls.Add(launchView4);

            launchView5.Name = "view5";
            launchView5.Size = new Size(832, 200);
            launchView5.Location = new Point(10, 11);
            launchView5.OnLaunch += (s, e) =>
            {
                SelectedFile = ((LaunchView.LaunchEventArgs)e).Path;
                this.Close();
            };
            launchView5.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage5.Controls.Add(launchView5);

            try
            {
                LoadLaunchViewsAsync();
            }
            catch { }

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

            this.Shown += (s, e) =>
            {
                ((Form1)this.Owner).OnRecentFilesAdded += (sender, eventArgs) =>
                {
                    var recentFile = ((Form1.RecentFilesAddedEventArgs)eventArgs).RecentFile;
                    foreach (var item in listBox1.Items)
                    {
                        if (item.ToString() == recentFile)
                        {
                            listBox1.Items.Remove(item);
                            break;
                        }
                    }
                    listBox1.Items.Insert(0, recentFile);
                };

                listBox1.Focus();
                NativeMethods.SetForegroundWindow(this.Handle);
            };

            this.MouseDown += (s, e) =>
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left) mousePosition = new Point(e.X, e.Y);
            };
            this.MouseMove += (s, e) =>
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    this.Location = new Point(this.Location.X + e.X - mousePosition.X, this.Location.Y + e.Y - mousePosition.Y);
            };

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
            var area = Screen.FromPoint(Cursor.Position).WorkingArea;
            this.Location = new Point(area.X + (area.Width - this.Width) / 2, area.Y + (area.Height - this.Height) / 2);

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

            listBox1.MouseUp += (s, eventArgs) =>
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
            };

            toolStripMenuItem2.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                try
                {
                    SelectedFile = Path.GetDirectoryName(listBox1.SelectedItem.ToString());
                }
                catch { }
                this.Close();
            };

            toolStripMenuItem3.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                var item = listBox1.SelectedItem;

                var result = MessageBox.Show("選択したアイテムを削除しますか？", Program.AppName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) listBox1.Items.Remove(item);
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

        public List<string> GetRecentFiles()
        {
            var list = new List<string>();
            foreach (var item in listBox1.Items) list.Add(item.ToString());
            return list.AsEnumerable().Reverse().ToList();
        }

        private void SaveLaunchViews()
        {
            if (!Directory.Exists(powerCacheOfficeLaunchDataFolder)) Directory.CreateDirectory(powerCacheOfficeLaunchDataFolder);
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView1.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView1.GetLargeImageListAsBase64Strings(), launchView1.GetPaths()), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView2.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView2.GetLargeImageListAsBase64Strings(), launchView2.GetPaths()), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView3.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView3.GetLargeImageListAsBase64Strings(), launchView3.GetPaths()), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView4.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView4.GetLargeImageListAsBase64Strings(), launchView4.GetPaths()), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView5.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView5.GetLargeImageListAsBase64Strings(), launchView5.GetPaths()), jsonSerializerOptions));
            }
            catch { }
        }

        private Task LoadLaunchViewsAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    var path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView1.Name + ".json");
                    var launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                    launchView1.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);

                    path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView2.Name + ".json");
                    launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                    launchView2.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);

                    path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView3.Name + ".json");
                    launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                    launchView3.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);

                    path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView4.Name + ".json");
                    launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                    launchView4.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);

                    path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView5.Name + ".json");
                    launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                    launchView5.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);

                    launchView1.Update();
                    launchView2.Update();
                    launchView3.Update();
                    launchView4.Update();
                    launchView5.Update();
                }
                catch { }
            });
        }
    }
}
