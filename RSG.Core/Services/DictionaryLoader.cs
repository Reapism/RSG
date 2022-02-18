using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Holds a collection of dictionaries, and provides methods for CRUD
    /// operations on the collection of <see cref="IRsgDictionary"/>(s).
    /// <para>Lazily initalizes the selected dictionary until <see cref="GetSelectedDictionaryAsync"/>
    /// or <see cref="SelectAsync(string)"/> is called.</para>
    /// </summary>
    public class DictionaryLoader : IDictionaryLoader
    {
        // Dependencies
        private readonly IWordListCreator wordListService;
        private readonly IDictionaryProvider dictionaryConfiguration;

        // Members
        private RsgDictionary selectedDictionary;
        private bool isFullyInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryLoader"/> class.
        /// </summary>
        /// <param name="wordListService">Used to construct the wordlist contained in
        /// the <see cref="IDictionaryProvider.Dictionaries"/>.</param>
        /// <param name="dictionaryConfiguration">The dictionary configuration.</param>
        public DictionaryLoader(
            IWordListCreator wordListService,
            IDictionaryProvider dictionaryConfiguration)
        {
            this.wordListService = wordListService;
            this.dictionaryConfiguration = dictionaryConfiguration;
            isFullyInitialized = false;
        }

        /// <summary>
        /// Attempts to remove a dictionary from the internal collection.
        /// </summary>
        /// <param name="dictionary">The dictionary to remove.</param>
        /// <returns>Whether the dictionary was successfully removed.</returns>
        public bool Remove(IRsgDictionary dictionary)
        {
            if (dictionary is null)
            {
                return false;
            }

            return dictionaryConfiguration.Dictionaries.Remove((RsgDictionary)dictionary);
        }

        /// <summary>
        /// Attempts to add a dictionary to the internal collection.
        /// </summary>
        /// <param name="dictionary">The dictionary to add.</param>
        /// <exception cref="ArgumentException">If the <paramref name="dictionary"/> is <see langword="null"/>.</exception>
        public void Add(IRsgDictionary dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (DoesDictionaryExist(dictionary.Name))
            {
                throw new ArgumentException("Cannot add dictionary, name must be case insensitively unique!");
            }

            dictionaryConfiguration.Dictionaries.Add((RsgDictionary)dictionary);
        }

        /// <summary>
        /// Selects a dictionary given the name of the dictionary.
        /// </summary>
        /// <param name="dictionaryName">The name of the dictionary.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown if the dictionary
        /// name was not found.</exception>
        public async Task SelectAsync(string dictionaryName, CancellationToken cancellationToken)
        {
            if (!isFullyInitialized)
            {
                await LazyInitialize(cancellationToken);
            }

            var dictionary = dictionaryConfiguration.Dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary is null)
            {
                throw new ArgumentException($"The {dictionaryName} dictionary was not found.");
            }

            await GetWordListFor(cancellationToken);
        }

        /// <summary>
        /// Returns the selected dictionary in an asynchronous operation.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<RsgDictionary> GetSelectedDictionaryAsync(CancellationToken cancellationToken = default)
        {
            if (!isFullyInitialized)
            {
                await LazyInitialize(cancellationToken);
            }

            return selectedDictionary;
        }

        private async Task LazyInitialize(CancellationToken cancellationToken)
        {
            // Ensure the dictionary instance is not null.
            cancellationToken.ThrowIfCancellationRequested();
            if (selectedDictionary == null)
            {
                selectedDictionary = dictionaryConfiguration.Dictionaries.FirstOrDefault();
                if (selectedDictionary is null)
                {
                    throw new ApplicationException("Dictionaries is null from the dictionary configuration.");
                }
            }

            await GetWordListFor(cancellationToken);
            isFullyInitialized = true;
        }

        private async Task GetWordListFor(CancellationToken cancellationToken)
        {
            var wordList = await wordListService.CreateWordListAsync(selectedDictionary, cancellationToken);

            selectedDictionary.SetWordList(wordList);
        }

        private bool DoesDictionaryExist(string dictionaryName)
        {
            return dictionaryConfiguration.Dictionaries.Any(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
