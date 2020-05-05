using Microsoft.Extensions.FileProviders;
using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    public class DictionaryService
    {
        private SortedDictionary<string, RsgDictionary> dictionaries;
        private IRsgDictionary selectedDictionary;

        public DictionaryService(SortedDictionary<string, RsgDictionary> dictionaries)
        {
            this.dictionaries = dictionaries;
        }

        public IRsgDictionary GetDictionary()
        {
            return GetSelectedDictionary();
        }

        public void SelectDictionary(string dictionaryName)
        {
            var success = dictionaries.TryGetValue(dictionaryName, out var dictionary);
            if (success)
                throw new ArgumentException($"The {dictionaryName} dictionary was not found.");

            selectedDictionary = dictionary;

        }

        private IRsgDictionary GetSelectedDictionary()
        {
            return selectedDictionary;
        }

        public void AddDictionary(RsgDictionary dictionaryToAdd)
        {
            var succedded = dictionaries.TryAdd(dictionaryToAdd.Name, dictionaryToAdd);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<IEnumerable<RsgDictionary>> GetDefaultDictionaries()
        {
            var queue = new Queue<RsgDictionary>(10);

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "Resources");
            var fileInfo = embeddedProvider.GetFileInfo("DefaultDictionaries.json");
            using var stream = fileInfo.CreateReadStream();
            queue = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

            return queue;
        }
    }
}
