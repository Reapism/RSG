using RSG.Core.Enums;
using RSG.Core.Interfaces.Services;
using System;
using System.Threading;

namespace RSG.Core.Services
{
    public static class RandomProvider
    {
        private static SystemRandomProvider psuedoRandom;
        private static CryptoRandomProvider cryptographicRandom;

        private static int seed = Environment.TickCount;
        static RandomProvider()
        {
            psuedoRandom = new SystemRandomProvider();
            cryptographicRandom = new CryptoRandomProvider();

            SelectedRandomizationType = RandomizationType.Pseudorandom;
        }

        public static RandomizationType SelectedRandomizationType { get; set; }

        public static IRandom Random => SelectedRandomizationType switch
        {
            RandomizationType.Pseudorandom => psuedoRandom,
            RandomizationType.CryptographicRandom => psuedoRandom,
            _ => psuedoRandom,
        };
    }
}
