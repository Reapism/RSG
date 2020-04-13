using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a collection of words.
    /// </summary>
    public struct WordsCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordsCollection"/> struct
        /// with a <paramref name="partitionSize"/>.
        /// </summary>
        /// <param name="partitionSize">The size of the partition.</param>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public WordsCollection(int partitionSize)
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
        /// Gets or sets a value indicating whether this <see cref="WordsCollection"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; set; }

        public void AddWords(BigInteger numberOfWords)
        {
            var lastPartition = GetNumberOfPartitionsAndLastPartition(numberOfWords).Item2;
            var threads = GetThreads(numberOfWords);
            ExecuteThreads(threads, numberOfWords);
        }

        /// <summary>
        /// The total number of words stored in partition.
        /// </summary>
        public BigInteger Count()
        {
            var partitionCount = BigInteger.Parse((WordsPartition.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            var lastPartitionCount = BigInteger.Parse(WordsPartition.Last().Count.ToString());
            var count = partitionCount + lastPartitionCount;
            return count;
        }

        public ConcurrentQueue<IWord> GetWordsAtIndex(int index)
        {
            var count = WordsPartition.Count();
            var emptyQueue = new ConcurrentQueue<IWord>();

            if (index < 0 || index >= count)
                return emptyQueue;

            return WordsPartition.ElementAt(index);
        }

        private Tuple<BigInteger, BigInteger> GetNumberOfPartitionsAndLastPartition(BigInteger numberOfWords)
        {
            var numberOfPartitions = BigInteger.DivRem(numberOfWords, BigInteger.Parse(PartitionSize.ToString()), out var remainder);
            var lastPartition = Tuple.Create(numberOfPartitions, remainder);

            return lastPartition;
        }

        private IEnumerable<Thread> GetThreads(BigInteger numberOfWords)
        {
            var queue = new Queue<Thread>();
            var partitions = GetNumberOfPartitionsAndLastPartition(numberOfWords);
            var fullPartitions = partitions.Item1 - BigInteger.One;

            for (var currentPartition = BigInteger.Zero; currentPartition < fullPartitions;)
            {
                queue.Enqueue(new Thread(new ThreadStart(PopulateWords)));
            }

            queue.Enqueue(new Thread(new ThreadStart(PopulatePartialWords)));

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
