using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSG.Core.Models.Results
{
    /// <summary>
    /// <see cref="StringResult"/>
    /// </summary>
    public class StringResult : Result<IStringRequest>, IStringResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringResult"/> class.
        /// </summary>
        /// <param name="request">The <see cref="IStringRequest"/> used for generating this result.</param>
        /// <param name="strings">The generated string(s).</param>
        /// <param name="duration">The duration of the generation.</param>
        public StringResult(IStringRequest request, IEnumerable<string> strings, TimeSpan duration)
            : base(request, duration)
        {
            Strings = strings;
        }

        /// <summary>
        /// Returns an empty string result.
        /// </summary>
        public static IStringResult Empty { get; private set; } = new StringResult(null, Enumerable.Empty<string>(), TimeSpan.Zero);

        /// <inheritdoc/>
        public IEnumerable<string> Strings { get; }
    }
}