using System;
using System.Security.Cryptography;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }

    public class SystemRandomProvider : IRandom
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomProvider"/> class.
        /// </summary>
        public SystemRandomProvider()
            : this(Random.Shared)
        {
            random = Random.Shared;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomProvider"/> class.
        /// </summary>
        /// <param name="random"></param>
        public SystemRandomProvider(Random random)
        {
            this.random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public int Next()
        {
            return random.Next();
        }

        public int Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }

    public class CryptoRandomProvider : IRandom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoRandomProvider"/> class.
        /// </summary>
        public CryptoRandomProvider()
        {
        }

        public int Next()
        {
            return RandomNumberGenerator.GetInt32(0, int.MaxValue);
        }

        public int Next(int maxValue)
        {
            return RandomNumberGenerator.GetInt32(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return RandomNumberGenerator.GetInt32(minValue, maxValue);
        }
    }
}
