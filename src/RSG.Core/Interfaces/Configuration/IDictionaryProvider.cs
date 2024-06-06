using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Provides a type with access to <see cref="RsgDictionary"/>(s).
    /// </summary>
    public interface IDictionaryProvider
    {
        /// <summary>
        /// Gets a <see cref="IList{T}"/> of <see cref="RsgDictionary"/>(s).
        /// </summary>
        IList<RsgDictionary> Dictionaries { get; }
    }
}
