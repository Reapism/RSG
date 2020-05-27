using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Services
{
    public class DictionaryThreadService
    {
        private readonly ThreadUtility threadUtility;
        private readonly BigInteger iterations;

        public DictionaryThreadService(ThreadUtility threadUtility)
        {
            this.threadUtility = threadUtility;
            this.iterations = iterations;
        }
        public BigInteger Iterations { get; set; }
        public int PartitionSize { get; set; }
        public int MaximumThreads { get; set; }

        public void ExecutePartition(Thread thread)
        {
            thread.
        }

        public void ExecuteLastPartition(Action action)
        {

        }

        public int GetThreadCount()
        {
            var numberOfThreads = BigInteger.Divide(iterations, threadUtility.Threads.ToBigInteger());

            return int.Parse(numberOfThreads.ToString());
        }

        public IEnumerable<Thread> GetThreads(ThreadPriority threadPriority, Action generateWordsAction)
        {
            var threadCount = GetThreadCount();
            var threads = new Queue<Thread>();
            int i;

            for (i = 0; i < threadCount - 1; i++)
            {
                var thread = new Thread(
                    new ThreadStart(generateWordsAction))
                {
                    Name = $"WordGen_{i}",
                    Priority = threadPriority,
                    IsBackground = true
                };
                threads.Enqueue(thread);
            }

            threads.Enqueue(new Thread(new ThreadStart(generateWordsAction)) { Name = $"WordGen_{i}"});

            return threads;
        }
    }
}
