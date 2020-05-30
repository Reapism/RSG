using RSG.Core.Interfaces;
using System.Collections.Generic;

namespace RSG.Core.Models
{
    public class GeneratedWord : IGeneratedWord
    {
        public IDictionary<int, IPositionCharacterPair> NoisyCharacterPositions { get; set; }

        public string Word { get; set; }
    }
}
