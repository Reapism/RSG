using System.Collections.Generic;
using RSG.Core.Models;

namespace RSG.Core.Services
{
    public class DictionaryService
    {
        private IEnumerable<RsgDictionary> Dictionaries { get; set; }
        private RsgDictionary SelectedDictionary { get; set; }

        public DictionaryService()
        {

        }

    }
}
