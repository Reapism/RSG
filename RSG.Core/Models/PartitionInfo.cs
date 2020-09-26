using RSG.Core.Extensions;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Models
{
    [DebuggerDisplay("NumberOfPartitions = {NumberOfPartitions}, TotalIterations = {TotalIterations}")]
    internal class PartitionInfo
    {
        public PartitionInfo()
        {
            TotalIterations = BigInteger.Zero;
        }

        internal BigInteger TotalIterations { get; set; }

        internal int NumberOfPartitions { get; set; }

        internal int FullPartitionSize { get; set; }

        internal int LastPartitionSize { get; set; }

        internal int ThreadCount { get; set; }

        internal int TotalThreadPoolThreads { get; set; }

        internal int TotalAsyncIOThreads { get; set; }

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

            ThreadPool.GetAvailableThreads(out var _, out var asyncIoThreads);
            var partitionInfo = new PartitionInfo()
            {
                TotalIterations = iterations,
                ThreadCount = threadCount,
                FullPartitionSize = fullPartSize,
                LastPartitionSize = (int)lastPartSize,
                NumberOfPartitions = threadCount,
                TotalThreadPoolThreads = ThreadPool.ThreadCount,
                TotalAsyncIOThreads = asyncIoThreads
            };


            return partitionInfo;
        }
    }
}
