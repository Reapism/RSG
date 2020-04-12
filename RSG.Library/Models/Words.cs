using RSG.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a collection of words.
    /// </summary>
    public class Words
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Words"/> class
        /// with a <paramref name="partitionSize"/>.
        /// </summary>
        /// <param name="partitionSize">The size of the partition.</param>
        public Words(int partitionSize, bool isNoisy)
        {
            PartitionSize = partitionSize;
            IsNoisy = isNoisy;
            _wordsPartition = new ConcurrentQueue<ConcurrentQueue<IWord>>();
        }

        /// <summary>
        /// Maps a partitioned collection of <see cref="LightWord"/>(s).
        /// </summary>
        private IEnumerable<ConcurrentQueue<IWord>> _wordsPartition;


        public int PartitionSize { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Words"/> instance contains noise.
        /// </summary>
        public bool IsNoisy { get; set; }


        public ConcurrentQueue<IWord> GetWordsAtIndex(int index)
        {
            if (IsNoisy)
            {
                return GetNoisyWordsAtIndex(index);
            }

            return GetLightWordsAtIndex(index).Cast<ConcurrentQueue<LightWord>>();
        }

        private ConcurrentQueue<LightWord> GetLightWordsAtIndex(int index)
        {
            var count = _lightWordsPartition.Count();
            var emptyQueue = new ConcurrentQueue<LightWord>();

            if (index < 0 || index >= count)
                return emptyQueue;

            return _lightWordsPartition.ElementAt(1);
        }

        private ConcurrentQueue<IWord> GetNoisyWordsAtIndex(int index)
        {

        }

        /// <summary>
        /// The total number of words stored in partition.
        /// </summary>
        public BigInteger Count()
        {
            if (IsNoisy)
                return NoisyCount();

            return LightCount();
        }

        private BigInteger NoisyCount()
        {
            var partitionCount = BigInteger.Parse((NoisyWordsPartition.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            var lastPartitionCount = BigInteger.Parse(NoisyWordsPartition.Last().Values.Count.ToString());
            var count = partitionCount + lastPartitionCount;

            return count;
        }

        private BigInteger LightCount()
        {
            var partitionCount = BigInteger.Parse((LightWordsPartition.Count() - 1).ToString()) * BigInteger.Parse(PartitionSize.ToString());
            var lastPartitionCount = BigInteger.Parse(LightWordsPartition.Last().Values.Count.ToString());
            var count = partitionCount + lastPartitionCount;

            return count;
        }
    }
}
