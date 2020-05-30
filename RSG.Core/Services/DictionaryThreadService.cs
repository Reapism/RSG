using RSG.Core.Extensions;
using RSG.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Services
{
    public class DictionaryThreadService
    {
        private readonly IThreadService threadService;
        private readonly BigInteger iterations;

        public DictionaryThreadService(IThreadService threadService)
        {
            this.threadService = threadService;
        }

        public BigInteger Iterations { get; set; }
        public int PartitionSize { get; set; }
        public int MaximumThreads { get; set; }

        public void Execute(Thread thread)
        {
            thread.Start();
        }

        public int ComputeThreadCount()
        {
            var numberOfThreadsToCreate = BigInteger.Divide(iterations, threadService.GetThreadsCount().ToBigInteger());

            return int.Parse(numberOfThreadsToCreate.ToString());
        }

        public IEnumerable<Thread> GetThreads(ThreadPriority threadPriority, Action generateWordsAction)
        {
            var threadCount = ComputeThreadCount();
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

            threads.Enqueue(new Thread(new ThreadStart(generateWordsAction)) { Name = $"WordGen_{i}" });

            return threads;
        }
    }
}
