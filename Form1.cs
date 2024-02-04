using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public partial class Form1 : Form
    {
        private AppSettings appSettings = new AppSettings();
        private CacheSettings cacheSettings = new CacheSettings();
        private CreateCacheManager createCacheManager = new CreateCacheManager();
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        public Form1()
        {
            InitializeComponent();

            try
            {
                appSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(Path.Combine(Application.StartupPath, "appSettings.json")));
            }
            catch { appSettings = new AppSettings(); }

            appSettings.UpdateSettings();
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }

            appSettings.CacheTargetDirectories.ForEach(x => { listBox1.Items.Add(x); });
            checkBox1.Checked = appSettings.IsRelatedExcel;
            checkBox2.Checked = appSettings.IsRelatedWord;
            checkBox3.Checked = appSettings.IsRelatedPowerPoint;

            try
            {
                cacheSettings = JsonSerializer.Deserialize<CacheSettings>(File.ReadAllText(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json")));
            }
            catch { cacheSettings = new CacheSettings(); }

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.createdCacheList.txt")))
            {
                MargeCreatedCacheToCacheSettings();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.temp");
            try
            {
                DeleteReadonlyAttribute(new DirectoryInfo(tempFolder));
            }
            catch { }
            try
            {
                Directory.Delete(tempFolder, true);
            }
            catch { }
            if (!Directory.Exists(tempFolder)) Directory.CreateDirectory(tempFolder);

            var cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
            if (!Directory.Exists(cacheFolder)) Directory.CreateDirectory(cacheFolder);

            fileSystemWatcher1.Path = cacheFolder;
            fileSystemWatcher1.Error += (s, eventArgs) =>
            {
                EnableForm1();
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
                    using (var form2 = new Form2(cacheRelation.RemotePath))
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
                                var tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.temp");
                                if (!Directory.Exists(tempDirectory)) Directory.CreateDirectory(tempDirectory);

                                var tempFile = Path.Combine(tempDirectory, "【差分確認】" + Path.GetFileName(cacheRelation.RemotePath));
                                File.Copy(cacheRelation.RemotePath, tempFile, true);

                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.UseShellExecute = true;
                                psi.Arguments = $@"""{tempFile}""";

                                var extension = Path.GetExtension(tempFile).ToLower();
                                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm")
                                {
                                    psi.FileName = appSettings.ExcelPath;
                                }
                                else if (extension == ".doc" || extension == ".docx" || extension == ".docm")
                                {
                                    psi.FileName = appSettings.WordPath;
                                }
                                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm")
                                {
                                    psi.FileName = appSettings.PowerPointPath;
                                }
                                Process.Start(psi);
                            }
                            catch (Exception ex)
                            {
                                EnableForm1();
                                MessageBox.Show(ex.Message, Program.AppName);
                            }

                            return;
                        }
                        else if (form2.Result == Form2.ConfirmDiffTool)
                        {
                            try
                            {
                                var tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.temp");
                                if (!Directory.Exists(tempDirectory)) Directory.CreateDirectory(tempDirectory);

                                var tempFileRemote = Path.Combine(tempDirectory, "【リモート】" + Path.GetFileName(cacheRelation.RemotePath));
                                File.Copy(cacheRelation.RemotePath, tempFileRemote, true);

                                await Task.Delay(1000); // 更新検知から実際に更新されるまで少し待つ

                                var tempFileLocal = Path.Combine(tempDirectory, "【ローカル】" + Path.GetFileName(cacheRelation.LocalPath));
                                File.Copy(cacheRelation.LocalPath, tempFileLocal, true);

                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.UseShellExecute = true;
                                psi.Arguments = $@"""{tempFileLocal}"" ""{tempFileRemote}""";

                                var extension = Path.GetExtension(tempFileLocal).ToLower();
                                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm")
                                {
                                    psi.FileName = appSettings.ExcelDiffToolPath;
                                    psi.Arguments += " " + appSettings.ExcelDiffToolArguments;
                                }
                                else if (extension == ".doc" || extension == ".docx" || extension == ".docm")
                                {
                                    psi.FileName = appSettings.WordDiffToolPath;
                                    psi.Arguments += " " + appSettings.WordDiffToolArguments;
                                }
                                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm")
                                {
                                    psi.FileName = appSettings.PowerPointDiffToolPath;
                                    psi.Arguments += " " + appSettings.PowerPointDiffToolArguments;
                                }

                                Process.Start(psi);
                            }
                            catch (Exception ex)
                            {
                                EnableForm1();
                                MessageBox.Show(ex.Message, Program.AppName);
                            }

                            return;
                        }
                    }
                }

                await Task.Delay(3000); // 更新検知から実際に更新されるまで少し待つ
                try
                {
                    Program.CopyAll(cacheRelation.LocalPath, cacheRelation.RemotePath);
                    cacheSettings.CacheRelations.Remove(cacheRelation);
                    cacheSettings.CacheRelations.Add(new CacheRelation(cacheRelation.RemotePath, cacheRelation.LocalPath, File.GetLastWriteTime(cacheRelation.RemotePath)));
                    try
                    {
                        File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    EnableForm1();
                    MessageBox.Show(ex.Message, Program.AppName);
                }
            };

            listBox1.KeyDown += (s, eventArgs) =>
            {
                if (eventArgs.KeyData == Keys.Delete)
                {
                    if (listBox1.SelectedItems.Count != 1) return;
                    var dr = MessageBox.Show(
                        "選択したアイテムを削除してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
                    if (dr != DialogResult.Yes) return;

                    listBox1.Items.Remove(listBox1.SelectedItems[0]);

                    appSettings.CacheTargetDirectories = listBox1.Items.OfType<string>().ToList();
                    try
                    {
                        File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                    }
                    catch { }
                }
            };

            this.FormClosing += (s, eventArgs) =>
            {
                var dr = MessageBox.Show(
                    "終了してよろしいですか？\n\n※ローカルの更新はリモートに反映されなくなります。\n※最小化するとタスクトレイに常駐して処理を継続します。", Program.AppName, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) eventArgs.Cancel = true;
            };

            this.Shown += (s, eventArgs) => DisableForm1();

            this.SizeChanged += async (s, eventArgs) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    await Task.Delay(500);
                    DisableForm1();
                }
            };

            notifyIcon1.MouseClick += (s, eventArgs) =>
            {
                if (eventArgs.Button != MouseButtons.Left) return;
                EnableForm1();
            };

            toolStripMenuItem1.Click += (s, eventArgs) => EnableForm1();
            toolStripMenuItem2.Click += (s, eventArgs) =>
            {
                var result = MessageBox.Show("再起動しますか？", Program.AppName, MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                Application.Restart();
            };
            toolStripMenuItem3.Click += (s, eventArgs) => this.Close();

            timer1.Start();
        }

        public void OpenFile(string[] args)
        {
            if (args == null || args.Length == 0) return;

            foreach (var filePath in args)
            {
                var extension = Path.GetExtension(filePath).ToLower();
                if (extension != ".xls" && extension != ".xlsx" && extension != ".xlsm" &&
                    extension != ".doc" && extension != ".docx" && extension != ".docm" &&
                    extension != ".ppt" && extension != ".pptx" && extension != ".pptm") continue;

                string arguments = string.Empty;
                if (listBox1.Items.OfType<string>().Any(x => filePath.ToLower().StartsWith(x.ToLower())))
                {
                    // キャッシュ対象の場合
                    var cacheRelation = cacheSettings.CacheRelations.FirstOrDefault(x => x.RemotePath == filePath);
                    if (cacheRelation == null)
                    {
                        // キャッシュ済みでない場合
                        var cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
                        var itemCacheFolder = Path.Combine(cacheFolder, Guid.NewGuid().ToString());
                        if (!Directory.Exists(itemCacheFolder)) Directory.CreateDirectory(itemCacheFolder);

                        var cacheFile = Path.Combine(itemCacheFolder, Path.GetFileName(filePath));
                        try
                        {
                            Program.CopyAll(filePath, cacheFile);
                            cacheSettings.CacheRelations.Add(new CacheRelation(filePath, cacheFile, File.GetLastWriteTime(filePath)));
                            try
                            {
                                File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                                    Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                            }
                            catch { }
                        }
                        catch (Exception ex)
                        {
                            EnableForm1();
                            MessageBox.Show(ex.Message, Program.AppName);
                        }

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
                                Program.CopyAll(filePath, localPath);
                                cacheSettings.CacheRelations.Remove(cacheRelation);
                                cacheSettings.CacheRelations.Add(new CacheRelation(filePath, localPath, lastWriteTime));
                                try
                                {
                                    File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                                        Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                                }
                                catch { }
                            }
                            catch (Exception ex)
                            {
                                EnableForm1();
                                MessageBox.Show(ex.Message, Program.AppName);
                            }

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

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.Arguments = $@"""{arguments}""";

                if (extension == ".xls" || extension == ".xlsx" || extension == ".xlsm")
                {
                    psi.FileName = appSettings.ExcelPath;
                }
                else if (extension == ".doc" || extension == ".docx" || extension == ".docm")
                {
                    psi.FileName = appSettings.WordPath;
                }
                else if (extension == ".ppt" || extension == ".pptx" || extension == ".pptm")
                {
                    psi.FileName = appSettings.PowerPointPath;
                }

                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    EnableForm1();
                    MessageBox.Show(ex.Message, Program.AppName);
                }
            }
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
                File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var keyword = textBox2.Text;
            var cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
            var queryString = $@"search-ms:query={keyword} NOT kind:folder&crumb=location:{cacheFolder}";

            var psi = new ProcessStartInfo(queryString);
            psi.UseShellExecute = true;
            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                EnableForm1();
                MessageBox.Show(ex.Message, Program.AppName);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var fileType = Application.ProductName + ".x";
            if (checkBox1.Checked)
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;

                Microsoft.Win32.RegistryKey cmdkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\shell\\open\\command");
                cmdkey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                cmdkey.Close();

                Microsoft.Win32.RegistryKey iconkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\DefaultIcon");
                iconkey.SetValue("", "\"" + Application.StartupPath + "\\x.ico\",0");
                iconkey.Close();

                Microsoft.Win32.RegistryKey regkey1 = currentUserKey.CreateSubKey("Software\\Classes\\.xls");
                regkey1.SetValue("", fileType);
                regkey1.Close();
                Microsoft.Win32.RegistryKey regkey2 = currentUserKey.CreateSubKey("Software\\Classes\\.xlsx");
                regkey2.SetValue("", fileType);
                regkey2.Close();
                Microsoft.Win32.RegistryKey regkey3 = currentUserKey.CreateSubKey("Software\\Classes\\.xlsm");
                regkey3.SetValue("", fileType);
                regkey3.Close();
            }
            else
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;
                currentUserKey.DeleteSubKeyTree("Software\\Classes\\" + fileType);
            }

            appSettings.IsRelatedExcel = checkBox1.Checked;
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            var fileType = Application.ProductName + ".d";
            if (checkBox2.Checked)
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;

                Microsoft.Win32.RegistryKey cmdkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\shell\\open\\command");
                cmdkey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                cmdkey.Close();

                Microsoft.Win32.RegistryKey iconkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\DefaultIcon");
                iconkey.SetValue("", "\"" + Application.StartupPath + "\\d.ico\",0");
                iconkey.Close();

                Microsoft.Win32.RegistryKey regkey1 = currentUserKey.CreateSubKey("Software\\Classes\\.doc");
                regkey1.SetValue("", fileType);
                regkey1.Close();
                Microsoft.Win32.RegistryKey regkey2 = currentUserKey.CreateSubKey("Software\\Classes\\.docx");
                regkey2.SetValue("", fileType);
                regkey2.Close();
                Microsoft.Win32.RegistryKey regkey3 = currentUserKey.CreateSubKey("Software\\Classes\\.docm");
                regkey3.SetValue("", fileType);
                regkey3.Close();
            }
            else
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;
                currentUserKey.DeleteSubKeyTree("Software\\Classes\\" + fileType);
            }

            appSettings.IsRelatedWord = checkBox2.Checked;
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            var fileType = Application.ProductName + ".p";
            if (checkBox3.Checked)
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;

                Microsoft.Win32.RegistryKey cmdkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\shell\\open\\command");
                cmdkey.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                cmdkey.Close();

                Microsoft.Win32.RegistryKey iconkey = currentUserKey.CreateSubKey("Software\\Classes\\" + fileType + "\\DefaultIcon");
                iconkey.SetValue("", "\"" + Application.StartupPath + "\\p.ico\",0");
                iconkey.Close();

                Microsoft.Win32.RegistryKey regkey1 = currentUserKey.CreateSubKey("Software\\Classes\\.ppt");
                regkey1.SetValue("", fileType);
                regkey1.Close();
                Microsoft.Win32.RegistryKey regkey2 = currentUserKey.CreateSubKey("Software\\Classes\\.pptx");
                regkey2.SetValue("", fileType);
                regkey2.Close();
                Microsoft.Win32.RegistryKey regkey3 = currentUserKey.CreateSubKey("Software\\Classes\\.pptm");
                regkey3.SetValue("", fileType);
                regkey3.Close();
            }
            else
            {
                Microsoft.Win32.RegistryKey currentUserKey = Microsoft.Win32.Registry.CurrentUser;
                currentUserKey.DeleteSubKeyTree("Software\\Classes\\" + fileType);
            }

            appSettings.IsRelatedPowerPoint = checkBox3.Checked;
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                "すべてのキャッシュを削除してよろしいですか？", Program.AppName, MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            var cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
            try
            {
                DeleteReadonlyAttribute(new DirectoryInfo(cacheFolder));
            }
            catch { }
            try
            {
                Directory.Delete(cacheFolder, true);
            }
            catch { }
            if (!Directory.Exists(cacheFolder)) Directory.CreateDirectory(cacheFolder);

            cacheSettings = new CacheSettings();
            try
            {
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
            }
            catch { }

            MessageBox.Show("キャッシュの削除が完了しました。", Program.AppName);
        }

        private void DeleteReadonlyAttribute(DirectoryInfo di)
        {
            try
            {
                if ((di.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) di.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in di.GetFiles())
                    if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) fi.Attributes = FileAttributes.Normal;
            }
            catch { }
            
            foreach (DirectoryInfo d in di.GetDirectories())
            {
                try
                {
                    DeleteReadonlyAttribute(d);
                }
                catch { }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cacheSettings.CacheRelations.RemoveAll(x => !File.Exists(x.LocalPath));
            try
            {
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
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

            if (!listBox1.Items.OfType<string>().Any(x => textBox3.Text.ToLower().StartsWith(x.ToLower())))
            {
                listBox1.Items.Add(textBox3.Text);
                appSettings.CacheTargetDirectories = listBox1.Items.OfType<string>().ToList();
                try
                {
                    File.WriteAllText(Path.Combine(Application.StartupPath, "appSettings.json"), JsonSerializer.Serialize(appSettings, jsonSerializerOptions));
                }
                catch { }
            }

            button3.Enabled = false;
            textBox3.Enabled = false;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = createCacheManager.CacheTargetCount;
            progressBar1.Value = createCacheManager.CreatedCacheCount;
        }

        private void MargeCreatedCacheToCacheSettings()
        {
            try
            {
                var text = File.ReadAllText(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.createdCacheList.txt"));
                var createdCacheList = text.Split('\n');

                foreach (var item in createdCacheList)
                {
                    if (string.IsNullOrEmpty(item)) continue;

                    var createdCache = item.Split('\t');
                    if (cacheSettings.CacheRelations.Any(x => x.RemotePath == createdCache[0])) continue;

                    cacheSettings.CacheRelations.Add(new CacheRelation(createdCache[0], createdCache[1], DateTime.Parse(createdCache[2])));
                }

                File.WriteAllText(Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\cacheSettings.json"), JsonSerializer.Serialize(cacheSettings, jsonSerializerOptions));
                File.Delete(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.createdCacheList.txt"));
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
    }
}
