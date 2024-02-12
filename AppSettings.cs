using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PowerCacheOffice
{
    public class AppSettings
    {
        public string Version { get; set; }
        public string ExcelPath { get; set; }
        public string WordPath { get; set; }
        public string PowerPointPath { get; set; }

        public string ExcelDiffToolPath { get; set; }
        public string ExcelDiffToolArguments { get; set; }
        public string WordDiffToolPath { get; set; }
        public string WordDiffToolArguments { get; set; }
        public string PowerPointDiffToolPath { get; set; }
        public string PowerPointDiffToolArguments { get; set; }

        public bool IsRelatedExcel { get; set; }
        public bool IsRelatedWord { get; set; }
        public bool IsRelatedPowerPoint { get; set; }
        public bool IsStartUp { get; set; }
        public List<string> CacheTargetDirectories { get; set; }

        public Keys OpenClipboardPathKey { get; set; }
        public int OpenClipboardPathModKey {  get; set; }
        public Keys OpenRecentFileKey { get; set; }
        public int OpenRecentFileModKey {  get; set; }

        public AppSettings()
        {
            Version = "1.2.1";
            ExcelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            WordPath = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
            PowerPointPath = @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE";

            ExcelDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
            ExcelDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";
            WordDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
            WordDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";
            PowerPointDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
            PowerPointDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";

            IsRelatedExcel = true;
            IsRelatedWord = true;
            IsRelatedPowerPoint = true;
            IsStartUp = true;
            CacheTargetDirectories = new List<string>();

            OpenClipboardPathKey = Keys.R;
            OpenClipboardPathModKey = HotKey.MOD_KEY_ALT;
            OpenRecentFileKey = Keys.R;
            OpenRecentFileModKey = HotKey.MOD_KEY_ALT + HotKey.MOD_KEY_SHIFT;
        }

        public void UpdateSettings()
        {
            if (this.Version == "1.0.0")
            {
                this.ExcelDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                this.ExcelDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";
                this.WordDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                this.WordDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";
                this.PowerPointDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                this.PowerPointDiffToolArguments = @"/u /dl ""ローカル"" /dr ""リモート""";

                this.Version = "1.1.0";
            }

            if (this.Version == "1.1.0")
            {
                this.OpenClipboardPathKey = Keys.R;
                this.OpenClipboardPathModKey = HotKey.MOD_KEY_ALT;
                this.OpenRecentFileKey = Keys.R;
                this.OpenRecentFileModKey = HotKey.MOD_KEY_ALT + HotKey.MOD_KEY_SHIFT;

                this.Version = "1.2.0";
            }

            if (this.Version == "1.2.0")
            {
                this.IsStartUp = true;

                this.Version = "1.2.1";
            }

            if (this.ExcelPath == @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE" ||
                this.ExcelPath == @"C:\Program Files (x86)\Microsoft Office\root\Office16\EXCEL.EXE")
            {
                if (File.Exists(@"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE"))
                {
                    this.ExcelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
                }
                else if (File.Exists(@"C:\Program Files (x86)\Microsoft Office\root\Office16\EXCEL.EXE"))
                {
                    this.ExcelPath = @"C:\Program Files (x86)\Microsoft Office\root\Office16\EXCEL.EXE";
                }
            }

            if (this.WordPath == @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE" ||
                this.WordPath == @"C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE")
            {
                if (File.Exists(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE"))
                {
                    this.WordPath = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
                }
                else if (File.Exists(@"C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE"))
                {
                    this.WordPath = @"C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.EXE";
                }
            }

            if (this.PowerPointPath == @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE" ||
                this.PowerPointPath == @"C:\Program Files (x86)\Microsoft Office\root\Office16\POWERPNT.EXE")
            {
                if (File.Exists(@"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE"))
                {
                    this.PowerPointPath = @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE";
                }
                else if (File.Exists(@"C:\Program Files (x86)\Microsoft Office\root\Office16\POWERPNT.EXE"))
                {
                    this.PowerPointPath = @"C:\Program Files (x86)\Microsoft Office\root\Office16\POWERPNT.EXE";
                }
            }

            var userWinmergePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Programs\WinMerge\WinMergeU.exe");

            if (this.ExcelDiffToolPath == @"C:\Program Files\WinMerge\WinMergeU.exe" ||
                this.ExcelDiffToolPath == userWinmergePath)
            {
                if (File.Exists(@"C:\Program Files\WinMerge\WinMergeU.exe"))
                {
                    this.ExcelDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                }
                else if (File.Exists(userWinmergePath))
                {
                    this.ExcelDiffToolPath = userWinmergePath;
                }
            }

            if (this.WordDiffToolPath == @"C:\Program Files\WinMerge\WinMergeU.exe" ||
                this.WordDiffToolPath == userWinmergePath)
            {
                if (File.Exists(@"C:\Program Files\WinMerge\WinMergeU.exe"))
                {
                    this.WordDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                }
                else if (File.Exists(userWinmergePath))
                {
                    this.WordDiffToolPath = userWinmergePath;
                }
            }

            if (this.PowerPointDiffToolPath == @"C:\Program Files\WinMerge\WinMergeU.exe" ||
                this.PowerPointDiffToolPath == userWinmergePath)
            {
                if (File.Exists(@"C:\Program Files\WinMerge\WinMergeU.exe"))
                {
                    this.PowerPointDiffToolPath = @"C:\Program Files\WinMerge\WinMergeU.exe";
                }
                else if (File.Exists(userWinmergePath))
                {
                    this.PowerPointDiffToolPath = userWinmergePath;
                }
            }
        }
    }
}
