using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Factories
{
    public class DictionaryServiceFactory
    {
        private readonly WordListService wordListService;
        private readonly IDictionaryConfiguration dictionaryConfiguration;

        public DictionaryServiceFactory(
            WordListService wordListService,
            IDictionaryConfiguration dictionaryConfiguration)
        {
            this.wordListService = wordListService;
            this.dictionaryConfiguration = dictionaryConfiguration;
        }

        /// <summary>
        /// Creates an instance of <see cref="ConcurrentDictionary{TKey, TValue}"/>.
        /// <para>If an existing <see cref="IDictionaryConfiguration"/> exists, it will
        /// create it from there, otherwise creates from the default dictionaries.</para>
        /// </summary>
        /// <returns>An instance of <see cref="ConcurrentDictionary{TKey, TValue}"/>.</returns>
        public async Task<ConcurrentDictionary<string, IRsgDictionary>> CreateAsync()
        {
            IEnumerable<IRsgDictionary> dictionariesToAdd;
            var dictionaries = new ConcurrentDictionary<string, IRsgDictionary>();

            if (dictionaryConfiguration.Dictionaries is null ||
                !dictionaryConfiguration.Dictionaries.Any())
            {
                dictionariesToAdd = await CreateDefaultDictionariesAsync();
            }
            else
            {
                dictionariesToAdd = CreateDictionaryFromConfiguration();
            }

            foreach (var dict in dictionariesToAdd)
            {
                dictionaries.TryAdd(dict.Name, dict);
            }

            return dictionaries;
        }

        public async Task<bool> LoadFirstDictionaryAsync(RsgDictionary rsgDictionary)
        {
            var wordList = await wordListService.CreateWordList(rsgDictionary);
            rsgDictionary.WordList = wordListService.CreateIndexedWordList(wordList);
            rsgDictionary.Count = rsgDictionary.WordList.Count().ToBigInteger();

            return rsgDictionary.WordList.Any() ? true : false;
        }

        private async Task<IEnumerable<IRsgDictionary>> CreateDefaultDictionariesAsync()
        {
            using var stream = await ResourceUtility.GetResourceStream("DefaultDictionaries.json");
            var dictionaries = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            return dictionaries;
        }

        private IEnumerable<IRsgDictionary> CreateDictionaryFromConfiguration()
        {
            return dictionaryConfiguration.Dictionaries;
        }
    }
}
