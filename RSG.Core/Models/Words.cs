using RSG.Core.Extensions;
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
    /// Represents a collection of generated words.
    /// </summary>
    public struct Words
    {
        private const int DefaultPartitionSize = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Words"/> struct
        /// and whether its <paramref name="isNoisy"/> or not.
        /// </summary>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public Words(bool isNoisy)
        {
            PartitionSize = DefaultPartitionSize;
            IsNoisy = isNoisy;

            // Queue is number of partitions, Dictionary is number of words K: index, V: IGeneratedWord
            PartitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
        }

        /// <summary>
        /// Gets or sets a value that maps a partitioned collection of <see cref="IGeneratedWord"/>(s).
        /// </summary>
        public ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>> PartitionedWords { get; set; }

        /// <summary>
        /// Gets the size of each partition of words.
        /// </summary>
        public int PartitionSize { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Words"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; set; }

        public async void AddWords(BigInteger numberOfWords)
        {
            InstantiatePartitionedWords(numberOfWords);

            IEnumerable<Thread> threads = GetThreads(PartitionedWords.Count(), ThreadPriority.Normal);
            ExecuteThreads(threads, numberOfWords);
        }

        private void InstantiatePartitionedWords(in BigInteger numberOfWords)
        {
            var tuple = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            var numberOfPartitions = tuple.Item1;

            for (var bi = BigInteger.Zero; bi < numberOfPartitions; bi++)
            {
                PartitionedWords.Enqueue(
                    new ConcurrentDictionary<int, IGeneratedWord>(
                        int.Parse(numberOfPartitions.ToString()),
                        PartitionSize));
            }
        }

        /// <summary>
        /// The total number of words stored in all partition.
        /// </summary>
        /// <returns>The number of words.</returns>
        public BigInteger Count()
        {
            BigInteger partitionCount = BigInteger.Parse((PartitionedWords.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            BigInteger lastPartitionCount = PartitionedWords.Last().Count.ToBigInteger());

            BigInteger count = partitionCount + lastPartitionCount;

            return count;
        }

        /// <summary>
        /// Returns a specific partition of words given the partition index.
        /// </summary>
        /// <param name="partitionIndex">The index of the parent collection.</param>
        /// <returns>A specific partition </returns>
        public ConcurrentDictionary<int, IGeneratedWord> GetWordsAtIndex(int partitionIndex)
        {
            int count = PartitionedWords.Count();
            var emptyQueue = new ConcurrentDictionary<int, IGeneratedWord>();

            if (partitionIndex < 0 || partitionIndex >= count)
                return emptyQueue;

            return PartitionedWords.ElementAt(partitionIndex);
        }

        private Tuple<BigInteger, BigInteger> GetNumberOfPartitionsAndLastPartition(in BigInteger numberOfWords)
        {
            BigInteger numberOfPartitions = BigInteger.DivRem(numberOfWords, BigInteger.Parse(PartitionSize.ToString()), out BigInteger remainder);
            Tuple<BigInteger, BigInteger> lastPartition = Tuple.Create(numberOfPartitions, remainder);

            return lastPartition;
        }

        private IEnumerable<Thread> GetThreads(in BigInteger numberOfWords, ThreadPriority threadPriority)
        {
            Queue<Thread> queue = new Queue<Thread>();
            Tuple<BigInteger, BigInteger> partitions = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            BigInteger fullPartitions = partitions.Item1 - BigInteger.One;

            for (BigInteger currentPartition = BigInteger.Zero; currentPartition < fullPartitions;)
            {
                queue.Enqueue(
                    new Thread(
                        new ThreadStart(PopulateWords))
                    {
                        IsBackground = true,
                        Priority = threadPriority,
                        Name = $"Words_PartitionIndex_{currentPartition}",
                    });
            }

            queue.Enqueue(new Thread(new ThreadStart(PopulatePartialWords))
            {
                IsBackground = true,
                Priority = threadPriority,
                Name = $"Words_PartitionIndex_{fullPartitions}",
            });

            return queue;
        }

        private void ExecuteThreads(IEnumerable<Thread> threads, in BigInteger lastPartition)
        {
            int fullPartitionedThreads = threads.Count() - 1;

            for (int i = 0; i < fullPartitionedThreads; i++, threads.GetEnumerator().MoveNext())
            {
                Thread thread = threads.GetEnumerator().Current;
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
