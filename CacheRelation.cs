using System;

namespace PowerCacheOffice
{
    public class CacheRelation
    {
        public string RemotePath { get; set; }
        public string LocalPath { get; set; }
        public DateTime RemoteLastWriteTime { get; set; }

        public CacheRelation(string remotePath, string localPath, DateTime remoteLastWriteTime)
        {
            RemotePath = remotePath;
            LocalPath = localPath;
            RemoteLastWriteTime = remoteLastWriteTime;
        }
    }
}
