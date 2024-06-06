using RSG.Core.Extensions;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Models
{
    [DebuggerDisplay("NumberOfPartitions = {NumberOfPartitions}, TotalIterations = {TotalIterations}")]
    internal class PartitionInfo
    {
        public PartitionInfo(BigInteger iterations)
        {
            TotalIterations = iterations;
        }

        internal BigInteger TotalIterations { get; }

        internal int NumberOfPartitions { get; private set; }

        internal int FullPartitionSize { get; private set; }

        internal int LastPartitionSize { get; private set; }

        internal int ThreadCount { get; private set; }

        internal int TotalThreadPoolThreads { get; private set; }

        internal int TotalAsyncIOThreads { get; private set; }

        internal static PartitionInfo Get(in BigInteger iterations, int threadCount)
        {
            var fullPartSize = int.Parse(BigInteger.DivRem(
                iterations,
                threadCount.ToBigInteger(),
                out var lastPartSize).ToString());

            if (lastPartSize == BigInteger.Zero)
            {
                lastPartSize = fullPartSize.ToBigInteger();
            }
            else
            {
                lastPartSize += fullPartSize.ToBigInteger();
            }

            ThreadPool.GetMaxThreads(out var workerThreads, out var completionThreads);
            var partitionInfo = new PartitionInfo(iterations)
            {
                ThreadCount = threadCount,
                FullPartitionSize = fullPartSize,
                LastPartitionSize = (int)lastPartSize,
                NumberOfPartitions = threadCount,
                TotalThreadPoolThreads = workerThreads,
                TotalAsyncIOThreads = completionThreads
            };


            return partitionInfo;
        }
    }
}
