using System.Security.Cryptography;

namespace RSG.Services
{
    /// <summary>
    /// Use <see cref="RandomNumberGenerator"/> to generate cryptographically random numbers.
    /// </summary>
    public class CryptoRandomProvider : IRandomProvider<int>
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
