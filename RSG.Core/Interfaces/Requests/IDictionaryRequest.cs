using RSG.Core.Interfaces.Requests;
using RSG.Core.Models;

namespace RSG.Core.Interfaces.Request
{
    /// <summary>
    /// A minimum set contract for requesting to generate word(s).
    /// </summary>
    public interface IDictionaryRequest : IRequest
    {
        /// <summary>
        /// Gets or sets the dictionary used to make this request.
        /// </summary>
        RsgDictionary SelectedDictionary { get; set; }
    }
}
