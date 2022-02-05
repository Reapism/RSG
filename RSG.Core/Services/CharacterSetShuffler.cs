using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using System.Linq;
using System.Text;

namespace RSG.Core.Services
{
    /// <summary>
    /// A service for shuffling the character list around.
    /// </summary>
    public class CharacterSetShuffler : ICharacterSetShuffler
    {
        private readonly ICharacterSetProvider characterSetProvider;
        private readonly IShuffle<char> shuffle;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetShuffler"/> class.
        /// </summary>
        /// <param name="characterSetProvider">An instance of the <see cref="ICharacterSetProvider"/>.</param>
        /// <param name="shuffle">An instance of <see cref="IShuffle{T}"/> of type
        /// <see langword="char"/>.</param>
        public CharacterSetShuffler(
            ICharacterSetProvider characterSetProvider,
            IShuffle<char> shuffle)
        {
            this.characterSetProvider = characterSetProvider;
            this.shuffle = shuffle;
        }

        /// <summary>
        /// Gets the character list from this instance.
        /// <para>Each invocation triggers a new scrambled
        /// character list.</para>
        /// </summary>
        /// <param name="chars">The characters to shuffle.</param>
        /// <returns></returns>
        public char[] Shuffle()
        {
            return GetNewCharacterList();
        }

        private char[] GetNewCharacterList()
        {
            return ScrambleCharacterList();
        }

        private char[] ScrambleCharacterList()
        {
            var allCharacters = CleanCharacterList();
            shuffle.Shuffle(allCharacters);

            return allCharacters;
        }

        private char[] CleanCharacterList()
        {
            var characterList = GetCharacterListAsString();
            var distinctCharList = characterList.Distinct().ToArray();

            return distinctCharList;
        }

        private char[] GetCharacterListAsString()
        {
            var enabledCharacterSets = characterSetProvider.Characters.ToCharArray();

            return enabledCharacterSets;
        }
    }
}
