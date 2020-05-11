using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    public class DictionaryService
    {
        private SortedDictionary<string, RsgDictionary> dictionaries;
        private RsgDictionary selectedDictionary;

        public DictionaryService(SortedDictionary<string, RsgDictionary> dictionaries)
        {
            this.dictionaries = dictionaries;
            selectedDictionary = dictionaries.FirstOrDefault().Value;
        }

        public void SelectDictionary(string dictionaryName)
        {
            var success = dictionaries.TryGetValue(dictionaryName, out var dictionary);
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

            var model = new RsgDictionary()
            {
                Description = dictionaryToAdd.Description,
                Name = dictionaryToAdd.Name,
                IsSourceLocal = dictionaryToAdd.IsSourceLocal,
                Source = dictionaryToAdd.Source
            };

            var wordList = await WordListService.CreateWordList(dictionaryToAdd);

            model.WordList = wordList;
            model.Count = wordList.Count().ToBigInteger();

            // TryAdd due to asynchronous nature.
            dictionaries.TryAdd(model.Name, model);
        }
    }
}
