using System;
using System.Security.Cryptography;

namespace RSG.Core.Interfaces.Services
{
    /// <summary>
    /// Provides a contract for generating random int(s).
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Returns a non-negative random <see cref="int"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="int.MaxValue"/>.</returns>
        int Next();

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must
        /// be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue;
        /// that is, the range of return values ordinarily includes 0 but not maxValue. However,
        /// if maxValue equals 0, maxValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxValue"/> is less than 0.</exception>
        int Next(int maxValue);

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater
        /// than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        int Next(int minValue, int maxValue);
    }

    /// <summary>
    /// A <see cref="Random"/> provider that uses a threadsafe <see cref="Random"/> instance.
    /// </summary>
    public class SystemRandomProvider : IRandom
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomProvider"/> class.
        /// </summary>
        public SystemRandomProvider()
            : this(Random.Shared)
        {
            random = Random.Shared;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRandomProvider"/> class.
        /// </summary>
        /// <param name="random">A random instance.</param>
        public SystemRandomProvider(Random random)
        {
            this.random = random ?? throw new ArgumentNullException(nameof(random));
        }

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

    /// <summary>
    /// Use <see cref="RandomNumberGenerator"/> to generate cryptographically random numbers.
    /// </summary>
    public class CryptoRandomProvider : IRandom
    {
        /// <inheritdoc/>
        public int Next()
        {
            return RandomNumberGenerator.GetInt32(0, int.MaxValue);
        }

        /// <inheritdoc/>
        public int Next(int maxValue)
        {
            return RandomNumberGenerator.GetInt32(maxValue);
        }

        /// <inheritdoc/>
        public int Next(int minValue, int maxValue)
        {
            return RandomNumberGenerator.GetInt32(minValue, maxValue);
        }
    }
}
