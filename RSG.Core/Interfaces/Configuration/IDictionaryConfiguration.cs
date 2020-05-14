using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Configuration
{
    public interface IDictionaryConfiguration
    {
        IEnumerable<RsgDictionary> Dictionaries { get; set; }

        string Source { get; set; }

        bool UseSpace { get; set; }

        bool CapitalizeEachWord { get; set; }

        char AliterationCharacter { get; set; }

        double AliterationFrequency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool UseNoise { get; set; }

        double NoiseFrequency { get; set; }
    }
}
