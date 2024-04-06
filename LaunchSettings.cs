using System.Collections.Generic;

namespace PowerCacheOffice
{
    internal class LaunchSettings
    {
        public string Version { get; set; }
        public List<string> LaunchViewBase64Images { get; set; }
        public List<string> LaunchViewPaths { get; set; }

        public LaunchSettings(List<string> launchViewBase64Images, List<string> launchViewPaths)
        {
            Version = "1.0.0";
            LaunchViewBase64Images = launchViewBase64Images;
            LaunchViewPaths = launchViewPaths;
        }
    }
}
