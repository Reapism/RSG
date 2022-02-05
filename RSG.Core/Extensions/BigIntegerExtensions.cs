using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Extensions
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
    }
}
