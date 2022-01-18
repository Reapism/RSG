using RSG.Core.Enums;

namespace RSG.Core.Configuration
{
    public class RsgConfiguration
    {
        public RsgConfiguration()
        {

        }

        public bool CheckForUpdatesOnLoad { get; set; }
        public bool FirstTimeUsingCurrentVersion { get; set; }
        public bool CopySelectionsToClipboard { get; set; }
        public RandomizationType RandomizationType { get; set; }
        public int NumberOfLaunchesThisVersion { get; set; }
        public int NumberOfLaunchesTotal { get; set; }
        public bool UseStickyWindows { get; set; }
        public string CurrentVersion { get; set; }
        public string DictionaryConfigurationSource { get; set; }
        public string StringConfigurationSource { get; set; }
    }
}
