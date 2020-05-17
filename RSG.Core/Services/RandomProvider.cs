using RSG.Core.Enums;

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

        public static RandomizationType SelectedRandomizationType { get; set; }

        public static System.Random Random => SelectedRandomizationType switch
        {
            RandomizationType.Pseudorandom => PsuedoRandom,
            RandomizationType.ReapRandom => ReapRandom,
            _ => PsuedoRandom,
        };
    }
}
