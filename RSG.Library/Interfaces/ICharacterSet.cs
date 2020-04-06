using RSG.Library.Models;
using System.Collections.Generic;

namespace RSG.Library.Interfaces
{
    public interface ICharacterSet
    {
        IDictionary<string, CharacterSet> CharacterSets { get; set; }
    }
}
