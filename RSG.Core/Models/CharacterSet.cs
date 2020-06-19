using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class CharacterSet : ICharacterSet
    {
        public IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}
