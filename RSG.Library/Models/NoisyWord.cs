using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class NoisyWord : IWord
    {
        public NoisyWord(string word, IDictionary<int, char> characterPositions)
        {
            Word = word;
            CharactersPositions = characterPositions;
        }

        /// <summary>
        /// Gets or sets the word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets characters postions appended within a word.
        /// Key{}
        /// </summary>
        public IDictionary<int, char> CharactersPositions { get; set; }
    }
}
