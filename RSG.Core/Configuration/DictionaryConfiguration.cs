using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Threading;

namespace RSG.Core.Configuration
{
    public class DictionaryConfiguration : IDictionaryConfiguration
    {
        private bool isFullyInitialized;
        private ObservableCollection<RsgDictionary> dictionaries;

        public DictionaryConfiguration()
        {
            isFullyInitialized = false;
        }

        /// <summary>
        /// Gets or sets a <see cref="IList{T}"/> of <see cref="RsgDictionary"/>(s).
        /// </summary>
        public ObservableCollection<RsgDictionary> Dictionaries
        {
            get
            {
                if (dictionaries is null)
                {
                    return new List<RsgDictionary>();
                }

                return dictionaries;
            }

            set
            {
                dictionaries = value;
            }
        }

        /// <summary>
        /// Gets or sets the source of the configuration file.
        /// <para>Must be a local source.</para>
        /// </summary>
        [JsonIgnore]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use spaces in generating words or not.
        /// </summary>
        public bool UseSpace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use noise in generating words or not.
        /// </summary>
        public bool UseNoise { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use aliteration in generating words or not.
        /// </summary>
        public bool UseAliteration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to capitailize each generated word or not.
        /// </summary>
        public bool CapitalizeEachWord { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the aliteration character used when generating words.
        /// <para>"\u0000" for none.</para>
        /// </summary>
        public char AliterationCharacter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the frequency in using aliteration words.
        /// </summary>
        public double AliterationFrequency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the frequency in generating words with noise.
        /// </summary>
        public double NoiseFrequency { get; set; }

        /// <summary>
        /// Gets or sets a range value indicating the number of noise to be generated per word.
        /// </summary>
        public Range NoisePerWordRange { get; set; }

        /// <summary>
        /// Gets or sets a range value indicating the number of aliterations to be generated.
        /// </summary>
        public Range AliterationRange { get; set; }

        /// <summary>
        /// Gets or sets a maximum value indicating the number of threads the service can use for the generation procedure.
        /// </summary>
        public int MaximumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="ThreadPriority"/> value indicating priority of the generation.
        /// </summary>
        public ThreadPriority Priority { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DictionaryConfiguration configuration &&
                   Comparer<IList<RsgDictionary>>.Default.Compare(this.Dictionaries, configuration.Dictionaries) == 0 &&
                   Source == configuration.Source &&
                   UseSpace == configuration.UseSpace &&
                   UseNoise == configuration.UseNoise &&
                   UseAliteration == configuration.UseAliteration &&
                   CapitalizeEachWord == configuration.CapitalizeEachWord &&
                   AliterationCharacter == configuration.AliterationCharacter &&
                   AliterationFrequency == configuration.AliterationFrequency &&
                   NoiseFrequency == configuration.NoiseFrequency &&
                   NoisePerWordRange.Equals(configuration.NoisePerWordRange) &&
                   AliterationRange.Equals(configuration.AliterationRange) &&
                   MaximumThreadCount == configuration.MaximumThreadCount &&
                   Priority == configuration.Priority;
        }
    }
}