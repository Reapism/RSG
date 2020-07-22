using System.Threading;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Represents a contract for a thread configuration.
    /// </summary>
    public interface IThreadConfiguration
    {
        /// <summary>
        /// Gets or sets the minimum number of threads to use.
        /// </summary>
        int MinimumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of threads to use.
        /// </summary>
        int MaximumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets the priority of the threads.
        /// </summary>
        ThreadPriority ThreadPriority { get; set; }
    }
}
