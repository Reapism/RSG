using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents a word list.
    /// </summary>
    public interface IWordList
    {
        /// <summary>
        /// Gets or sets the word list.
        /// </summary>
        IEnumerable<string> Words { get; set; }
    }
}
