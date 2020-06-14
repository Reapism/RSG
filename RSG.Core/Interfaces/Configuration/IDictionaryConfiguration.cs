using RSG.Core.Models;
using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Interfaces.Configuration
{
    public interface IDictionaryConfiguration
    {
        IEnumerable<IRsgDictionary> Dictionaries { get; set; }

        string Source { get; set; }

        bool UseSpace { get; set; }

        bool CapitalizeEachWord { get; set; }

        bool UseNoise { get; set; }

        char AliterationCharacter { get; set; }

        double AliterationFrequency { get; set; }

        double NoiseFrequency { get; set; }

        int MaximumThreadCount { get; set; }

        int PartitionSize { get; set; }

        ThreadPriority Priority { get; set; }
    }
}
