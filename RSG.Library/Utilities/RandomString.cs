using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Library.Utilities
{
    public static class RandomString
    {
        public static IEnumerable<string> GenerateRandomStrings(int numberOfIterations, int length)
        {
            return GenerateRandomStrings(BigInteger.Parse(numberOfIterations.ToString()), BigInteger.Parse(length.ToString()));
        }

        public static IEnumerable<string> GenerateRandomStrings(BigInteger numberOfIterations, BigInteger length)
        {
            var queue = new Queue<string>();

            for (BigInteger bi = 0; bi < numberOfIterations; bi++)
            {
                queue.Enqueue(GenerateRandomString(length));
            }

            return queue;
        }

        private static string GenerateRandomString(int length)
        {
            return GenerateRandomString(BigInteger.Parse(length.ToString()));
        }

        private static string GenerateRandomString(BigInteger length)
        {
            var stringBuilder = new StringBuilder();
            var maxLength = Characters.CharacterSet.Length;

            for (var bi = BigInteger.Zero; bi < length; bi++)
            {
                stringBuilder.Append(Characters.CharacterSet[RsgRandom.Rnd.Next(maxLength)]);
            }

            return stringBuilder.ToString();
        }
    }
}
