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

        // TODO: Make this stateless.
        private ICharacterSet characterSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetService"/> class.
        /// </summary>
        /// <param name="stringConfiguration">An instance of the <see cref="IStringConfiguration"/>.</param>
        /// <param name="shuffle">An instance of <see cref="IShuffle{T}"/> of type
        /// <see langword="char"/>.</param>
        public CharacterSetService(
            IStringConfiguration stringConfiguration,
            ICharacterSet characterSet,
            IShuffle<char> shuffle)
        {
            this.stringConfiguration = stringConfiguration;
            this.shuffle = shuffle;
            this.characterSet = characterSet;

            Create();
        }

        /// <summary>
        /// Gets the character list from this instance.
        /// </summary>
        public char[] CharacterList => GetNewCharacterList();

        private void Create()
        {
            if (stringConfiguration.CharacterSet is null ||
                !stringConfiguration.CharacterSet.Characters.Any())
            {
                CreateDefault();
            }
            else
            {
                CreateFromConfiguration();
            }
        }

        private void CreateDefault()
        {
            characterSet.Characters.Add(
                CharacterSetConstants.Lowercase,
                new SingleCharacterSet(CharacterSetConstants.LowercaseSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Uppercase,
                new SingleCharacterSet(CharacterSetConstants.UppercaseSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Numbers,
                new SingleCharacterSet(CharacterSetConstants.NumbersSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Punctuation,
                new SingleCharacterSet(CharacterSetConstants.PunctuationSet, false));
            characterSet.Characters.Add(
                CharacterSetConstants.Space,
                new SingleCharacterSet(CharacterSetConstants.SpaceSet, false));
            characterSet.Characters.Add(
                CharacterSetConstants.Symbols,
                new SingleCharacterSet(CharacterSetConstants.SymbolsSet, false));
        }

        private void CreateFromConfiguration()
        {
            characterSet = stringConfiguration.CharacterSet;
        }


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
            var enabledCharacterSets = characterSet.Characters.Values.Where(set => set.Enabled);

            foreach (var set in enabledCharacterSets)
            {
                strBuilder.Append(set.Characters);
            }

            return strBuilder.ToString().ToCharArray();
        }
    }
}
