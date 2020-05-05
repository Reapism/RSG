using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a word list.
    /// </summary>
    public class WordList : IWordList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordList"/> class.
        /// </summary>
        public WordList()
        {
            Words = new Queue<string>(100000);
        }

        /// <summary>
        /// Gets or sets the word list.
        /// </summary>
        public IEnumerable<string> Words { get; set; }
    }
}
