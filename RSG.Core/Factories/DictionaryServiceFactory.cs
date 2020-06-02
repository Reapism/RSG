﻿using RSG.Core.Extensions;
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
        private readonly WordListService wordListService;

        public DictionaryServiceFactory(WordListService wordListService)
        {
            this.wordListService = wordListService;
        }

        public async Task<ConcurrentDictionary<string, IRsgDictionary>> CreateAsync()
        {
            IEnumerable<IRsgDictionary> defaultDictionaries = await GetDefaultDictionariesAsync();
            var dictionaries = new ConcurrentDictionary<string, IRsgDictionary>();

            foreach (IRsgDictionary dict in defaultDictionaries)
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

        private async Task<IEnumerable<IRsgDictionary>> GetDefaultDictionariesAsync()
        {
            using System.IO.Stream stream = await ResourceUtility.GetResourceStream("DefaultDictionaries.json");
            Queue<RsgDictionary> dictionaries = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            return dictionaries;
        }
    }
}
