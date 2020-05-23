using System.Numerics;

namespace RSG.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static BigInteger ToBigInteger(this byte value)
        {
            return BigInteger.Parse(value.ToString());
        }

        public static BigInteger ToBigInteger(this short value)
        {
            return BigInteger.Parse(value.ToString());
        }

        public static BigInteger ToBigInteger(this int value)
        {
            return BigInteger.Parse(value.ToString());
        }

        public static BigInteger ToBigInteger(this long value)
        {
            return BigInteger.Parse(value.ToString());
        }
    }
}
