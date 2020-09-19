using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// The necessary resulting information of generating random strings
    /// to construct a <see cref="StatisticsDetailed"/> instance.
    /// </summary>
    public class Result : ResultBase
    {
        /// <summary>
        /// Gets or sets the character list used to generate the strings.
        /// </summary>
        public string CharacterList { get; set; }

        /// <summary>
        /// Gets or sets the random strings produced by the generation.
        /// </summary>
        public IEnumerable<string> Strings { get; set; }

        /// <summary>
        /// Gets or sets the string length for this generation.
        /// </summary>
        public BigInteger StringLength { get; set; }
    }
}
