using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerCacheOffice
{
    public class CreateCacheManager
    {
        public int CacheTargetCount { get; private set; }
        public int CreatedCacheCount { get; private set; }

        private static readonly string powerCacheOfficeDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice");
        private static readonly string powerCacheOfficeCacheFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
        private List<string> caches = new List<string>();

        public CreateCacheManager()
        {
            CacheTargetCount = 0;
            CreatedCacheCount = 0;
        }

        public Task CountCacheTargetAsync(string folder)
        {
            return Task.Run(() =>
            {
                CountCacheTarget(folder);
            });
        }

        private void CountCacheTarget(string folder)
        {
            if (folder.ToLower().StartsWith(powerCacheOfficeDataFolder.ToLower())) return;
            foreach (var x in Directory.EnumerateFiles(folder))
            {
                try
                {
                    if ((new FileInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) continue;

                    var extension = Path.GetExtension(x).ToLower();
                    if (extension != ".xls" && extension != ".xlsx" && extension != ".xlsm" &&
                        extension != ".doc" && extension != ".docx" && extension != ".docm" &&
                        extension != ".ppt" && extension != ".pptx" && extension != ".pptm") continue;

                    CacheTargetCount++;
                }
                catch { }
            }

            foreach (var x in Directory.EnumerateDirectories(folder))
            {
                try
                {
                    if ((new DirectoryInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) continue;
                    CountCacheTarget(x);
                }
                catch { }
            }
        }

        public Task CreateCacheAsync(string folder, List<string> caches)
        {
            this.caches = caches;
            return Task.Run(() =>
            {
                CreateCache(folder);
            });
        }

        private void CreateCache(string folder)
        {
            if (folder.ToLower().StartsWith(powerCacheOfficeDataFolder.ToLower())) return;
            foreach (var x in Directory.EnumerateFiles(folder))
            {
                try
                {
                    if ((new FileInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) continue;

                    var extension = Path.GetExtension(x).ToLower();
                    if (extension != ".xls" && extension != ".xlsx" && extension != ".xlsm" &&
                        extension != ".doc" && extension != ".docx" && extension != ".docm" &&
                        extension != ".ppt" && extension != ".pptx" && extension != ".pptm") continue;

                    try
                    {
                        if (caches.Any(cache => cache == x)) continue;

                        var itemCacheFolder = Path.Combine(powerCacheOfficeCacheFolder, Guid.NewGuid().ToString());
                        if (!Directory.Exists(itemCacheFolder)) Directory.CreateDirectory(itemCacheFolder);

                        var cacheFile = Path.Combine(itemCacheFolder, Path.GetFileName(x));
                        Program.CopyFileAndAttributes(x, cacheFile);

                        File.AppendAllText(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.createdCacheList.txt"),
                            x + "\t" + cacheFile + "\t" + File.GetLastWriteTime(x).ToString() + "\n");
                    }
                    finally { CreatedCacheCount++; }
                }
                catch { }
            }

            foreach (var x in Directory.EnumerateDirectories(folder))
            {
                try
                {
                    if ((new DirectoryInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) continue;
                    CreateCache(x);
                }
                catch { }
            }
        }
    }
}
