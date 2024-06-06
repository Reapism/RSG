﻿using System.ComponentModel;

namespace RSG.Core.Enums
{
    /// <summary>
    /// Represents possible values of randomization
    /// methods for the generators.
    /// </summary>
    public enum RandomizationType
    {
        /// <summary>
        /// Pseudorandom randomization.
        /// </summary>
        [Description("Pseudorandom")]
        Pseudorandom,

        /// <summary>
        /// Cryptographic Randomization
        /// </summary>
        [Description("Cryptographic Random")]
        CryptographicRandom
    }
}
