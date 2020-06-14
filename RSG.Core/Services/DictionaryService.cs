using RSG.Core.Extensions;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Holds a collection of dictionaries, and provides methods for CRUD
    /// operations on the collection of <see cref="IRsgDictionary"/>(s).
    /// <para>Lazily initalizes the selected dictionary until <see cref="GetSelectedDictionary"/>
    /// or <see cref="SelectDictionary(string)"/> is called.</para>
    /// </summary>
    public class DictionaryService
    {
        // Dependencies
        private DictionaryServiceFactory dictionaryServiceFactory;
        private WordListService wordListService;

        // Members
        private ConcurrentDictionary<string, IRsgDictionary> dictionaries;
        private RsgDictionary selectedDictionary;
        private bool isFullyInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryService"/> class.
        /// </summary>
        /// <param name="dictionaryServiceFactory">A factory used to create
        /// members of the <see cref="DictionaryService"/>.</param>
        public DictionaryService(
            DictionaryServiceFactory dictionaryServiceFactory,
            WordListService wordListService,
            IDictionaryConfiguration dictionaryConfiguration)
        {
            this.dictionaryServiceFactory = dictionaryServiceFactory;
            this.wordListService = wordListService;
            isFullyInitialized = false;
        }

        /// <summary>
        /// Attempts to remove a dictionary from the internal collection.
        /// </summary>
        /// <param name="dictionary">The dictionary to remove.</param>
        /// <returns>Whether the dictionary was successfully removed.</returns>
        public bool RemoveDictionary(IRsgDictionary dictionary)
        {
            if (dictionary is null)
            {
                return false;
            }

            return dictionaries.TryRemove(dictionary.Name, out var _);
        }

        /// <summary>
        /// Attempts to add a dictionary to the internal collection.
        /// </summary>
        /// <param name="dictionary">The dictionary to add.</param>
        /// <returns>Whether the dictionary was successfully added.</returns>
        /// <exception cref="ArgumentException">If the <paramref name="dictionary"/> is <see langword="null"/>.</exception>
        public bool AddDictionary(IRsgDictionary dictionary)
        {
            if (dictionary is null && DoesDictionaryExist(dictionary.Name))
            {
                throw new ArgumentException("Cannot add dictionary, name must be case insensitively unique!");
            }

            // TryAdd due to asynchronous nature.
            return dictionaries.TryAdd(dictionary.Name, dictionary);
        }

        /// <summary>
        /// Selects a dictionary given the name of the dictionary.
        /// </summary>
        /// <param name="dictionaryName">The name of the dictionary.</param>
        /// <exception cref="ArgumentException">Thrown if the dictionary
        /// name was not found.</exception>
        public async void SelectDictionary(string dictionaryName)
        {
            if (!isFullyInitialized)
            {
                await LazyInitialize();
            }

            var success = dictionaries.TryGetValue(dictionaryName, out var dictionary);
            if (!success)
            {
                throw new ArgumentException($"The {dictionaryName} dictionary was not found.");
            }

            var words = await wordListService.CreateWordList(dictionary);

            selectedDictionary.WordList = wordListService.CreateIndexedWordList(words);
            selectedDictionary.Count = selectedDictionary.WordList.Count().ToBigInteger();
        }

        public async Task<RsgDictionary> GetSelectedDictionary()
        {
            if (!isFullyInitialized)
            {
                await LazyInitialize();
            }

            return selectedDictionary;
        }

        private async Task LazyInitialize()
        {
            // Ensure dictionaries is non null.
            if (dictionaries == null)
            {
                dictionaries = await dictionaryServiceFactory.CreateAsync();
            }

            // Ensure the dictionary instance is not null.
            if (selectedDictionary == null)
            {
                selectedDictionary = (RsgDictionary)dictionaries.FirstOrDefault().Value;
            }

            var words = await wordListService.CreateWordList(selectedDictionary);

            selectedDictionary.WordList = wordListService.CreateIndexedWordList(words);
            selectedDictionary.Count = selectedDictionary.WordList.Count().ToBigInteger();
            isFullyInitialized = true;
        }


        private bool DoesDictionaryExist(string dictionaryName)
        {
            return dictionaries.Any(d => d.Key.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
