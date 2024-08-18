using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal partial class Form3 : Form
    {
        private static dynamic wScriptShell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
        private static readonly string powerCacheOfficeDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice");
        private static readonly string powerCacheOfficeLaunchDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\launch");

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        private List<string> recentFiles = new List<string>();
        private Point mousePosition = new Point();
        private Form1 mainForm = null;
        private ListBox selectedListBox = null;
        private bool isDarkMode = false;

        private LaunchView launchView1 = null;
        private LaunchView launchView2 = null;
        private LaunchView launchView3 = null;
        private LaunchView launchView4 = null;
        private LaunchView launchView5 = null;

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

        public Form3(bool isDarkMode, Form1 mainForm)
        {
            this.isDarkMode = isDarkMode;
            this.mainForm = mainForm;
            launchView1 = new LaunchView(mainForm, this);
            launchView2 = new LaunchView(mainForm, this);
            launchView3 = new LaunchView(mainForm, this);
            launchView4 = new LaunchView(mainForm, this);
            launchView5 = new LaunchView(mainForm, this);

            if (!Directory.Exists(powerCacheOfficeLaunchDataFolder)) Directory.CreateDirectory(powerCacheOfficeLaunchDataFolder);
            try
            {
                recentFiles = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "recentFiles.json")));
            }
            catch { recentFiles = new List<string>(); }

            InitializeComponent();

            initialWidth = this.Width;
            initialHeight = this.Height;

            float f = NativeMethods.GetDpiForSystem();
            this.Width = (int)Math.Round(initialWidth * (this.DeviceDpi / f));
            this.Height = (int)Math.Round(initialHeight * (this.DeviceDpi / f));

            listBox1.ItemHeight = (int)Math.Round(listBox1.ItemHeight * (f / 96f));
            listBox1.Height = listBox1.ItemHeight * 7;

            listBox2.ItemHeight = (int)Math.Round(listBox2.ItemHeight * (f / 96f));
            listBox2.Height = listBox2.ItemHeight * 7;

            var launchViewInitialWidth = 658;
            var launchViewInitialHeight = 150;
            var launchViewInitialX = 10;
            var launchViewInitialY = 11;

            launchView1.Name = "view1";
            launchView1.Size = new Size(
                (int)Math.Round(launchViewInitialWidth * (f / 96f)), (int)Math.Round(launchViewInitialHeight * (f / 96f)));
            launchView1.Location = new Point(
                (int)Math.Round(launchViewInitialX * (f / 96f)), (int)Math.Round(launchViewInitialY * (f / 96f)));
            launchView1.OnLaunch += (s, e) =>
            {
                mainForm.OpenRecentFile(((LaunchView.LaunchEventArgs)e).Path);
                this.Visible = false;
            };
            launchView1.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage1.Controls.Add(launchView1);

            launchView2.Name = "view2";
            launchView2.Size = new Size(
                (int)Math.Round(launchViewInitialWidth * (f / 96f)), (int)Math.Round(launchViewInitialHeight * (f / 96f)));
            launchView2.Location = new Point(
                (int)Math.Round(launchViewInitialX * (f / 96f)), (int)Math.Round(launchViewInitialY * (f / 96f)));
            launchView2.OnLaunch += (s, e) =>
            {
                mainForm.OpenRecentFile(((LaunchView.LaunchEventArgs)e).Path);
                this.Visible = false;
            };
            launchView2.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage2.Controls.Add(launchView2);

            launchView3.Name = "view3";
            launchView3.Size = new Size(
                (int)Math.Round(launchViewInitialWidth * (f / 96f)), (int)Math.Round(launchViewInitialHeight * (f / 96f)));
            launchView3.Location = new Point(
                (int)Math.Round(launchViewInitialX * (f / 96f)), (int)Math.Round(launchViewInitialY * (f / 96f)));
            launchView3.OnLaunch += (s, e) =>
            {
                mainForm.OpenRecentFile(((LaunchView.LaunchEventArgs)e).Path);
                this.Visible = false;
            };
            launchView3.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage3.Controls.Add(launchView3);

            launchView4.Name = "view4";
            launchView4.Size = new Size(
                (int)Math.Round(launchViewInitialWidth * (f / 96f)), (int)Math.Round(launchViewInitialHeight * (f / 96f)));
            launchView4.Location = new Point(
                (int)Math.Round(launchViewInitialX * (f / 96f)), (int)Math.Round(launchViewInitialY * (f / 96f)));
            launchView4.OnLaunch += (s, e) =>
            {
                mainForm.OpenRecentFile(((LaunchView.LaunchEventArgs)e).Path);
                this.Visible = false;
            };
            launchView4.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage4.Controls.Add(launchView4);

            launchView5.Name = "view5";
            launchView5.Size = new Size(
                (int)Math.Round(launchViewInitialWidth * (f / 96f)), (int)Math.Round(launchViewInitialHeight * (f / 96f)));
            launchView5.Location = new Point(
                (int)Math.Round(launchViewInitialX * (f / 96f)), (int)Math.Round(launchViewInitialY * (f / 96f)));
            launchView5.OnLaunch += (s, e) =>
            {
                mainForm.OpenRecentFile(((LaunchView.LaunchEventArgs)e).Path);
                this.Visible = false;
            };
            launchView5.OnItemChanged += (s, e) => SaveLaunchViews();
            tabPage5.Controls.Add(launchView5);

            LoadLaunchViews();

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
                mainForm.OnRecentFilesAdded += (sender, eventArgs) =>
                {
                    var recentFile = ((Form1.RecentFilesAddedEventArgs)eventArgs).RecentFile;
                    recentFiles.RemoveAll(x => x == recentFile);
                    recentFiles.Add(recentFile);
                    if (recentFiles.Count > 100) recentFiles.RemoveRange(100, recentFiles.Count - 100);
                    try
                    {
                        File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "recentFiles.json"), JsonSerializer.Serialize(recentFiles, jsonSerializerOptions));
                    }
                    catch { }

                    listBox1.BeginUpdate();
                    listBox1.Items.Clear();
                    foreach (var item in recentFiles.AsEnumerable().Reverse())
                    {
                        listBox1.Items.Add(item);
                    }
                    listBox1.EndUpdate();
                };

                listBox1.Focus();
                NativeMethods.SetForegroundWindow(this.Handle);
            };

            this.FormClosing += (s, e) =>
            {
                if (e.CloseReason != CloseReason.UserClosing) return;

                this.Visible = false;
                e.Cancel = true;
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

            InitializeListBox1();
            InitializeListBox2();
            InitializeListView1();

            textBox1.PreviewKeyDown += (s, eventArgs) =>
            {
                if (eventArgs.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        InitializeListBox1();
                        InitializeListBox2();
                        InitializeListView1();
                    }
                    else
                    {
                        InitializeListBox1(textBox1.Text);
                        InitializeListBox2(textBox1.Text);
                        InitializeListView1(textBox1.Text);
                    }
                }
            };
            textBox1.KeyPress += (s, eventArgs) =>
            {
                if (eventArgs.KeyChar == (char)Keys.Enter || eventArgs.KeyChar == (char)Keys.Escape)
                {
                    eventArgs.Handled = true;
                }
            };

            timer1.Start();
        }

        private void InitializeListBox1()
        {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (var item in recentFiles.AsEnumerable().Reverse())
            {
                listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();
        }

        private void InitializeListBox1(string text)
        {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (var item in recentFiles.AsEnumerable().Reverse())
            {
                if (item.ToLower().Contains(text.ToLower())) listBox1.Items.Add(item);
            }
            listBox1.EndUpdate();
        }

        private void InitializeListBox2()
        {
            listBox2.BeginUpdate();
            listBox2.Items.Clear();
            var items = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "*")
                .OrderByDescending(filePath => File.GetLastWriteTime(filePath).Date)
                .ThenByDescending(filePath => File.GetLastWriteTime(filePath).TimeOfDay)
                .Distinct()
                .ToList();
            foreach (var item in items)
            {
                var file = GetSourcePathFromShortcutFile(item);
                if (!string.IsNullOrEmpty(file) && !isOfficeFile(file)) listBox2.Items.Add(file);
            }
            listBox2.EndUpdate();
        }

        private void InitializeListBox2(string text)
        {
            listBox2.BeginUpdate();
            listBox2.Items.Clear();
            var items = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "*")
                .OrderByDescending(filePath => File.GetLastWriteTime(filePath).Date)
                .ThenByDescending(filePath => File.GetLastWriteTime(filePath).TimeOfDay)
                .Distinct()
                .ToList();
            foreach (var item in items)
            {
                var file = GetSourcePathFromShortcutFile(item);
                if (!string.IsNullOrEmpty(file) && !isOfficeFile(file) &&
                    file.ToLower().Contains(text.ToLower())) listBox2.Items.Add(file);
            }
            listBox2.EndUpdate();
        }

        private void InitializeListView1()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            try
            {
                var backupSettings = JsonSerializer.Deserialize<BackupSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json")));
                foreach (var item in backupSettings.BackupRelations)
                {
                    var arr = new string[3] { item.LastWriteTime.ToString(), Path.GetFileName(item.OriginalFilePath), item.OriginalFilePath };
                    var listViewItem = new ListViewItem(arr);
                    listViewItem.Tag = item.BackupFilePath;
                    listView1.Items.Add(listViewItem);
                }
            }
            catch { }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.EndUpdate();
        }

        private void InitializeListView1(string text)
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            try
            {
                var backupSettings = JsonSerializer.Deserialize<BackupSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json")));
                foreach (var item in backupSettings.BackupRelations)
                {
                    if (!item.OriginalFilePath.ToLower().Contains(text.ToLower())) continue;

                    var arr = new string[3] { item.LastWriteTime.ToString(), Path.GetFileName(item.OriginalFilePath), item.OriginalFilePath };
                    var listViewItem = new ListViewItem(arr);
                    listViewItem.Tag = item.BackupFilePath;
                    listView1.Items.Add(listViewItem);
                }
            }
            catch { }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.EndUpdate();
        }

        private bool isOfficeFile(string path)
        {
            var extension = Path.GetExtension(path).ToLower();
            if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods" ||
                extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt" ||
                extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
            {
                return true;
            }
            return false;
        }

        private string GetSourcePathFromShortcutFile(string path)
        {
            try
            {
                return wScriptShell.CreateShortcut(path).TargetPath;
            }
            catch { return string.Empty; }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tabControl1.MouseUp += (s, eventArgs) =>
            {
                if (tabControl1.SelectedTab == null) return;
                var selectedPage = tabControl1.SelectedTab;

                using (var form4 = new Form4(isDarkMode, selectedPage.Text))
                {
                    var result = form4.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        selectedPage.Text = form4.PageName;
                        SaveLaunchViews();
                    }
                }
            };

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
                    selectedListBox = listBox1;
                }
                else
                {
                    mainForm.OpenRecentFile(listBox1.SelectedItem.ToString());
                    this.Visible = false;
                }
            };

            listBox2.MouseMove += (s, eventArgs) =>
            {
                var index = listBox2.IndexFromPoint(eventArgs.Location);
                if (index != ListBox.NoMatches) listBox2.SelectedIndex = index;

                if (listBox2.SelectedItem != null && eventArgs.Button == MouseButtons.Left)
                {
                    string[] files = { listBox2.SelectedItem.ToString() };
                    var dataObject = new DataObject(DataFormats.FileDrop, files);
                    listBox2.DoDragDrop(dataObject, DragDropEffects.Copy);
                };
            };

            listBox2.MouseUp += (s, eventArgs) =>
            {
                if (listBox2.SelectedItems.Count != 1) return;
                if (!listBox2.ClientRectangle.Contains(eventArgs.Location)) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(listBox2.PointToScreen(eventArgs.Location));
                    selectedListBox = listBox2;
                }
                else
                {
                    mainForm.OpenRecentFile(listBox2.SelectedItem.ToString());
                    this.Visible = false;
                }
            };

            listView1.MouseMove += (s, eventArgs) =>
            {
                listView1.Focus();
                var item = listView1.GetItemAt(eventArgs.X, eventArgs.Y);
                if (item != null)
                {
                    item.Focused = true;
                    item.Selected = true;
                }
            };

            listView1.Click += (s, eventArgs) =>
            {
                if (listView1.SelectedItems.Count != 1) return;

                var result = MessageBox.Show("以下のファイルを復元しますか？\n※現在のファイルは上書きされます。\n\n" +
                    listView1.SelectedItems[0].SubItems[2].Text, "", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                try
                {
                    var backupSettings = JsonSerializer.Deserialize<BackupSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json")));
                    var backupRelations = backupSettings.BackupRelations.First(item => item.BackupFilePath == listView1.SelectedItems[0].Tag.ToString());

                    File.Copy(backupRelations.BackupFilePath, backupRelations.OriginalFilePath, true);
                    MessageBox.Show("バックアップからの復元に成功しました。");
                }
                catch { MessageBox.Show("バックアップからの復元に失敗しました。"); }
            };

            listBox1.MouseUp += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                if (!listBox1.ClientRectangle.Contains(eventArgs.Location)) return;

                if (eventArgs.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(listBox1.PointToScreen(eventArgs.Location));
                    selectedListBox = listBox1;
                }
                else
                {
                    mainForm.OpenRecentFile(listBox1.SelectedItem.ToString());
                    this.Visible = false;
                }
            };

            toolStripMenuItem1.Click += (s, eventArgs) =>
            {
                if (selectedListBox == null) return;
                if (selectedListBox.SelectedItems.Count != 1) return;
                Clipboard.SetText(selectedListBox.SelectedItem.ToString());
            };

            toolStripMenuItem2.Click += (s, eventArgs) =>
            {
                if (selectedListBox == null) return;
                if (selectedListBox.SelectedItems.Count != 1) return;

                mainForm.OpenRecentFile(Path.GetDirectoryName(selectedListBox.SelectedItem.ToString()));
                this.Visible = false;
            };

            toolStripMenuItem3.Click += (s, eventArgs) =>
            {
                if (selectedListBox == null) return;
                if (selectedListBox.SelectedItems.Count != 1) return;
                try
                {
                    var path = selectedListBox.SelectedItem.ToString();
                    if (!Directory.Exists(path)) path = Path.GetDirectoryName(path);

                    var folders = Directory.GetDirectories(@path);
                    var files = Directory.GetFiles(@path);
                    var items = new LaunchMenuItem[folders.Length + files.Length];

                    for (int i = 0; i < folders.Length; i++)
                    {
                        items[i] = new LaunchMenuItem(folders[i], mainForm, this);
                    }

                    int num = 0;
                    for (int i = folders.Length; i < folders.Length + files.Length; i++)
                    {
                        items[i] = new LaunchMenuItem(files[num], mainForm, this);
                        num++;
                    }

                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(items);
                    menu.Show(new Point(Cursor.Position.X + 1, Cursor.Position.Y + 1));
                }
                catch { }
            };

            // toolStripMenuItem4.Click += (s, eventArgs) =>
            // {
            //     if (selectedListBox == null) return;
            //     if (selectedListBox.SelectedItems.Count != 1) return;
            //     var item = selectedListBox.SelectedItem;
            // 
            //     var result = MessageBox.Show("選択したアイテムを削除しますか？", Program.AppName, MessageBoxButtons.YesNo);
            //     if (result == DialogResult.Yes) selectedListBox.Items.Remove(item);
            // };
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

        private void SaveLaunchViews()
        {
            if (!Directory.Exists(powerCacheOfficeLaunchDataFolder)) Directory.CreateDirectory(powerCacheOfficeLaunchDataFolder);
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView1.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView1.GetLargeImageListAsBase64Strings(), launchView1.GetPaths(), tabPage1.Text), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView2.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView2.GetLargeImageListAsBase64Strings(), launchView2.GetPaths(), tabPage2.Text), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView3.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView3.GetLargeImageListAsBase64Strings(), launchView3.GetPaths(), tabPage3.Text), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView4.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView4.GetLargeImageListAsBase64Strings(), launchView4.GetPaths(), tabPage4.Text), jsonSerializerOptions));
                File.WriteAllText(Path.Combine(powerCacheOfficeLaunchDataFolder, launchView5.Name + ".json"),
                    JsonSerializer.Serialize(new LaunchSettings(launchView5.GetLargeImageListAsBase64Strings(), launchView5.GetPaths(), tabPage5.Text), jsonSerializerOptions));
            }
            catch { }
        }

        private void LoadLaunchViews()
        {
            try
            {
                var path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView1.Name + ".json");
                var launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                launchView1.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);
                tabPage1.Text = launchSettings.LaunchViewPageName;

                path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView2.Name + ".json");
                launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                launchView2.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);
                tabPage2.Text = launchSettings.LaunchViewPageName;

                path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView3.Name + ".json");
                launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                launchView3.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);
                tabPage3.Text = launchSettings.LaunchViewPageName;

                path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView4.Name + ".json");
                launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                launchView4.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);
                tabPage4.Text = launchSettings.LaunchViewPageName;

                path = Path.Combine(powerCacheOfficeLaunchDataFolder, launchView5.Name + ".json");
                launchSettings = JsonSerializer.Deserialize<LaunchSettings>(File.ReadAllText(path));
                launchView5.SetItems(launchSettings.LaunchViewBase64Images, launchSettings.LaunchViewPaths);
                tabPage5.Text = launchSettings.LaunchViewPageName;
            }
            catch { }
        }

        public void ShowInCenterScreen(bool isDarkMode)
        {
            var area = Screen.FromPoint(Cursor.Position).WorkingArea;
            this.Location = new Point(area.X + (area.Width - this.Width) / 2, area.Y + (area.Height - this.Height) / 2);
            this.WindowState = FormWindowState.Normal;

            Program.ChangeDarkMode(this, isDarkMode);
            if (isDarkMode)
            {
                this.Icon = Properties.Resources.PowerCacheOfficeLaunchWhite;
            }
            else
            {
                this.Icon = Properties.Resources.PowerCacheOfficeLaunchBlack;
            }

            InitializeListBox2();
            textBox1.Focus();
            NativeMethods.SetForegroundWindow(this.Handle);
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                InitializeListBox1();
                InitializeListBox2();
                InitializeListView1();
            }
            else
            {
                InitializeListBox1(textBox1.Text);
                InitializeListBox2(textBox1.Text);
                InitializeListView1(textBox1.Text);
            }
        }
    }
}
