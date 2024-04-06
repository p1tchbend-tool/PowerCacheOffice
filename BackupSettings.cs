using System.Collections.Generic;

namespace PowerCacheOffice
{
    internal class BackupSettings
    {
        public string Version { get; set; }
        public List<BackupRelation> BackupRelations { get; set; }

        public BackupSettings()
        {
            Version = "1.0.0";
            BackupRelations = new List<BackupRelation>();
        }
    }
}
