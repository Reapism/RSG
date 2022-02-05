using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Provides a collection of character sets.
    /// </summary>
    public interface ICharacterSetProvider
    {
        /// <summary>
        /// Gets the collection of character(s) to be provided.
        /// </summary>
        IList<CharacterSet> Characters { get; }

        /// <summary>
        /// Converts the <see cref="Characters"/>
        /// </summary>
        /// <returns>Returns a <see cref="char[]"/></returns>
        char[] ToCharArray();
    }
}
