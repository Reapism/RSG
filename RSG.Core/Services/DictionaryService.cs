using RSG.Core.Extensions;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Holds a collection of dictionaries, and provides methods for CRUD
    /// operations on the collection of <see cref="RsgDictionary"/>(s).
    /// </summary>
    public class DictionaryService
    {
        // Dependencies
        private DictionaryServiceFactory dictionaryServiceFactory;
        private WordListService wordListService;

        // Members
        private ConcurrentDictionary<string, IRsgDictionary> dictionaries;
        private RsgDictionary selectedDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryService"/> class.
        /// </summary>
        /// <param name="dictionaryServiceFactory">A factory used to create
        /// members of the <see cref="DictionaryService"/>.</param>
        public DictionaryService(
            DictionaryServiceFactory dictionaryServiceFactory,
            WordListService wordListService)
        {
            this.dictionaryServiceFactory = dictionaryServiceFactory;
            this.wordListService = wordListService;
        }

        /// <summary>
        /// Selects a dictionary given the name of the dictionary.
        /// </summary>
        /// <param name="dictionaryName">The name of the dictionary.</param>
        /// <exception cref="ArgumentException">Thrown if the dictionary
        /// name was not found.</exception>
        public async void SelectDictionary(string dictionaryName)
        {
            // Ensure dictionaries is non null.
            if (dictionaries == null)
                dictionaries = await dictionaryServiceFactory.CreateAsync();

            // Ensure the dictionary instance is not null.
            if (GetSelectedDictionary() == null)
                selectedDictionary = (RsgDictionary)dictionaries.FirstOrDefault().Value;

            bool success = dictionaries.TryGetValue(dictionaryName, out var dictionary);
            if (!success)
                throw new ArgumentException($"The {dictionaryName} dictionary was not found.");

            var words = await wordListService.CreateWordList(dictionary);

            selectedDictionary.WordList = wordListService.CreateIndexedWordList(words);
            selectedDictionary.Count = selectedDictionary.WordList.Count().ToBigInteger();
        }

        public RsgDictionary GetSelectedDictionary()
        {
            return selectedDictionary;
        }

        public async Task<bool> AddDictionaryAsync(IRsgDictionary dictionaryToAdd)
        {
            if (!DoesDictionaryExist(dictionaryToAdd.Name))
                throw new ArgumentException("Cannot add dictionary, name must be unique!");

            // TryAdd due to asynchronous nature.
            return dictionaries.TryAdd(dictionaryToAdd.Name, dictionaryToAdd);
        }

        public async Task<bool> RemoveDictionaryAsync(IRsgDictionary dictionaryToRemove)
        {
            return dictionaries.TryRemove(dictionaryToRemove.Name, out var dictionary);
        }

        private bool DoesDictionaryExist(string dictionaryName)
        {
            return dictionaries.Any(d => d.Key.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
