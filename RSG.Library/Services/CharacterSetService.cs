using RSG.Library.Models;
using RSG.Library.Utilities;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace RSG.Library.Services
{
    internal class CharacterSetService
    {
        public ConcurrentDictionary<string, CharacterSet> CharacterSets { get; set; }

        public char[] GetNewCharacterList()
        {
            return ScrambleCharacterList();
        }

        private char[] ScrambleCharacterList()
        {
            var allCharacters = CleanCharacterList().ToCharArray();
            ScrambleStringUtility.KnuthShuffle(allCharacters, RandomProvider.Rnd);

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
            {
                strBuilder.Append(set.Characters);
            }

            return strBuilder.ToString();
        }

    }
}
