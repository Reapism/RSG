using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class DictionaryStatistic : IStatistic<IDictionaryResult>, ICharacterFrequency, IIterationsFrequency
    {
        public IDictionary<char, int> OccurrencesByCharacter { get; set; }

        public IEnumerable<FrequencyUnit> IterationFrequencies { get; }

        public IDictionaryResult Result { get; }
    }
}
