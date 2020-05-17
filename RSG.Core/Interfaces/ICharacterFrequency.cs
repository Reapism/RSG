using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    public interface ICharacterFrequency
    {
        IEnumerable<int> MostFrequentCharacterCount { get; set; }

        IEnumerable<int> LeastFrequentCharacterCount { get; set; }

        IEnumerable<char> MostFrequentCharacter { get; set; }

        IEnumerable<char> LeastFrequentCharacter { get; set; }
    }
}
