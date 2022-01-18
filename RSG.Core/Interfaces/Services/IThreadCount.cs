using System.Numerics;

namespace RSG.Core.Interfaces.Services
{
    /// <summary>
    /// Provides contracts for getting the thread count given an arbitrary criteria.
    /// </summary>
    public interface IThreadCount
    {
        /// <summary>
        /// Gets the number of threads to use given the number
        /// of iterations for an operations.
        /// </summary>
        /// <param name="numberOfIterations">The number of iterations.</param>
        /// <returns>The number of threads given the iterations.</returns>
        int GetThreadsCount(BigInteger numberOfIterations);
    }
}
