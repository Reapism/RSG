using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a generated word with the potential for storing additional characters.
    /// </summary>
    public class GeneratedWord : IGeneratedWord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratedWord"/> class.
        /// </summary>
        /// <param name="word">The original word.</param>
        /// <param name="positionalCharacters">Additional characters that obfuscate the original word in someway.</param>
        public GeneratedWord(string word, IEnumerable<IPositionalCharacter> positionalCharacters)
        {
            Word = word;
            AdditionalCharacterPositions = positionalCharacters;
        }

        /// <inheritdoc/>
        public IEnumerable<IPositionalCharacter> AdditionalCharacterPositions { get; }

        /// <inheritdoc/>
        public string Word { get; }

        /// <inheritdoc/>
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
