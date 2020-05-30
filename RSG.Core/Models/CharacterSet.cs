using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class CharacterSet : ICharacterSet
    {
        public IDictionary<string, CharSet> CharacterSets { get; set; }
    }
}
