using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    public interface ICharacterFrequency
    {
        IDictionary<char, int> OccurrencesByCharacter { get; set; }
    }
}
