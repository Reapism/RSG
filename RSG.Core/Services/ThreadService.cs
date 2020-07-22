using RSG.Core.Extensions;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using System;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contains information about threads on the executing
    /// environment.
    /// </summary>
    public sealed class ThreadService : IThreadService, IThreadConfiguration
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadService"/> class
        /// defaulting the minimum and maximum thread count.
        /// </summary>
        public ThreadService()
        {
            MinimumThreadCount = 1;
            MaximumThreadCount = GetEnvironmentThreadsCount();
        }

        /// <summary>
        /// Gets or sets the minimum number of threads to use.
        /// </summary>
        public int MinimumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of threads to use.
        /// </summary>
        public int MaximumThreadCount { get; set; }

        /// <summary>
        /// Gets or sets the priority of the threads.
        /// </summary>
        public ThreadPriority ThreadPriority { get; set; }

        /// <summary>
        /// Gets the number of threads needed to execute the workload on each thread
        /// based on value of the <paramref name="numberOfIterations"/> parameter.
        /// </summary>
        /// <param name="numberOfIterations">An arbitrarily large value representing the number
        /// of tasks in a sequence.</param>
        /// <returns>The number of threads.</returns>
        public int GetThreadsCount(BigInteger numberOfIterations)
        {
            return GetNumberOfThreadsEvenly(numberOfIterations);
        }

        /// <summary>
        /// Gets the number of logical threads on the executing environment.
        /// </summary>
        /// <returns>The number of threads on the execution environment.</returns>
        public int GetEnvironmentThreadsCount()
        {
            return Environment.ProcessorCount;
        }

        /// <summary>
        /// Gets the number of threads needed to evenly spread out the workload on each thread
        /// based on value of the <paramref name="numberOfIterations"/> parameter.
        /// </summary>
        /// <param name="numberOfIterations">An arbitrarily large value representing the number
        /// of tasks in a sequence.</param>
        /// <returns>The number of threads.</returns>
        public int GetNumberOfThreadsEvenly(BigInteger numberOfIterations)
        {
            var numberOfThreadsToCreate = BigInteger.Divide(numberOfIterations, GetEnvironmentThreadsCount().ToBigInteger());
            var threads = int.Parse(numberOfThreadsToCreate.ToString());

            if (threads >= MaximumThreadCount)
            {
                return MaximumThreadCount;
            }

            return threads;
        }

    }
}
