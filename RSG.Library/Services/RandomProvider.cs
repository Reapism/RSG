using RSG.Core.Constants;
using System.ComponentModel;

namespace RSG.Core.Services
{
    public static class RandomProvider
    {
        [Description(RandomizationConstants.PSEUDORANDOM)]
        private static System.Random PsuedoRandom;

        [Description(RandomizationConstants.REAP_RANDOM)]
        private static System.Random ReapRandom;

        public static string SelectedRandomizationType { get; set; }

        static RandomProvider()
        {
            PsuedoRandom = new System.Random();
            ReapRandom = new System.Random(PsuedoRandom.Next(int.MinValue, int.MaxValue));
        }

        public static System.Random Random
        {
            get
            {
                return SelectedRandomizationType switch
                {
                    RandomizationConstants.PSEUDORANDOM => PsuedoRandom,
                    RandomizationConstants.REAP_RANDOM => ReapRandom,
                    _ => PsuedoRandom,
                };
            }
        }
    }
}
