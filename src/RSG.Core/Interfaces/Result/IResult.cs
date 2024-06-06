using RSG.Core.Enums;
using System;
using System.Numerics;

namespace RSG.Core.Interfaces.Result
{
    /// <summary>
    /// Represents a simple contract for all results created by
    /// <see cref="RSG"/>.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets the duration for generating a result.
        /// </summary>
        TimeSpan Duration { get; }
    }
}