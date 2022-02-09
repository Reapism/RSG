using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class CharacterFrequency : ICharacterFrequency
    {
        public IDictionary<char, int> OccurrencesByCharacter { get; set; }
    }
}
