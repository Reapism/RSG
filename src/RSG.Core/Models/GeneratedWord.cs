using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a generated word with the potential for storing additional characters.
    /// </summary>
    public sealed class GeneratedWord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratedWord"/> class.
        /// </summary>
        /// <param name="word">The original word.</param>
        /// <param name="positionalCharacters">Additional characters that obfuscate the original word in someway.</param>
        public GeneratedWord(string word, IEnumerable<PositionalCharacter> positionalCharacters)
        {
            Word = word;
            AdditionalCharacterPositions = positionalCharacters;
        }

        /// <summary>
        /// Gets the locations of additional character positions that were part of the generation settings.
        /// </summary>
        public IEnumerable<PositionalCharacter> AdditionalCharacterPositions { get; }

        /// <summary>
        /// Gets the original, unmodified generated word.
        /// </summary>
        public string Word { get; }

        /// <summary>
        /// Provides the ability to render the <see cref="Word"/> with potential <see cref="AdditionalCharacterPositions"/>.
        /// </summary>
        /// <returns>A rendered word computed via <see cref="AdditionalCharacterPositions"/>.</returns>
        public string RenderWord()
        {
            if (AdditionalCharacterPositions is null || !AdditionalCharacterPositions.Any())
            {
                return Word;
            }

            var wordBuilder = new StringBuilder(Word);
            foreach (var positionalCharacter in AdditionalCharacterPositions)
            {
                var characterPosition = positionalCharacter.Position;
                var character = positionalCharacter.Character;
                wordBuilder.Insert(characterPosition, character);
            }

            var renderedWord = wordBuilder.ToString();

            return renderedWord;
        }
    }
}
