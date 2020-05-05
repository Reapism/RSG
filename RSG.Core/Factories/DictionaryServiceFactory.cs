using Microsoft.Extensions.FileProviders;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Factories
{
    public class DictionaryServiceFactory
    {
        private static async Task<IEnumerable<RsgDictionary>> GetDefaultDictionaries()
        {
            var queue = new Queue<RsgDictionary>(10);

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "Resources");
            var fileInfo = embeddedProvider.GetFileInfo("DefaultDictionaries.json");
            using var stream = fileInfo.CreateReadStream();
            queue = await SerializationUtility.DeserializeJsonASync<Queue<RsgDictionary>>(stream);

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
