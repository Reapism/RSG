using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class CharacterFrequency : ICharacterFrequency
    {
        public IEnumerable<int> MostFrequentCharacterCount { get; set; }

        public IEnumerable<int> LeastFrequentCharacterCount { get; set; }

        public IEnumerable<char> MostFrequentCharacter { get; set; }

        public IEnumerable<char> LeastFrequentCharacter { get; set; }
    }
}
