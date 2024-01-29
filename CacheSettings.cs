using System.Collections.Generic;

namespace PowerCacheOffice
{
    public class CacheSettings
    {
        public string Version { get; set; }
        public List<CacheRelation> CacheRelations { get; set; }

        public CacheSettings()
        {
            Version = "1.0.0";
            CacheRelations = new List<CacheRelation>();
        }
    }
}
