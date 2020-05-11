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
        private static async Task<IEnumerable<RsgDictionary>> GetDefaultDictionaries()
        {
            using var stream = await ResourceUtility.GetResourceStream("DefaultDictionaries.json");
            var queue = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            foreach (var dictionary in queue)
            {
                dictionary.WordList = await WordListService.CreateWordList(dictionary);
                dictionary.Count = dictionary.WordList.Count().ToBigInteger();
            }

            return queue;
        }

        private static async Task<DictionaryService> InitializeAsync()
        {
            var dictionaries = await GetDefaultDictionaries();
            var sortedDictionary = new SortedDictionary<string, RsgDictionary>();

            foreach (var dict in dictionaries)
            {
                sortedDictionary.Add(dict.Name, dict);
            }

            var service = new DictionaryService(sortedDictionary);

            return service;
        }

        public static async Task<DictionaryService> CreateAsync()
        {
            var dictionaryService = await InitializeAsync();
            return dictionaryService;
        }
    }
}
