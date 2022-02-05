using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System;

namespace RSG.Core.Models.Results
{
    /// <summary>
    /// Represents properties of an RSG dictionary.
    /// </summary>
    public class DictionaryResult : Result<IDictionaryRequest>, IDictionaryResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryResult"/> class.
        /// </summary>
        /// <param name="request">The request used to generate this result.</param>
        /// <param name="duration">The duration of generating this result.</param>
        public DictionaryResult(IDictionaryRequest request, TimeSpan duration, WordContainer wordContainer)
            : base(request, duration)
        {
            Words = wordContainer;
        }

        /// <inheritdoc/>
        public IDictionaryRequest Request { get; }

        /// <inheritdoc/>
        public WordContainer Words { get; set; }
    }
}
