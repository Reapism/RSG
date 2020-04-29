using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents a particular result.
    /// </summary>
    public interface IStringResult : IResult
    {
        /// <summary>
        /// Gets or sets the characters list.
        /// </summary>
        string Characters { get; set; }

        /// <summary>
        /// Gets or sets the strings generated.
        /// </summary>
        IEnumerable<string> Strings { get; set; }

        /// <summary>
        /// Gets or sets the string length.
        /// </summary>
        BigInteger StringLength { get; set; }
    }
}
