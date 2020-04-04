using RSG.Library.Constants;

namespace RSG.Library.Services
{
    public static class RandomProvider
    {
        private static System.Random PsuedoRandom;
        private static System.Random ReapRandom;

        public static string SelectedRandomizationType { get; set; }

        static RandomProvider()
        {
            PsuedoRandom = new System.Random();
            ReapRandom = new System.Random(PsuedoRandom.Next(int.MaxValue));
        }

        public static System.Random Rnd
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
