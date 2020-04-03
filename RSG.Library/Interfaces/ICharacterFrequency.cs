using System.Numerics;

namespace RSG.Library.Interfaces
{
    internal interface ICharacterFrequency
    {
        BigInteger MostFrequentCharacterNumber { get; set; }
        BigInteger LeastFrequentCharacterNumber { get; set; }
        char MostFrequentCharacter { get; set; }
        char LeastFrequentCharacter { get; set; }
    }
}
