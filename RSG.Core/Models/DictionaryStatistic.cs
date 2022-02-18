using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class DictionaryStatistic : IStatistic<IWordResult>, ICharacterFrequency, IIterationsFrequencyProvider
    {
        public IDictionary<char, int> OccurrencesByCharacter { get; set; }

        public IEnumerable<FrequencyUnit> IterationFrequencies { get; }

        public IWordResult Result { get; }
    }
}
