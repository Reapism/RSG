using RSG.Library.Enums;

namespace RSG.Library.Utilities.Random
{
    public static class RsgRandom
    {
        private static System.Random _psuedoRandom;
        private static System.Random _reapRandom;

        public static RandomizationType SelectedRandomizationType { get; set; }

        static RsgRandom()
        {
            _psuedoRandom = new System.Random();
            _reapRandom = new System.Random(_psuedoRandom.Next(int.MaxValue));
        }

        public static System.Random Rnd {
            get 
            {
                return SelectedRandomizationType switch
                {
                    RandomizationType.Pseudorandom => _psuedoRandom,
                    RandomizationType.ReapRandom => _reapRandom,
                    _ => _psuedoRandom,
                };
            } 
        }
    }
}
