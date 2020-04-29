using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSG.Core.Services
{
    public class CharacterSetService : ICharacterSet
    {
        /// <inheritdoc/>
        public IDictionary<string, CharacterSet> CharacterSets { get; set; }

        public char[] GetNewCharacterList()
        {
            return ScrambleCharacterList();
        }

        private char[] ScrambleCharacterList()
        {
            var allCharacters = CleanCharacterList();
            var allCharactersArray = allCharacters.ToCharArray();
            ScrambleStringUtility.KnuthShuffle(allCharactersArray, RandomProvider.Random);

            return allCharactersArray;
        }

        private string CleanCharacterList()
        {
            var characterList = GetCharacterList();
            var distinctCharList = characterList.Distinct().ToArray();
            var returnStr = new string(distinctCharList);

            return returnStr;
        }

        private string GetCharacterList()
        {
            var strBuilder = new StringBuilder();
            var enabledCharacterSets = CharacterSets.Values.Where(set => set.Enabled);

            foreach (var set in enabledCharacterSets)
                strBuilder.Append(set.Characters);

            return strBuilder.ToString();
        }
    }
}
