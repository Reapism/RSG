namespace RSG.Core.Configuration
{
    /// <summary>
    /// Responsible for creating a <see cref="StringConfiguration"/> instance.
    /// </summary>
    internal class LoadStringConfiguration : LoadBase<StringConfiguration>
    {
        public const string ExternalConfigurationName = "String.json";
        public const string InternalConfigurationName = "DefaultStringConfiguration.json";
    }
}
