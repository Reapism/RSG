using RSG.Library.Interfaces;
using RSG.Library.Models;
using RSG.Library.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSG.Library.Services
{
    internal class CharacterSetService : ICharacterSet
    {
        public IDictionary<string, CharacterSet> CharacterSets { get; set; }

        public char[] GetNewCharacterList()
        {
            return ScrambleCharacterList();
        }

        private char[] ScrambleCharacterList()
        {
            var allCharacters = CleanCharacterList().ToCharArray();
            ScrambleStringUtility.KnuthShuffle(allCharacters, RandomProvider.Random);

            return allCharacters;
        }

        private string CleanCharacterList()
        {
            var characterList = GetCharacterList();
            return characterList.Distinct().ToString();
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
