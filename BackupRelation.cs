using System;

namespace PowerCacheOffice
{
    public class BackupRelation
    {
        public string OriginalFilePath { get; set; }
        public string BackupFilePath { get; set; }
        public DateTime LastWriteTime { get; set; }

        public BackupRelation(string originalFilePath, string backupFilePath, DateTime lastWriteTime)
        {
            OriginalFilePath = originalFilePath;
            BackupFilePath = backupFilePath;
            LastWriteTime = lastWriteTime;
        }
    }
}
