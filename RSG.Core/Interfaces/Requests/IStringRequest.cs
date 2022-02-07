using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Requests;
using System.Numerics;

namespace RSG.Core.Interfaces.Request
{
    /// <summary>
    /// A minimum set contract for requesting to generate string(s).
    /// </summary>
    public interface IStringRequest : IRequest, ICharacterSetProvider
    {
        /// <summary>
        /// Gets the length of the string.
        /// </summary>
        int StringLength { get; }
    }
}
