using RSG.Core.Constants;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System.Linq;
using System.Text;

namespace RSG.Core.Services
{
    /// <summary>
    /// A service for creating a <see cref="ICharacterSet"/>
    /// </summary>
    public class CharacterSetService
    {
        private readonly IStringConfiguration stringConfiguration;
        private readonly IShuffle<char> shuffle;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetService"/> class.
        /// </summary>
        /// <param name="stringConfiguration">An instance of the <see cref="IStringConfiguration"/>.</param>
        /// <param name="shuffle">An instance of <see cref="IShuffle{T}"/> of type
        /// <see langword="char"/>.</param>
        public CharacterSetService(
            IStringConfiguration stringConfiguration,
            IShuffle<char> shuffle)
        {
            this.stringConfiguration = stringConfiguration;
            this.shuffle = shuffle;
        }

        /// <summary>
        /// Gets the character list from this instance.
        /// <para>Each invocation triggers a new scrambled
        /// character list.</para>
        /// </summary>
        public char[] CharacterList => GetNewCharacterList();

        private char[] GetNewCharacterList()
        {
            return ScrambleCharacterList();
        }

        private char[] ScrambleCharacterList()
        {
            var allCharacters = CleanCharacterList();
            shuffle.Shuffle(allCharacters, RandomProvider.Random);

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
            var strBuilder = new StringBuilder();
            var enabledCharacterSets = stringConfiguration.Characters.Values.Where(set => set.Enabled);

            foreach (var set in enabledCharacterSets)
            {
                strBuilder.Append(set.Characters);
            }

            return strBuilder.ToString().ToCharArray();
        }
    }
}
