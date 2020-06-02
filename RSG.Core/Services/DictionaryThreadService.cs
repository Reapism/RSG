using RSG.Core.Extensions;
using RSG.Core.Interfaces.Configuration;
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

        public DictionaryThreadService(IThreadService threadService, IDictionaryConfiguration dictionaryConfiguration)
        {
            this.threadService = threadService;
            MaximumThreads = dictionaryConfiguration.MaximumThreadCount;
        }

        public BigInteger Iterations { get; set; }

        public int MaximumThreads { get; }

        public void Execute(Thread thread)
        {
            thread.Start();
        }

        //public IEnumerable<Thread> GetThreads(in BigInteger numberOfIterations, ThreadPriority threadPriority)
        //{
        //    var threadCount = ComputeThreadCount(numberOfIterations);
        //    var threads = new Queue<Thread>();
        //    int i;

        //    for (i = 0; i < threadCount - 1; i++)
        //    {
        //        var thread = new Thread(
        //            new ThreadStart(generateWordsAction))
        //        {
        //            Name = $"WordGen_{i}",
        //            Priority = threadPriority,
        //            IsBackground = true
        //        };
        //        threads.Enqueue(thread);
        //    }

        //    threads.Enqueue(new Thread(new ThreadStart(generateWordsAction)) { Name = $"WordGen_{i}" });

        //    return threads;
        //}

        public int ComputePartitionSize(in BigInteger numberOfIterations)
        {
            var numberOfThreadsToCreate = BigInteger.Divide(numberOfIterations, threadService.GetThreadsCount().ToBigInteger());

            return int.Parse(numberOfThreadsToCreate.ToString());
        }

    }
}
