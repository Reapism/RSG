using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    internal interface ICharacterFrequency
    {
        IEnumerable<int> MostFrequentCharacterCount { get; set; }

        IEnumerable<int> LeastFrequentCharacterCount { get; set; }

        IEnumerable<char> MostFrequentCharacter { get; set; }

        IEnumerable<char> LeastFrequentCharacter { get; set; }
    }
}
