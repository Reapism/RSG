using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class CharacterSet : ICharacterSet
    {
        public CharacterSet()
        {
            Characters = new Dictionary<string, SingleCharacterSet>();
        }

        public IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}
