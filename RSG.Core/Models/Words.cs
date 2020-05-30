using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Utilities;
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
    public class Words
    {
        private readonly RandomWordGenerator randomWordGenerator;
        private readonly bool computePartitionSize;
        private BigInteger count;

        /// <summary>
        /// Initializes a new instance of the <see cref="Words"/> class
        /// if it's noisy and with a default partition size.
        /// </summary>
        /// <param name="randomWordGenerator">A <see cref="RandomWordGenerator"/> instance used 
        /// to populate the internal words.</param>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public Words(RandomWordGenerator randomWordGenerator, bool isNoisy)
            : this(randomWordGenerator, isNoisy, -1)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Words"/> class
        /// if it's noisy and with a partition size.
        /// </summary>
        /// <param name="randomWordGenerator">A <see cref="RandomWordGenerator"/> instance used 
        /// to populate the internal words.</param>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        /// <param name="partitionSize">The size of each partition. Pass -1 to automatically compute.</param>
        public Words(RandomWordGenerator randomWordGenerator, bool isNoisy, int partitionSize)
        {
            if (partitionSize == -1)
                computePartitionSize = true;

            PartitionSize = partitionSize;
            this.randomWordGenerator = randomWordGenerator;
            IsNoisy = isNoisy;
            count = BigInteger.Zero;

            // Queue is number of partitions, Dictionary is number of words K: index, V: IGeneratedWord
            PartitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
        }

        public BigInteger Count => count;

        /// <summary>
        /// Gets the size of each partition of words.
        /// </summary>
        public int PartitionSize { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Words"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; set; }

        /// <summary>
        /// Gets a value that maps a partitioned collection of <see cref="IGeneratedWord"/>(s).
        /// </summary>
        public ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>> PartitionedWords { get; internal set; }

        public void Add(in BigInteger numberOfWords)
        {

        }

        public void Clear()
        {
            foreach (ConcurrentDictionary<int, IGeneratedWord> dictionary in PartitionedWords)
            {
                dictionary.Clear();
            }
        }

        /// <summary>
        /// Returns a specific partition of words given the partition index.
        /// </summary>
        /// <param name="partitionIndex">The index of the parent collection.</param>
        /// <returns>A specific partition </returns>
        public ConcurrentDictionary<int, IGeneratedWord> GetWordsAtIndex(int partitionIndex)
        {
            var count = PartitionedWords.Count();
            var emptyQueue = new ConcurrentDictionary<int, IGeneratedWord>();

            if (partitionIndex < 0 || partitionIndex >= count)
                return emptyQueue;

            return PartitionedWords.ElementAt(partitionIndex);
        }

        /// <summary>
        /// Sets the new count of all the words stored.
        /// </summary>
        private void SetCount()
        {
            BigInteger partitionCount = BigInteger.Parse((PartitionedWords.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            var lastPartitionCount = PartitionedWords.Last().Count.ToBigInteger();

            count = partitionCount + lastPartitionCount;
        }

        private void InstantiatePartitionedWords(in BigInteger numberOfWords)
        {
            Tuple<int, BigInteger> tuple = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            var numberOfPartitions = tuple.Item1;

            for (BigInteger bi = BigInteger.Zero; bi < numberOfPartitions; bi++)
            {
                PartitionedWords.Enqueue(
                    new ConcurrentDictionary<int, IGeneratedWord>(
                        int.Parse(numberOfPartitions.ToString()),
                        PartitionSize));
            }
        }

        private Tuple<int, BigInteger> GetNumberOfPartitionsAndLastPartition(in BigInteger numberOfWords)
        {
            var numberOfPartitions = int.Parse(BigInteger.DivRem(numberOfWords, BigInteger.Parse(PartitionSize.ToString()), out BigInteger remainder).ToString());
            var lastPartition = Tuple.Create(numberOfPartitions, remainder);

            return lastPartition;
        }

        private IEnumerable<Thread> Execute(in BigInteger numberOfWords, ThreadPriority threadPriority)
        {
            var threads = new Queue<Thread>();
            Tuple<int, BigInteger> partitions = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            var fullPartitions = partitions.Item1 - 1;
            var currentPartition = 0;

            for (; currentPartition < fullPartitions; currentPartition++)
            {
                var thread =
                    new Thread(
                        new ThreadStart(PopulateWords))
                    {
                        IsBackground = true,
                        Priority = threadPriority,
                        Name = $"Words_PartitionIndex_{currentPartition}"
                    };

                threads.Enqueue(thread);
                thread.Start(PartitionSize);
            }

            var lastThread =
                    new Thread(
                        new ThreadStart(PopulateWords))
                    {
                        IsBackground = true,
                        Priority = threadPriority,
                        Name = $"Words_PartitionIndex_{currentPartition}"
                    };

            threads.Enqueue(lastThread);
            lastThread.Start(partitions.Item2);

            return threads;
        }

        private void PopulateWords()
        {

        }
    }
}
