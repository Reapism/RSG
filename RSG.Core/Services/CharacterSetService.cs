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
            string allCharacters = CleanCharacterList();
            char[] allCharactersArray = allCharacters.ToCharArray();
            ScrambleStringUtility.KnuthShuffle(allCharactersArray, RandomProvider.Random);

            return allCharactersArray;
        }

        private string CleanCharacterList()
        {
            string characterList = GetCharacterList();
            char[] distinctCharList = characterList.Distinct().ToArray();
            string returnStr = new string(distinctCharList);

            return returnStr;
        }

        private string GetCharacterList()
        {
            StringBuilder strBuilder = new StringBuilder();
            IEnumerable<CharacterSet> enabledCharacterSets = CharacterSets.Values.Where(set => set.Enabled);

            foreach (CharacterSet set in enabledCharacterSets)
                strBuilder.Append(set.Characters);

            return strBuilder.ToString();
        }
    }
}
