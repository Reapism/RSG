using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Configuration
{
    public class DictionaryConfiguration : IDictionaryConfiguration
    {
        public IEnumerable<RsgDictionary> Dictionaries { get; set; }

        /// <summary>
        /// Gets or sets source of the serialized configuration.
        /// <para>Must be a local file.</para>
        /// </summary>
        public string Source { get; set; }

        public bool UseSpace { get; set; }

        public bool CapitalizeEachWord { get; set; }

        public char AliterationCharacter { get; set; }

        public double AliterationFrequency { get; set; }

        public bool UseNoise { get; set; }

        public double NoiseFrequency { get; set; }

        public int MaximumThreadCount { get; set; }

        public ThreadPriority Priority { get; set; }

        /// <summary>
        /// -1 if <see cref="Words"/> should automatically determine.
        /// </summary>
        public int PartitionSize { get; set; }
    }
}