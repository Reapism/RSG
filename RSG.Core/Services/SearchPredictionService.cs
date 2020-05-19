using RSG.Core.Interfaces;
using RSG.Core.Models;
using System.Linq;
using System.Text;

namespace RSG.Core.Services
{
    /// <summary>
    /// Allows changing of the search model on the client side using this api.
    /// </summary>
    public class SearchPredictionService
    {
        public Search Search { get; set; }

        public SearchPredictionService(in Search search)
        {
            Search = search;
        }

        public void UpdateExpectedCharacterList(in ICharacterSets characterSet)
        {
            UpdateExpectedCharacterListHelper(characterSet);
        }

        private void UpdateExpectedCharacterListHelper(in ICharacterSets characterSet)
        {
            StringBuilder expectedCharacterListBuilder = new StringBuilder();

            foreach (CharacterSet set in characterSet.CharacterSets.Values)
            {
                bool containsSet = set.Characters.Any(ch => set.Characters.Contains(ch));
                if (containsSet)
                    expectedCharacterListBuilder.Append(set.Characters);
            }

            Search.ExpectedCharacterList = expectedCharacterListBuilder.ToString();
        }
    }
}
