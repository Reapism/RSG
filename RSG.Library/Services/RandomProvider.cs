using RSG.Core.Constants;
using System.ComponentModel;

namespace RSG.Core.Services
{
    public static class RandomProvider
    {
        private static System.Random PsuedoRandom;
        private static System.Random ReapRandom;

        static RandomProvider()
        {
            PsuedoRandom = new System.Random();
            ReapRandom = new System.Random(PsuedoRandom.Next(int.MinValue, int.MaxValue));
        }

        public static string RandomizationType { get; set; }

        public static System.Random Random
        {
            get
            {
                return RandomizationType switch
                {
                    RandomizationConstants.Pseudorandom => PsuedoRandom,
                    RandomizationConstants.ReapRandom => ReapRandom,
                    _ => PsuedoRandom,
                };
            }
        }
    }
}
