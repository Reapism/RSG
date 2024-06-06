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
    public sealed class ThreadBalancer : IThreadBalancer, IThreadConfiguration
    {
        private int minimumThreadCount;
        private int maximumThreadCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadBalancer"/> class
        /// defaulting the minimum and maximum thread count.
        /// </summary>
        public ThreadBalancer()
        {
            MinimumThreadCount = 1;
            MaximumThreadCount = GetEnvironmentThreadsCount();
        }

        /// <summary>
        /// Gets or sets the minimum number of threads to use.
        /// <para>Must be larger than <see langword="1"/> and less
        /// than or equal to maximum thread count.</para>
        /// </summary>
        public int MinimumThreadCount
        {
            get => minimumThreadCount;
            set
            {
                if (value < 1 || value > maximumThreadCount)
                {
                    minimumThreadCount = 1;
                }

                minimumThreadCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of threads to use.
        /// <para>Must be larger or equal to the <see cref="MinimumThreadCount"/>.</para>
        /// </summary>

        public int MaximumThreadCount
        {
            get => maximumThreadCount;
            set
            {
                if (value <= minimumThreadCount)
                {
                    return;
                }

                maximumThreadCount = value;
            }
        }

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
        public int GetThreadCountByIterations(BigInteger numberOfIterations)
        {
            return GetThreadCountByIterationsInternal(numberOfIterations);
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
        private int GetThreadCountByIterationsInternal(BigInteger numberOfIterations)
        {
            var numberOfThreadsToCreate = BigInteger.Divide(numberOfIterations, GetEnvironmentThreadsCount().ToBigInteger());
            var threads = int.Parse(numberOfThreadsToCreate.ToString());

            if (threads < MinimumThreadCount)
            {
                return MinimumThreadCount;
            }

            if (threads >= MaximumThreadCount)
            {
                return MaximumThreadCount;
            }

            return threads;
        }

    }
}
