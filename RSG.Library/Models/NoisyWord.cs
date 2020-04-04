using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Library.Models
{
    internal class NoisyWord
    {
        /// <summary>
        /// The word
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Characters appended within a word.
        /// </summary>
        public IEnumerable<char> Characters { get; set; }

        /// <summary>
        /// Postions within the word string where the characters are.
        /// </summary>
        public IEnumerable<int> Positions { get; set; }
    }
}
