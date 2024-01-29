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
                    if ((new DirectoryInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) return;
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

                        var cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PowerCacheOffice\.cache");
                        var itemCacheFolder = Path.Combine(cacheFolder, Guid.NewGuid().ToString());
                        if (!Directory.Exists(itemCacheFolder)) Directory.CreateDirectory(itemCacheFolder);

                        var cacheFile = Path.Combine(itemCacheFolder, Path.GetFileName(x));
                        Program.CopyAll(x, cacheFile);

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
                    if ((new DirectoryInfo(x).Attributes & FileAttributes.System) == FileAttributes.System) return;
                    CreateCache(x);
                }
                catch { }
            }
        }
    }
}
