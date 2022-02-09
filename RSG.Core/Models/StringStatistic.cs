using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
{
    public class StringStatistic : IStatistic<IStringResult>, ICharacterFrequency
    {
        public IStringResult Result { get; }

        public IDictionary<char, int> OccurrencesByCharacter { get; set; }
    }
}
