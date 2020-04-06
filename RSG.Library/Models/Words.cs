using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Library.Models
{
    internal class Words
    {
        /// <summary>
        /// Maps a partitioned collection of a word indexes(key) to its <see cref="NoisyWord"/> (value).
        /// </summary>
        public IEnumerable<ConcurrentDictionary<int, NoisyWord>> NoisyWordsPartition { get; set; }

        /// <summary>
        /// Maps a partitioned collection of a word indexes(key) to its <see cref="LightWord"/> (value).
        /// </summary>
        public IEnumerable<ConcurrentDictionary<int, LightWord>> LightWordsPartition { get; set; }

        /// <summary>
        /// Gets or sets the size of each index <see cref="WordsPartition"/>.
        /// </summary>
        public int PartitionSize { get; set; }

        /// <summary>
        /// Determines whether this <see cref="Words"/> instance is using the <see cref="NoisyWordsPartition"/>.
        /// </summary>
        public bool IsNoisy { get; set; }

        /// <summary>
        /// Initializes a <see cref="Words"/> with a particular 
        /// <see cref="PartitionSize"/>.
        /// </summary>
        /// <param name="partitionSize"></param>
        public Words(int partitionSize)
        {
            NoisyWordsPartition = new ConcurrentQueue<ConcurrentDictionary<int, NoisyWord>>();
            LightWordsPartition = new ConcurrentQueue<ConcurrentDictionary<int, LightWord>>();
            PartitionSize = partitionSize;
            IsNoisy = false;
        }

        /// <summary>
        /// The total number of words stored in partition.
        /// </summary>
        public BigInteger Count()
        {
            if (NoisyWordsPartition.Any())
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
