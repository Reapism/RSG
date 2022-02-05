using System.Numerics;

namespace RSG.Core.Interfaces.Requests
{
    /// <summary>
    /// Marks a specific
    /// type being a request.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Gets the number of iterations for this request.
        /// </summary>
        BigInteger Iterations { get; }
    }
}
