namespace RSG.Core.Interfaces.Services
{
    internal interface IUpdateService
    {
        public string Version { get; set; }
        public string Source { get; set; }

        public bool IsNewVersionAvailable();
    }
}
