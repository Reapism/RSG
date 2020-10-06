using RSG.Core.Enums;
using System;
using System.Threading;

namespace RSG.Core.Services
{
    public static class RandomProvider
    {
        private static ThreadLocal<Random> PsuedoRandom;

        private static int seed = Environment.TickCount;
        static RandomProvider()
        {
            PsuedoRandom = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        }

        public static RandomizationType SelectedRandomizationType { get; set; }

        public static ThreadLocal<Random> Random => SelectedRandomizationType switch
        {
            RandomizationType.Pseudorandom => PsuedoRandom,
            _ => PsuedoRandom,
        };
    }
}
