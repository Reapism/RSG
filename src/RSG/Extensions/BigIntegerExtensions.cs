using System.Numerics;

namespace RSG.Extensions
{
    internal static class BigIntegerExtensions
    {
        /// <summary>
        /// Returns a <see cref="BigInteger"/> value that is equivalent to an <see cref="int"/> value.
        /// </summary>
        /// <param name="value">An int to copy the value from.</param>
        /// <returns>A new <see cref="BigInteger"/> containing the value of the int.</returns>
        internal static BigInteger ToBigInteger(this int value)
        {
            return new BigInteger(value);
        }

        internal static BigInteger ToBigInteger(this byte value)
        {
            return new BigInteger(value);
        }

        internal static BigInteger ToBigInteger(this short value)
        {
            return new BigInteger(value);
        }

        internal static BigInteger ToBigInteger(this long value)
        {
            return new BigInteger(value);
        }

        internal static BigInteger ToBigInteger(this string value)
        {
            return BigInteger.Parse(value);
        }
    }
}
