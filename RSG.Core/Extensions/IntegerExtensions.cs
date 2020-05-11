using System.Numerics;

namespace RSG.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static BigInteger ToBigInteger(this int value)
        {
            return BigInteger.Parse(value.ToString());
        }
    }
}
