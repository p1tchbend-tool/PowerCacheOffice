using System.Collections.Generic;

namespace PowerCacheOffice
{
    public class AppSettings
    {
        public string Version { get; set; }
        public string ExcelPath { get; set; }
        public string WordPath { get; set; }
        public string PowerPointPath { get; set; }
        public bool IsRelatedExcel { get; set; }
        public bool IsRelatedWord { get; set; }
        public bool IsRelatedPowerPoint { get; set; }
        public List<string> CacheTargetDirectories { get; set; }

        public AppSettings()
        {
            Version = "1.0.0";
            ExcelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            WordPath = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
            PowerPointPath = @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE";
            IsRelatedExcel = false;
            IsRelatedWord = false;
            IsRelatedPowerPoint = false;
            CacheTargetDirectories = new List<string>();
        }
    }
}
