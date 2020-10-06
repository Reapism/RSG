namespace RSG.Core.Configuration
{
    /// <summary>
    /// Responsible for creating a <see cref="DictionaryConfiguration"/> instance.
    /// </summary>
    internal class LoadDictionaryConfiguration : LoadBase<DictionaryConfiguration>
    {
        public const string ExternalConfigurationName = "Dictionary.json";
        public const string InternalConfigurationName = "DefaultDictionaryConfiguration.json";
    }
}
