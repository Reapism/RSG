using RSG.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Interfaces.Configuration
{
    public interface IDictionaryConfiguration
    {
        /// <summary>
        /// Gets or sets a <see cref="IList{T}"/> of <see cref="RsgDictionary"/>(s).
        /// </summary>
        IList<RsgDictionary> Dictionaries { get; set; }

        /// <summary>
        /// Gets or sets the source of the configuration file.
        /// <para>Must be a local source.</para>
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use spaces in generating words or not.
        /// </summary>
        bool UseSpace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use noise in generating words or not.
        /// </summary>
        bool UseNoise { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use aliteration in generating words or not.
        /// </summary>
        bool UseAliteration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to capitailize each generated word or not.
        /// </summary>
        bool CapitalizeEachWord { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the aliteration character used when generating words.
        /// <para>Typically, "\u0000" for none.</para>
        /// </summary>
        char AliterationCharacter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the frequency in using aliteration words.
        /// </summary>
        double AliterationFrequency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the frequency in noise generating words.
        /// </summary>
        double NoiseFrequency { get; set; }

        /// <summary>
        /// Gets or sets a range value indicating the number of noise to be generated per word.
        /// </summary>
        Range NoisePerWordRange { get; set; }

        /// <summary>
        /// Gets or sets a range value indicating the number of aliterations to be generated.
        /// </summary>
        Range AliterationRange { get; set; }

        /// <summary>
        /// Gets or sets a maximum value indicating the number of threads the service can use for the generation procedure.
        /// </summary>
        int MaximumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="ThreadPriority"/> value indicating priority of the generation.
        /// </summary>
        ThreadPriority Priority { get; set; }
    }
}
