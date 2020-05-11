using RSG.Core.Enums;
using RSG.Core.Interfaces.Configuration;
using System.Reflection;

namespace RSG.Core.Configuration
{
    public class RsgConfiguration : IRsgConfiguration
    {
        public RsgConfiguration()
        {
            CurrentVersion = int.Parse(Assembly.GetExecutingAssembly().GetName()?.Version.ToString().Replace(".", string.Empty));
        }

        public int CurrentVersion { get; set; }
        public bool CheckForUpdatesOnLoad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool FirstTimeUsingCurrentVersion { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool CopySelectionsToClipboard { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public RandomizationType RandomizationType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int NumberOfLaunchesThisVersion { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int NumberOfLaunchesTotal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool UseStickyWindows { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
