using RSG.Core.Interfaces.Result;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models.Result
{
    public class StringResult : ResultBase, IStringResult
    {
        /// <summary>
        /// Gets or sets the character list used to generate the strings.
        /// </summary>
        public string Characters { get; set; }

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