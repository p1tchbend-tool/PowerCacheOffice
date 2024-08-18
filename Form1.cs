using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    internal partial class Form1 : Form
    {
        public event EventHandler OnRecentFilesAdded = delegate { };
        public class RecentFilesAddedEventArgs : EventArgs
        {
            public string RecentFile;
            public RecentFilesAddedEventArgs(string recentFile)
            {
                RecentFile = recentFile;
            }
        }

        private static readonly string powerCacheOfficeDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice");
        private static readonly string powerCacheOfficeCacheFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
        private static readonly string powerCacheOfficeTempFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.temp");
        private static readonly string powerCacheOfficeBackupFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.backup");

        private static readonly int openClipboardPathId = 1;
        private static readonly int openRecentFileId = 2;
        private static HotKey hotKey = new HotKey();
        private static Form3 launchForm = null;

        private static string diffFilePath = string.Empty;
        private AppSettings appSettings = new AppSettings();
        private CacheSettings cacheSettings = new CacheSettings();
        private BackupSettings backupSettings = new BackupSettings();
        private CreateCacheManager createCacheManager = new CreateCacheManager();
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        private HttpClient httpClient = new HttpClient();
        private bool isUpdating = false;
        private bool isCacheCreating = false;

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

        public Form1()
        {
            InitializeComponent();

            initialWidth = this.Width;
            initialHeight = this.Height;

            float f = NativeMethods.GetDpiForSystem();
            this.Width = (int)Math.Round(initialWidth * (this.DeviceDpi / f));
            this.Height = (int)Math.Round(initialHeight * (this.DeviceDpi / f));

            listBox1.ItemHeight = (int)Math.Round(listBox1.ItemHeight * (f / 96f));
            listBox1.Height = listBox1.ItemHeight * 7;

            if (!Directory.Exists(powerCacheOfficeDataFolder)) Directory.CreateDirectory(powerCacheOfficeDataFolder);
            if (!Directory.Exists(powerCacheOfficeCacheFolder)) Directory.CreateDirectory(powerCacheOfficeCacheFolder);
            if (!Directory.Exists(powerCacheOfficeTempFolder)) Directory.CreateDirectory(powerCacheOfficeTempFolder);
            if (!Directory.Exists(powerCacheOfficeBackupFolder)) Directory.CreateDirectory(powerCacheOfficeBackupFolder);

            try
            {
                appSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json")));
            }
            catch { appSettings = new AppSettings(); }

            appSettings.UpdateSettings();
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }

            appSettings.CacheTargetDirectories.ForEach(x => { listBox1.Items.Add(x); });
            checkBox1.Checked = appSettings.IsRelatedExcel;
            checkBox2.Checked = appSettings.IsRelatedWord;
            checkBox3.Checked = appSettings.IsRelatedPowerPoint;
            checkBox4.Checked = appSettings.IsStartUp;
            checkBox5.Checked = appSettings.IsDeleteCacheAtStartUp;
            checkBox6.Checked = appSettings.IsBackup;
            ChangeHotKeyText(textBox4, appSettings.OpenClipboardPathModKey, appSettings.OpenClipboardPathKey);
            ChangeHotKeyText(textBox5, appSettings.OpenRecentFileModKey, appSettings.OpenRecentFileKey);

            launchForm = new Form3(appSettings.IsDarkMode, this);

            hotKey.Add(appSettings.OpenClipboardPathModKey, appSettings.OpenClipboardPathKey, openClipboardPathId);
            hotKey.Add(appSettings.OpenRecentFileModKey, appSettings.OpenRecentFileKey, openRecentFileId);
            hotKey.OnHotKey += (s, eventArgs) =>
            {
                if (((HotKey.HotKeyEventArgs)eventArgs).Id == openClipboardPathId)
                {
                    OpenClipboardPath();
                }
                else if (((HotKey.HotKeyEventArgs)eventArgs).Id == openRecentFileId)
                {
                    if (launchForm == null || launchForm.IsDisposed) launchForm = new Form3(appSettings.IsDarkMode, this);
                    launchForm.ShowInCenterScreen(appSettings.IsDarkMode);
                }
            };

            try
            {
                cacheSettings = JsonSerializer.Deserialize<CacheSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json")));
            }
            catch { cacheSettings = new CacheSettings(); }

            try
            {
                backupSettings = JsonSerializer.Deserialize<BackupSettings>(File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json")));
            }
            catch { backupSettings = new BackupSettings(); }

            if (File.Exists(Path.Combine(powerCacheOfficeDataFolder, ".createdCacheList.txt")))
            {
                if (!checkBox5.Checked)
                {
                    MargeCreatedCacheToCacheSettings();
                }
                else
                {
                    try
                    {
                        File.Delete(Path.Combine(powerCacheOfficeDataFolder, ".createdCacheList.txt"));
                    }
                    catch { }
                }
            }

            if (checkBox5.Checked) DeleteAllCache();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DeleteSubFolder(powerCacheOfficeTempFolder);

            label7.Click += (s, eventArgs) =>
            {
                appSettings.IsDarkMode = !appSettings.IsDarkMode;
                ChangeDarkModeForm1(appSettings.IsDarkMode);
                try
                {
                    File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                }
                catch { }
            };
            panel4.Click += (s, eventArgs) =>
            {
                appSettings.IsDarkMode = !appSettings.IsDarkMode;
                ChangeDarkModeForm1(appSettings.IsDarkMode);
                try
                {
                    File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                }
                catch { }
            };

            label8.Click += (s, eventArgs) => CheckUpdateAsync();
            panel5.Click += (s, eventArgs) => CheckUpdateAsync();

            fileSystemWatcher1.Path = powerCacheOfficeCacheFolder;
            fileSystemWatcher1.Error += (s, eventArgs) =>
            {
                MessageBox.Show("キャッシュの変更の監視を継続できなくなりました。\nアプリケーションを再起動します。", Program.AppName);
                Application.Restart();
            };
            fileSystemWatcher1.Renamed += async (s, eventArgs) =>
            {
                var fullPath = eventArgs.FullPath;
                var cacheRelation = cacheSettings.CacheRelations.FirstOrDefault(x => x.LocalPath == fullPath);
                if (cacheRelation == null) return;

                if (!IsNearlyEqualDateTime(cacheRelation.RemoteLastWriteTime, File.GetLastWriteTime(cacheRelation.RemotePath)))
                {
                    using (var form2 = new Form2(cacheRelation.RemotePath, appSettings.IsDarkMode))
                    {
                        form2.ShowDialog();
                        if (form2.Result == Form2.No)
                        {
                            return;
                        }
                        else if (form2.Result == Form2.Confirm)
                        {
                            try
                            {
                                if (!Directory.Exists(powerCacheOfficeTempFolder)) Directory.CreateDirectory(powerCacheOfficeTempFolder);

                                var tempFile = Path.Combine(powerCacheOfficeTempFolder, "【差分確認】" + Path.GetFileName(cacheRelation.RemotePath));
                                File.Copy(cacheRelation.RemotePath, tempFile, true);

                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.UseShellExecute = true;
                                psi.Arguments = $@"""{tempFile}""";

                                var extension = Path.GetExtension(tempFile).ToLower();
                                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
                                {
                                    psi.FileName = appSettings.ExcelPath;
                                }
                                else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
                                {
                                    psi.FileName = appSettings.WordPath;
                                }
                                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
                                {
                                    psi.FileName = appSettings.PowerPointPath;
                                }

                                var process = Process.Start(psi);
                                SetProcessWindowAsForeground(process);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }

                            return;
                        }
                        else if (form2.Result == Form2.ConfirmDiffTool)
                        {
                            try
                            {
                                if (!Directory.Exists(powerCacheOfficeTempFolder)) Directory.CreateDirectory(powerCacheOfficeTempFolder);

                                var tempFileRemote = Path.Combine(powerCacheOfficeTempFolder, "【リモート】" + Path.GetFileName(cacheRelation.RemotePath));
                                File.Copy(cacheRelation.RemotePath, tempFileRemote, true);

                                await Task.Delay(1000); // 更新検知から実際に更新されるまで少し待つ

                                var tempFileLocal = Path.Combine(powerCacheOfficeTempFolder, "【ローカル】" + Path.GetFileName(cacheRelation.LocalPath));
                                File.Copy(cacheRelation.LocalPath, tempFileLocal, true);

                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.UseShellExecute = true;
                                psi.Arguments = $@"""{tempFileLocal}"" ""{tempFileRemote}""";

                                var extension = Path.GetExtension(tempFileLocal).ToLower();
                                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
                                {
                                    psi.FileName = appSettings.ExcelDiffToolPath;
                                    psi.Arguments += " " + appSettings.ExcelDiffToolArguments;
                                }
                                else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
                                {
                                    psi.FileName = appSettings.WordDiffToolPath;
                                    psi.Arguments += " " + appSettings.WordDiffToolArguments;
                                }
                                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
                                {
                                    psi.FileName = appSettings.PowerPointDiffToolPath;
                                    psi.Arguments += " " + appSettings.PowerPointDiffToolArguments;
                                }

                                var process = Process.Start(psi);
                                SetProcessWindowAsForeground(process);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }

                            return;
                        }
                    }
                }

                await Task.Delay(3000); // 更新検知から実際に更新されるまで少し待つ
                try
                {
                    Program.CopyFileAndAttributes(cacheRelation.LocalPath, cacheRelation.RemotePath);
                    cacheSettings.CacheRelations.Remove(cacheRelation);
                    cacheSettings.CacheRelations.Add(new CacheRelation(cacheRelation.RemotePath, cacheRelation.LocalPath, File.GetLastWriteTime(cacheRelation.RemotePath)));
                    try
                    {
                        File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                    }
                    catch { }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
            };

            listBox1.KeyDown += (s, eventArgs) =>
            {
                if (eventArgs.KeyData == Keys.Delete) RemoveSelectedItemFromListBox1();
            };

            listBox1.MouseDown += (s, eventArgs) =>
            {
                if (eventArgs.Button != MouseButtons.Right) return;
                if (listBox1.SelectedItems.Count != 1) return;

                contextMenuStrip2.Show(listBox1.PointToScreen(eventArgs.Location));
            };
            toolStripMenuItem5.Click += (s, eventArgs) =>
            {
                if (listBox1.SelectedItems.Count != 1) return;
                Clipboard.SetText(listBox1.SelectedItem.ToString());
            };
            toolStripMenuItem6.Click += (s, eventArgs) => RemoveSelectedItemFromListBox1();

            this.FormClosing += (s, eventArgs) =>
            {
                if (eventArgs.CloseReason != CloseReason.UserClosing) return;

                var dr = MessageBox.Show(
                    "終了してよろしいですか？\n\n※ローカルの更新はリモートに反映されなくなります。\n※最小化するとタスクトレイに常駐して処理を継続します。", Program.AppName, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) eventArgs.Cancel = true;
            };

            this.Shown += (s, eventArgs) =>
            {
                Program.SortTabIndex(this);
                ChangeDarkModeForm1(appSettings.IsDarkMode);
                DisableForm1();
            };

            this.SizeChanged += async (s, eventArgs) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    await Task.Delay(500);
                    DisableForm1();
                }
            };

            notifyIcon1.MouseDown += (s, eventArgs) =>
            {
                if (eventArgs.Button == MouseButtons.Right) return;
                EnableForm1();
            };

            toolStripMenuItem1.Click += (s, eventArgs) => EnableForm1();
            toolStripMenuItem7.Click += (s, eventArgs) =>
            {
                if (launchForm == null || launchForm.IsDisposed) launchForm = new Form3(appSettings.IsDarkMode, this);
                launchForm.ShowInCenterScreen(appSettings.IsDarkMode);
            };
            toolStripMenuItem2.Click += (s, eventArgs) =>
            {
                var result = MessageBox.Show("再起動しますか？", Program.AppName, MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                Application.Restart();
            };
            toolStripMenuItem3.Click += (s, eventArgs) => this.Close();
            toolStripMenuItem4.Click += (s, eventArgs) => CheckUpdateAsync();

            textBox4.Enter += (s, eventArgs) => label6.Text = "登録するにはキーを押してください。";
            textBox4.Leave += (s, eventArgs) => label6.Text = "クリップボードのパスを開く";
            textBox4.KeyDown += (s, eventArgs) => RegisterHotKey(eventArgs.KeyCode, openClipboardPathId, textBox4, label6);

            textBox5.Enter += (s, eventArgs) => label5.Text = "登録するにはキーを押してください。";
            textBox5.Leave += (s, eventArgs) => label5.Text = "最近開いたファイルを表示";
            textBox5.KeyDown += (s, eventArgs) => RegisterHotKey(eventArgs.KeyCode, openRecentFileId, textBox5, label5);

            timer1.Start();
        }

        private async void CheckUpdateAsync()
        {
            if (isUpdating) return;
            isUpdating = true;
            try
            {
                var version = string.Empty;
                try
                {
                    version = await httpClient.GetStringAsync("https://raw.githubusercontent.com/p1tchbend-tool/PowerCacheOffice/master/App/VERSION.txt");
                }
                catch { }

                if (string.IsNullOrEmpty(version))
                {
                    MessageBox.Show("アップデートの確認に失敗しました。\n時間をおいて安定した通信環境でもう一度お試しください。", Program.AppName);
                }
                else
                {
                    if (version == FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion)
                    {
                        MessageBox.Show("バージョン  " + FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion + "\n\n最新のバージョンです。", Program.AppName);
                    }
                    else
                    {
                        try
                        {
                            var result = MessageBox.Show("新しいバージョンがあります。\n\nダウンロードしますか？", Program.AppName, MessageBoxButtons.YesNo);
                            if (result != DialogResult.Yes) return;

                            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/p1tchbend-tool/PowerCacheOffice/master/App/PowerCacheOfficeSetup.msi");
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            {
                                using (var outStream = File.Create(
                                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Downloads\PowerCacheOfficeSetup.msi")))
                                {
                                    stream.CopyTo(outStream);
                                }
                            }

                            var psi = new ProcessStartInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
                            psi.UseShellExecute = true;
                            var process = Process.Start(psi);
                            SetProcessWindowAsForeground(process);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
                    }
                }
            }
            finally { isUpdating = false; }
        }

        private void ChangeDarkModeForm1(bool enabled)
        {
            if (enabled)
            {
                label7.Text = " Light";
                panel4.BackgroundImage = Properties.Resources.sun;
                panel5.BackgroundImage = Properties.Resources.update_white;
            }
            else
            {
                label7.Text = " Dark";
                panel4.BackgroundImage = Properties.Resources.moon;
                panel5.BackgroundImage = Properties.Resources.update;
            }

            Program.ChangeDarkMode(this, enabled);
            label4.BackColor = Color.FromArgb(230, 230, 230);
            label4.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void RemoveSelectedItemFromListBox1()
        {
            if (listBox1.SelectedItems.Count != 1) return;
            var dr = MessageBox.Show(
                "選択したアイテムを削除してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            listBox1.Items.Remove(listBox1.SelectedItem);

            appSettings.CacheTargetDirectories = listBox1.Items.OfType<string>().ToList();
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void RegisterHotKey(Keys key, int id, TextBox textBox, Label label)
        {
            int modkey = HotKey.MOD_KEY_NONE;
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) modkey += HotKey.MOD_KEY_CONTROL;
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) modkey += HotKey.MOD_KEY_ALT;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) modkey += HotKey.MOD_KEY_SHIFT;

            if (modkey == HotKey.MOD_KEY_NONE)
            {
                label.Text = "先に修飾子キーを押してください。";
            }
            else if (key == Keys.ControlKey || key == Keys.Menu || key == Keys.ShiftKey)
            {
                label.Text = "修飾子以外のキーも押してください。";
            }
            else
            {
                ChangeHotKeyText(textBox, modkey, key);

                if (hotKey.Add(modkey, key, id))
                {
                    hotKey.Remove(id);
                    hotKey.Add(modkey, key, id);
                    label.Text = "登録に成功しました。";

                    if (id == openClipboardPathId)
                    {
                        appSettings.OpenClipboardPathModKey = modkey;
                        appSettings.OpenClipboardPathKey = key;
                        try
                        {
                            File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                        }
                        catch { }
                    }
                    else if (id == openRecentFileId)
                    {
                        appSettings.OpenRecentFileModKey = modkey;
                        appSettings.OpenRecentFileKey = key;
                        try
                        {
                            File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                        }
                        catch { }
                    }
                }
                else
                {
                    label.Text = "登録に失敗しました。";
                }
            }
        }

        public void OpenFile(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }
            else if (args[0] == "--check")
            {
                CheckFile(args);
                return;
            }
            else if (args[0] == "--diff")
            {
                DiffFile(args);
                return;
            }

            foreach (var filePath in args)
            {
                var extension = Path.GetExtension(filePath).ToLower();
                if (extension != ".xls" && extension != ".xlsx" && extension != ".xlsm" && extension != ".ods" &&
                    extension != ".doc" && extension != ".docx" && extension != ".docm" && extension != ".odt" &&
                    extension != ".ppt" && extension != ".pptx" && extension != ".pptm" && extension != ".odp") continue;

                OnRecentFilesAdded(this, new RecentFilesAddedEventArgs(filePath));

                string arguments = string.Empty;
                if (listBox1.Items.OfType<string>().Any(x => filePath.ToLower().StartsWith(x.ToLower())))
                {
                    // キャッシュ対象の場合
                    var cacheRelation = cacheSettings.CacheRelations.FirstOrDefault(x => x.RemotePath == filePath);
                    if (cacheRelation == null)
                    {
                        // キャッシュ済みでない場合
                        var itemCacheFolder = Path.Combine(powerCacheOfficeCacheFolder, Guid.NewGuid().ToString());
                        if (!Directory.Exists(itemCacheFolder)) Directory.CreateDirectory(itemCacheFolder);

                        var cacheFile = Path.Combine(itemCacheFolder, Path.GetFileName(filePath));
                        try
                        {
                            Program.CopyFileAndAttributes(filePath, cacheFile);
                            cacheSettings.CacheRelations.Add(new CacheRelation(filePath, cacheFile, File.GetLastWriteTime(filePath)));
                            try
                            {
                                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                            }
                            catch { }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }

                        arguments = cacheFile;
                    }
                    else
                    {
                        // キャッシュ済みの場合
                        var lastWriteTime = File.GetLastWriteTime(filePath);
                        if (!IsNearlyEqualDateTime(cacheRelation.RemoteLastWriteTime, lastWriteTime))
                        {
                            var localPath = cacheRelation.LocalPath;
                            try
                            {
                                BackupFile(localPath);  // ローカルキャッシュを上書き前にバックアップ

                                Program.CopyFileAndAttributes(filePath, localPath);
                                cacheSettings.CacheRelations.Remove(cacheRelation);
                                cacheSettings.CacheRelations.Add(new CacheRelation(filePath, localPath, lastWriteTime));
                                try
                                {
                                    File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                                }
                                catch { }
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }

                            arguments = localPath;
                        }
                        else
                        {
                            arguments = cacheRelation.LocalPath;
                        }
                    }
                }
                else
                {
                    // キャッシュ対象でない場合
                    arguments = filePath;
                }

                BackupFile(arguments);  // ローカルキャッシュとは別にバックアップを保持

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.Arguments = $@"""{arguments}""";

                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
                {
                    psi.FileName = appSettings.ExcelPath;
                }
                else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
                {
                    psi.FileName = appSettings.WordPath;
                }
                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
                {
                    psi.FileName = appSettings.PowerPointPath;
                }

                try
                {
                    var process = Process.Start(psi);
                    SetProcessWindowAsForeground(process);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
            }
        }

        private void CheckFile(string[] args)
        {
            if (args.Length != 2 || string.IsNullOrEmpty(args[1])) return;

            var cacheRelation = cacheSettings.CacheRelations.FirstOrDefault(x => x.RemotePath == args[1]);
            if (cacheRelation != null)
            {
                MessageBox.Show("ファイルはキャッシュされています。\n\n" +
                    "リモートのパス:\n" + cacheRelation.RemotePath + "\n\n" +
                    "ローカルのパス:\n" + cacheRelation.LocalPath, Program.AppName);
                return;
            }

            cacheRelation = cacheSettings.CacheRelations.FirstOrDefault(x => x.LocalPath == args[1]);
            if (cacheRelation != null)
            {
                MessageBox.Show("ファイルはキャッシュされています。\n\n" +
                    "リモートのパス:\n" + cacheRelation.RemotePath + "\n\n" +
                    "ローカルのパス:\n" + cacheRelation.LocalPath, Program.AppName);
                return;
            }

            MessageBox.Show("ファイルはキャッシュされていません。", Program.AppName);
        }

        private async void DiffFile(string[] args)
        {
            if (args.Length != 2 || string.IsNullOrEmpty(args[1]) || args[1] == diffFilePath) return;

            if (string.IsNullOrEmpty(diffFilePath))
            {
                diffFilePath = args[1];
                await Task.Delay(500);
                diffFilePath = string.Empty;
                return;
            }

            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.Arguments = $@"""{diffFilePath}"" ""{args[1]}""";

            var extension = Path.GetExtension(args[1]).ToLower();
            if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
            {
                psi.FileName = appSettings.ExcelDiffToolPath;
            }
            else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
            {
                psi.FileName = appSettings.WordDiffToolPath;
            }
            else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
            {
                psi.FileName = appSettings.PowerPointDiffToolPath;
            }

            try
            {
                var process = Process.Start(psi);
                SetProcessWindowAsForeground(process);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            foreach (var item in listBox1.Items)
                if (textBox1.Text.ToLower().StartsWith(item.ToString().ToLower())) return;

            listBox1.Items.Add(textBox1.Text);

            appSettings.CacheTargetDirectories = listBox1.Items.OfType<string>().ToList();
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        // private void button2_Click(object sender, EventArgs e)
        // {
        //     SearchFolderInExplorer(powerCacheOfficeCacheFolder, textBox2.Text);
        // }

        // private void button7_Click(object sender, EventArgs e)
        // {
        //     SearchFolderInExplorer(powerCacheOfficeBackupFolder, textBox2.Text);
        // }

        private void SearchFolderInExplorer(string folder, string keyword)
        {
            var queryString = $@"search-ms:query={keyword} NOT kind:folder&crumb=location:{folder}";

            var psi = new ProcessStartInfo(queryString);
            psi.UseShellExecute = true;
            try
            {
                var process = Process.Start(psi);
                SetProcessWindowAsForeground(process);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                RegisterFileType("PowerCacheOffice.x", "x.ico", ".xls", ".xlsx", ".xlsm", ".ods");
            }
            else
            {
                UnregisterFileType("PowerCacheOffice.x");
            }

            appSettings.IsRelatedExcel = checkBox1.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                RegisterFileType("PowerCacheOffice.d", "d.ico", ".doc", ".docx", ".docm", ".odt");
            }
            else
            {
                UnregisterFileType("PowerCacheOffice.d");
            }

            appSettings.IsRelatedWord = checkBox2.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                RegisterFileType("PowerCacheOffice.p", "p.ico", ".ppt", ".pptx", ".pptm", ".odp");
            }
            else
            {
                UnregisterFileType("PowerCacheOffice.p");
            }

            appSettings.IsRelatedPowerPoint = checkBox3.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void RegisterFileType(string fileType, string iconName, params string[] extensions)
        {
            var currentUserKey = Microsoft.Win32.Registry.CurrentUser;

            var iconKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\DefaultIcon");
            iconKey.SetValue("", $@"""{Application.StartupPath}\{iconName}"",0");
            iconKey.Close();

            var openCmdKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\open\command");
            openCmdKey.SetValue("", $@"""{Application.ExecutablePath}"" ""%1""");
            openCmdKey.Close();

            var menuKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\menu");
            menuKey.SetValue("MUIVerb", "Power Cache Office");
            menuKey.SetValue("SubCommands", "");
            menuKey.Close();

            var checkKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\menu\shell\check");
            checkKey.SetValue("", "キャッシュ状況を確認");
            checkKey.Close();

            var checkCmdKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\menu\shell\check\command");
            checkCmdKey.SetValue("", $@"""{Application.ExecutablePath}"" --check ""%1""");
            checkCmdKey.Close();

            var diffKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\menu\shell\diff");
            diffKey.SetValue("", "差分比較");
            diffKey.Close();

            var diffCmdKey = currentUserKey.CreateSubKey($@"Software\Classes\{fileType}\shell\menu\shell\diff\command");
            diffCmdKey.SetValue("", $@"""{Application.ExecutablePath}"" --diff ""%1""");
            diffCmdKey.Close();

            foreach (var extension in extensions)
            {
                var regkey = currentUserKey.CreateSubKey($@"Software\Classes\{extension}");
                regkey.SetValue("", fileType);
                regkey.Close();
            }
        }

        private void UnregisterFileType(string fileType)
        {
            var currentUserKey = Microsoft.Win32.Registry.CurrentUser;
            currentUserKey.DeleteSubKeyTree($@"Software\Classes\{fileType}");
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            var startUpKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (checkBox4.Checked)
            {
                startUpKey.SetValue(Application.ProductName, $@"""{Application.ExecutablePath}""");
            }
            else
            {
                startUpKey.DeleteValue(Application.ProductName, false);
            }

            appSettings.IsStartUp = checkBox4.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            appSettings.IsDeleteCacheAtStartUp = checkBox5.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            appSettings.IsBackup = checkBox6.Checked;
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                "すべてのキャッシュを削除してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            DeleteAllCache();
            MessageBox.Show("キャッシュの削除が完了しました。", Program.AppName);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                "すべてのバックアップを削除してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            DeleteAllBackup();
            MessageBox.Show("バックアップの削除が完了しました。", Program.AppName);
        }

        private void DeleteAllCache()
        {
            DeleteSubFolder(powerCacheOfficeCacheFolder);

            cacheSettings = new CacheSettings();
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void DeleteAllBackup()
        {
            DeleteSubFolder(powerCacheOfficeBackupFolder);

            backupSettings = new BackupSettings();
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json"), JsonSerializer.Serialize(backupSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void DeleteSubFolder(string path)
        {
            var di = new DirectoryInfo(path);
            DeleteReadonlyAttribute(di);

            foreach (var file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch { }
            }
            foreach (var folder in di.GetDirectories())
            {
                try
                {
                    folder.Delete(true);
                }
                catch { }
            }
        }

        private void DeleteReadonlyAttribute(DirectoryInfo di)
        {
            try
            {
                if ((di.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) di.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in di.GetFiles())
                {
                    try
                    {
                        if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) fi.Attributes = FileAttributes.Normal;
                    }
                    catch { }
                }
            }
            catch { }
            
            foreach (DirectoryInfo d in di.GetDirectories()) DeleteReadonlyAttribute(d);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cacheSettings.CacheRelations.RemoveAll(x => !File.Exists(x.LocalPath));
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
            }
            catch { }

            backupSettings.BackupRelations.RemoveAll(x => !File.Exists(x.BackupFilePath));
            try
            {
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json"), JsonSerializer.Serialize(backupSettings, jsonSerializerOptions));
            }
            catch { }

            MessageBox.Show("インデックスの再作成が完了しました。", Program.AppName);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("対象フォルダを入力してください。", Program.AppName);
                return;
            }
            if (!Directory.Exists(textBox3.Text))
            {
                MessageBox.Show("対象フォルダが存在しません。", Program.AppName);
                return;
            }

            var dr = MessageBox.Show(
                @"対象フォルダからローカルキャッシュを作成します。

※処理に時間がかかる可能性があります。
※大量のファイルをキャッシュすると、PCの容量が不足する可能性があります。

実行してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            if (isCacheCreating) return;
            isCacheCreating = true;
            try
            {
                if (!listBox1.Items.OfType<string>().Any(x => textBox3.Text.ToLower().StartsWith(x.ToLower())))
                {
                    listBox1.Items.Add(textBox3.Text);
                    appSettings.CacheTargetDirectories = listBox1.Items.OfType<string>().ToList();
                    try
                    {
                        File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                    }
                    catch { }
                }

                button3.Enabled = false;
                textBox3.Enabled = false;
                listBox1.Focus();
                try
                {
                    var caches = new List<string>();
                    cacheSettings.CacheRelations.ForEach(x => caches.Add(x.RemotePath));

                    createCacheManager = new CreateCacheManager();

                    label4.Visible = true;
                    await createCacheManager.CountCacheTargetAsync(textBox3.Text);
                    label4.Visible = false;
                    await createCacheManager.CreateCacheAsync(textBox3.Text, caches);

                    MargeCreatedCacheToCacheSettings();
                }
                finally
                {
                    textBox3.Enabled = true;
                    button3.Enabled = true;
                    label4.Visible = false;
                    createCacheManager = new CreateCacheManager();
                }
            }
            finally { isCacheCreating = false; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = createCacheManager.CacheTargetCount;
            progressBar1.Value = createCacheManager.CreatedCacheCount;

            if (isUpdating)
            {
                label8.BackColor = Color.FromArgb(128, 128, 128);
                panel5.BackColor = Color.FromArgb(128, 128, 128);
            }
            else
            {
                ChangeDarkModeForm1(appSettings.IsDarkMode);
            }
        }

        private void MargeCreatedCacheToCacheSettings()
        {
            try
            {
                var text = File.ReadAllText(Path.Combine(powerCacheOfficeDataFolder, ".createdCacheList.txt"));
                var createdCacheList = text.Split('\n');

                foreach (var item in createdCacheList)
                {
                    if (string.IsNullOrEmpty(item)) continue;

                    var createdCache = item.Split('\t');
                    if (cacheSettings.CacheRelations.Any(x => x.RemotePath == createdCache[0])) continue;

                    cacheSettings.CacheRelations.Add(new CacheRelation(createdCache[0], createdCache[1], DateTime.Parse(createdCache[2])));
                }

                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                File.Delete(Path.Combine(powerCacheOfficeDataFolder, ".createdCacheList.txt"));
            }
            catch { }
        }

        private void EnableForm1()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            listBox1.Focus();
            this.ShowInTaskbar = true;
        }

        private void DisableForm1()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private bool IsNearlyEqualDateTime(DateTime dateTime1, DateTime dateTime2)
        {
            var dt1 = dateTime1.AddTicks(-(dateTime1.Ticks % TimeSpan.TicksPerMinute));
            var dt2 = dateTime2.AddTicks(-(dateTime2.Ticks % TimeSpan.TicksPerMinute));
            return dt1 == dt2;
        }

        // private void button6_Click(object sender, EventArgs e)
        // {
        //     if (launchForm == null || launchForm.IsDisposed) launchForm = new Form3(appSettings.IsDarkMode, this);
        //     launchForm.ShowInCenterScreen(appSettings.IsDarkMode);
        // }

        public void OpenRecentFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            var extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
            {
                psi.FileName = appSettings.ExcelPath;
                psi.Arguments = $@"""{filePath}""";
                BackupFile(filePath);
            }
            else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
            {
                psi.FileName = appSettings.WordPath;
                psi.Arguments = $@"""{filePath}""";
                BackupFile(filePath);
            }
            else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
            {
                psi.FileName = appSettings.PowerPointPath;
                psi.Arguments = $@"""{filePath}""";
                BackupFile(filePath);
            }
            else
            {
                psi.FileName = filePath;
            }

            try
            {
                var process = Process.Start(psi);
                SetProcessWindowAsForeground(process);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }
        }

        private void OpenClipboardPath()
        {
            try
            {
                var text = Clipboard.GetText();
                if (string.IsNullOrEmpty(text)) return;

                text = text.Replace(@"""", "");
                if (!File.Exists(text) && !Directory.Exists(text)) return;

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                var extension = Path.GetExtension(text).ToLower();

                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm" || extension == ".ods")
                {
                    psi.FileName = appSettings.ExcelPath;
                    psi.Arguments = $@"""{text}""";
                    BackupFile(text);
                }
                else if (extension == ".doc" || extension == ".docx" || extension == ".docm" || extension == ".odt")
                {
                    psi.FileName = appSettings.WordPath;
                    psi.Arguments = $@"""{text}""";
                    BackupFile(text);
                }
                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm" || extension == ".odp")
                {
                    psi.FileName = appSettings.PowerPointPath;
                    psi.Arguments = $@"""{text}""";
                    BackupFile(text);
                }
                else
                {
                    psi.FileName = text;
                }

                try
                {
                    var process = Process.Start(psi);
                    SetProcessWindowAsForeground(process);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Program.AppName); }

                OnRecentFilesAdded(this, new RecentFilesAddedEventArgs(text));
            }
            catch { }
        }

        private void ChangeHotKeyText(TextBox textBox, int modkey, Keys key)
        {
            textBox.Text = string.Empty;

            if ((modkey & HotKey.MOD_KEY_CONTROL) == HotKey.MOD_KEY_CONTROL) textBox.Text = "Control + ";
            if ((modkey & HotKey.MOD_KEY_ALT) == HotKey.MOD_KEY_ALT) textBox.Text += "Alt + ";
            if ((modkey & HotKey.MOD_KEY_SHIFT) == HotKey.MOD_KEY_SHIFT) textBox.Text += "Shift + ";

            var kc = new KeysConverter();
            textBox.Text += kc.ConvertToString(key);
            textBox.Text = " " + textBox.Text;
        }

        private void BackupFile(string filePath)
        {
            if (!appSettings.IsBackup) return;
            try
            {
                var extension = Path.GetExtension(filePath).ToLower();
                if (extension != ".xls" && extension != ".xlsx" && extension != ".xlsm" && extension != ".ods" &&
                    extension != ".doc" && extension != ".docx" && extension != ".docm" && extension != ".odt" &&
                    extension != ".ppt" && extension != ".pptx" && extension != ".pptm" && extension != ".odp") return;

                var lastWriteTime = File.GetLastWriteTime(filePath);
                if (backupSettings.BackupRelations
                    .Where(x => x.OriginalFilePath == filePath)
                    .Any(x => IsNearlyEqualDateTime(x.LastWriteTime, lastWriteTime))) return;

                var backupFilePath = Path.Combine(powerCacheOfficeBackupFolder,
                    Path.GetFileNameWithoutExtension(filePath) + "-" + lastWriteTime.ToString("yyyyMMddHHmm") + "-" + Guid.NewGuid().ToString("N") + extension);
                Program.CopyFileAndAttributes(filePath, backupFilePath);

                backupSettings.BackupRelations.Add(new BackupRelation(filePath, backupFilePath, lastWriteTime));
                File.WriteAllText(Path.Combine(powerCacheOfficeDataFolder, "backupSettings.json"), JsonSerializer.Serialize(backupSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private async void SetProcessWindowAsForeground(Process process)
        {
            try
            {
                IntPtr hWnd = IntPtr.Zero;
                for (int i = 0; i < 10; i++)
                {
                    if (process != null) hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero) break;

                    await Task.Delay(500);
                }

                if (hWnd != IntPtr.Zero)
                {
                    NativeMethods.ShowWindow(hWnd, NativeMethods.SW_RESTORE);
                    NativeMethods.SetForegroundWindow(hWnd);
                }
            }
            catch { }
        }
    }
}
