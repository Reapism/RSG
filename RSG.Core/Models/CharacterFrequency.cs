using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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
