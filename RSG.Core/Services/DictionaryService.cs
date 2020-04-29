using System.Collections.Generic;
using System.Linq;
using RSG.Core.Models;
using System;

namespace RSG.Core.Services
{
    public class DictionaryService
    {
        private IEnumerable<RsgDictionary> dictionaries;
        private RsgDictionary selectedDictionary;

        public DictionaryService()
        {
            PopulateDictionaries();
        }

        public RsgDictionary GetDictionary()
        {
            return GetSelectedDictionary();
        }

        public void SelectDictionary(string dictionaryName)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name == dictionaryName);
            if (dictionary != null)
                selectedDictionary = dictionary;

            throw new ArgumentException($"The {dictionaryName} dictionary was not found.");
        }

        private RsgDictionary GetSelectedDictionary()
        {
            return selectedDictionary;
        }

        private void PopulateDictionaries()
        {

        }
    }
}
