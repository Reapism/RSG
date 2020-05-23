using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Core.Models
{
    public class CharacterSet : ICharacterSet
    {
        public IDictionary<string, CharSet> CharacterSets { get; set; }
    }
}
