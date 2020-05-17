using RSG.Core.Extensions;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSG.Core.Services
{
    public class DictionaryService
    {
        private SortedDictionary<string, RsgDictionary> dictionaries;
        private RsgDictionary selectedDictionary;
        private DictionaryServiceFactory dictionaryServiceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryService"/> class.
        /// </summary>
        /// <param name="factory"></param>
        public DictionaryService(DictionaryServiceFactory factory)
        {
            dictionaryServiceFactory = factory;
        }

        public void SelectDictionary(string dictionaryName)
        {
            bool success = dictionaries.TryGetValue(dictionaryName, out RsgDictionary dictionary);
            if (!success)
                throw new ArgumentException($"The {dictionaryName} dictionary was not found.");

            selectedDictionary = dictionary;
        }

        public RsgDictionary GetSelectedDictionary()
        {
            return selectedDictionary;
        }

        public async void AddDictionary(IRsgDictionary dictionaryToAdd)
        {
            if (dictionaries.Any(d => d.Key.Equals(dictionaryToAdd.Name, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Cannot add dictionary, name must be unique!");

            RsgDictionary model = new RsgDictionary()
            {
                Description = dictionaryToAdd.Description,
                Name = dictionaryToAdd.Name,
                IsSourceLocal = dictionaryToAdd.IsSourceLocal,
                Source = dictionaryToAdd.Source
            };

            IEnumerable<string> wordList = await WordListService.CreateWordList(dictionaryToAdd);

            model.WordList = wordList;
            model.Count = wordList.Count().ToBigInteger();

            // TryAdd due to asynchronous nature.
            dictionaries.TryAdd(model.Name, model);
        }
    }
}
