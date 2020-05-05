using RSG.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a collection of words.
    /// </summary>
    public struct Words
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Words"/> struct
        /// with a <paramref name="partitionSize"/>.
        /// </summary>
        /// <param name="partitionSize">The size of the partition.</param>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public Words(int partitionSize)
        {
            PartitionSize = partitionSize;
            IsNoisy = false;
            WordsPartition = new ConcurrentQueue<ConcurrentQueue<IWord>>();
            NoisyCharacterPositions = new Dictionary<int, char>();
        }

        /// <summary>
        /// Gets or sets a value that maps a partitioned collection of <see cref="IWord"/>(s).
        /// </summary>
        public ConcurrentQueue<ConcurrentQueue<IWord>> WordsPartition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating positions of characters.
        /// </summary>
        public IDictionary<int, char> NoisyCharacterPositions { get; set; }

        /// <summary>
        /// Gets or sets the size of each partition of words.
        /// </summary>
        public int PartitionSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Words"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; set; }

        public void AddWords(BigInteger numberOfWords)
        {
            var lastPartition = GetNumberOfPartitionsAndLastPartition(numberOfWords).Item2;
            var threads = GetThreads(numberOfWords, ThreadPriority.Normal);
            ExecuteThreads(threads, numberOfWords);

            foreach (var thread in threads)
            {
                //thread.
            }
        }

        /// <summary>
        /// The total number of words stored in all partition.
        /// </summary>
        /// <returns>The number of words.</returns>
        public BigInteger Count()
        {
            var partitionCount = BigInteger.Parse((WordsPartition.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            var lastPartitionCount = BigInteger.Parse(WordsPartition.Last().Count.ToString());
            var count = partitionCount + lastPartitionCount;
            return count;
        }

        /// <summary>
        /// Returns a specific partition of words given the partition index.
        /// </summary>
        /// <param name="partitionIndex">The index of the parent collection.</param>
        /// <returns>A specific partition </returns>
        public ConcurrentQueue<IWord> GetWordsAtIndex(int partitionIndex)
        {
            var count = WordsPartition.Count();
            var emptyQueue = new ConcurrentQueue<IWord>();

            if (partitionIndex < 0 || partitionIndex >= count)
                return emptyQueue;

            return WordsPartition.ElementAt(partitionIndex);
        }

        private Tuple<BigInteger, BigInteger> GetNumberOfPartitionsAndLastPartition(BigInteger numberOfWords)
        {
            var numberOfPartitions = BigInteger.DivRem(numberOfWords, BigInteger.Parse(PartitionSize.ToString()), out var remainder);
            var lastPartition = Tuple.Create(numberOfPartitions, remainder);

            return lastPartition;
        }

        private IEnumerable<Thread> GetThreads(BigInteger numberOfWords, ThreadPriority threadPriority)
        {
            var queue = new Queue<Thread>();
            var partitions = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            var fullPartitions = partitions.Item1 - BigInteger.One;

            for (var currentPartition = BigInteger.Zero; currentPartition < fullPartitions;)
            {
                queue.Enqueue(
                    new Thread(
                        new ThreadStart(PopulateWords))
                    {
                        IsBackground = true,
                        Priority = threadPriority,
                        Name = $"WordsGen_PartitionIndex_{currentPartition}",
                    });
            }

            queue.Enqueue(new Thread(new ThreadStart(PopulatePartialWords))
            {
                IsBackground = true,
                Priority = threadPriority,
                Name = $"WordsGen_PartitionIndex_{fullPartitions}",
            });

            return queue;
        }

        private void ExecuteThreads(IEnumerable<Thread> threads, BigInteger lastPartition)
        {
            var fullPartitionedThreads = threads.Count() - 1;
            for (var i = 0; i < fullPartitionedThreads; i++, threads.GetEnumerator().MoveNext())
            {
                var thread = threads.GetEnumerator().Current;
                thread.Start();
            }

            // start last thread with parameter
            threads.GetEnumerator().Current.Start(lastPartition);
        }

        private void PopulateWords()
        {

        }

        private void PopulatePartialWords()
        {

        }

    }
}
