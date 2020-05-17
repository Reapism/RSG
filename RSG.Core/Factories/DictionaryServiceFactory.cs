using RSG.Core.Extensions;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Factories
{
    public class DictionaryServiceFactory
    {
        private async Task<IEnumerable<RsgDictionary>> GetDefaultDictionaries()
        {
            using System.IO.Stream stream = await ResourceUtility.GetResourceStream("DefaultDictionaries.json");
            Queue<RsgDictionary> dictionaries = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            foreach (RsgDictionary dictionary in dictionaries)
            {
                dictionary.WordList = await WordListService.CreateWordList(dictionary);
                dictionary.Count = dictionary.WordList.Count().ToBigInteger();
            }

            return dictionaries;
        }

        public async Task<SortedDictionary<string, RsgDictionary>> CreateAsync()
        {
            IEnumerable<RsgDictionary> dictionaries = await GetDefaultDictionaries();
            SortedDictionary<string, RsgDictionary> sortedDictionary = new SortedDictionary<string, RsgDictionary>();

            foreach (RsgDictionary dict in dictionaries)
            {
                sortedDictionary.Add(dict.Name, dict);
            }

            return sortedDictionary;
        }
    }
}
