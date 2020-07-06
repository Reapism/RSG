using RSG.Core.Enums;
using System;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Represents generic configuration settings.
    /// </summary>
    public interface IRsgConfiguration
    {
        Version CurrentVersion { get; set; }

        int NumberOfLaunchesThisVersion { get; set; }

        int NumberOfLaunchesTotal { get; set; }

        bool CheckForUpdatesOnLoad { get; set; }

        bool CopySelectionsToClipboard { get; set; }

        bool FirstTimeUsingCurrentVersion { get; set; }

        bool UseStickyWindows { get; set; }

        RandomizationType RandomizationType { get; set; }

        string DictionaryConfigurationSource { get; set; }

        string StringConfigurationSource { get; set; }
    }
}
