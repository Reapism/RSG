using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Factories
{
    public class DictionaryServiceFactory
    {
        public async Task<ConcurrentDictionary<string, IRsgDictionary>> CreateAsync()
        {
            var defaultDictionaries = await GetDefaultDictionariesAsync();
            var dictionaries = new ConcurrentDictionary<string, IRsgDictionary>();

            foreach (var dict in defaultDictionaries)
            {
                dictionaries.TryAdd(dict.Name, dict);
            }

            return dictionaries;
        }

        public async Task<bool> LoadFirstDictionaryAsync(RsgDictionary rsgDictionary)
        {
            rsgDictionary.WordList = await WordListService.CreateWordList(rsgDictionary);
            rsgDictionary.Count = rsgDictionary.WordList.Count().ToBigInteger();

            return rsgDictionary.WordList.Any() ? true : false;
        }

        private async Task<IEnumerable<IRsgDictionary>> GetDefaultDictionariesAsync()
        {
            using var stream = await ResourceUtility.GetResourceStream("DefaultDictionaries.json");
            Queue<RsgDictionary> dictionaries = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            return dictionaries;
        }
    }
}
