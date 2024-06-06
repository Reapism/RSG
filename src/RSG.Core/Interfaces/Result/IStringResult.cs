using RSG.Core.Interfaces.Request;
using System.Collections.Generic;

namespace RSG.Core.Interfaces.Result
{
    /// <summary>
    /// Represents a particular result.
    /// </summary>
    public interface IStringResult : IResult
    {
        /// <summary>
        /// Gets the request that was used to create this result.
        /// </summary>
        IStringRequest Request { get; }

        /// <summary>
        /// Gets the strings generated.
        /// </summary>
        IEnumerable<string> Strings { get; }
    }
}
