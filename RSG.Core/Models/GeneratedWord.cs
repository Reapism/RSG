using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Core.Models
{
    public class GeneratedWord : IGeneratedWord
    {
        public IDictionary<int, char> NoisyCharacterPositions { get; set; }

        public string Word { get; set; }
    }
}
