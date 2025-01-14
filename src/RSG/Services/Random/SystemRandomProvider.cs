namespace RSG.Services
{
    /// <summary>
    /// A <see cref="Random"/> provider that uses a threadsafe <see cref="Random"/> instance.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SystemRandomProvider"/> class.
    /// </remarks>
    /// <param name="random">A random instance.</param>
    public class SystemRandomProvider(Random random) : IRandomProvider<int>
    {
        private readonly Random random = random ?? throw new ArgumentNullException(nameof(random));

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomProvider"/> class.
        /// </summary>
        public SystemRandomProvider()
            : this(Random.Shared)
        { }

        /// <inheritdoc/>
        public int Next()
        {
            return random.Next();
        }

        /// <inheritdoc/>
        public int Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        /// <inheritdoc/>
        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
