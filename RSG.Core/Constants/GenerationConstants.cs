namespace RSG.Core.Constants
{
    // TODO this will require refactor of how we generate anything.
    // Will require a Func<string> where we use functions to tell
    // RSG how it will generate using these types of generations.
    /// <summary>
    /// Represents constants for generating string with particular character sets.
    /// </summary>
    public static class GenerationConstants
    {
        /// <summary>
        /// Default generation.
        /// </summary>
        public const string Default = nameof(Default);

        /// <summary>
        /// Generation that has a customized character set.
        /// </summary>
        public const string Custom = nameof(Custom);

        /// <summary>
        /// Generation specific for primes.
        /// </summary>
        public const string Prime = nameof(Prime);

        /// <summary>
        /// Generation specific for passwords.
        /// </summary>
        public const string Password = nameof(Password);
    }
}
