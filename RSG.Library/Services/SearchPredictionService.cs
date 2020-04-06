using RSG.Library.Interfaces;
using RSG.Library.Models;
using System.Linq;
using System.Text;

namespace RSG.Library.Services
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

        public void UpdateExpectedCharacterList(in ICharacterSet characterSet)
        {
            UpdateExpectedCharacterListHelper(characterSet);
        }

        private void UpdateExpectedCharacterListHelper(in ICharacterSet characterSet)
        {
            var expectedCharacterListBuilder = new StringBuilder();

            foreach (var set in characterSet.CharacterSets.Values)
            {
                var containsSet = set.Characters.Any(ch => set.Characters.Contains(ch));
                if (containsSet)
                    expectedCharacterListBuilder.Append(set.Characters);
            }

            Search.ExpectedCharacterList = expectedCharacterListBuilder.ToString();
        }
    }
}
