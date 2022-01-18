using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Provides a collection of character sets.
    /// </summary>
    public interface ICharacterSetProvider
    {
        IList<CharacterSet> Characters { get; set; }
    }
}
