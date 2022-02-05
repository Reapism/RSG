using RSG.Core.Interfaces.Request;
using RSG.Core.Models;

namespace RSG.Core.Interfaces.Result
{
    /// <summary>
    /// Represents the result of generating words from
    /// the dictionary form.
    /// </summary>
    public interface IDictionaryResult : IResult
    {
        /// <summary>
        /// Gets or sets the dictionary used to create generate this result.
        /// </summary>
        IDictionaryRequest Request { get; }

        /// <summary>
        /// Gets or sets the generated words.
        /// </summary>
        WordContainer Words { get; }
    }
}
