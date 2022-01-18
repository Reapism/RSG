using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Configuration
{
    public interface IDictionaryProvider
    {
        /// <summary>
        /// Gets or sets a <see cref="IList{T}"/> of <see cref="RsgDictionary"/>(s).
        /// </summary>
        IList<RsgDictionary> Dictionaries { get; set; }
    }
}
