using RSG.Core.Enums;
using RSG.Core.Interfaces.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace RSG.Core.Configuration
{
    public class RsgConfiguration : IRsgConfiguration
    {
        private const string ConfigurationFileName = "RSG.config";

        public RsgConfiguration()
        {
            CurrentVersion = Assembly.GetExecutingAssembly().GetName()?.Version;

            // Load this info.
            StringConfigurationSource = string.Empty;
            DictionaryConfigurationSource = string.Empty;
        }

        public Version CurrentVersion { get; set; }
        public bool CheckForUpdatesOnLoad { get; set; }
        public bool FirstTimeUsingCurrentVersion { get; set; }
        public bool CopySelectionsToClipboard { get; set; }
        public RandomizationType RandomizationType { get; set; }
        public int NumberOfLaunchesThisVersion { get; set; }
        public int NumberOfLaunchesTotal { get; set; }
        public bool UseStickyWindows { get; set; }
        public string DictionaryConfigurationSource { get; set; }
        public string StringConfigurationSource { get; set; }
    }
}
