using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Library.Models
{
    internal struct Words
    {
        /// <summary>
        /// Maps a partitioned collection of a word indexes(key) to its <see cref="NoisyWord"/> (value).
        /// </summary>
        public IEnumerable<ConcurrentDictionary<BigInteger, NoisyWord>> NoisyWordsPartition { get; set; }

        /// <summary>
        /// Maps a partitioned collection of a word indexes(key) to its <see cref="LightWord"/> (value).
        /// </summary>
        public IEnumerable<ConcurrentDictionary<BigInteger, LightWord>> LightWordsPartition { get; set; }

        /// <summary>
        /// Gets or sets the size of each index <see cref="WordsPartition"/>.
        /// </summary>
        public BigInteger PartitionSize { get; set; }

        /// <summary>
        /// Initializes a <see cref="Words"/> with a particular 
        /// <see cref="PartitionSize"/>.
        /// </summary>
        /// <param name="partitionSize"></param>
        public Words(BigInteger partitionSize)
        {
            NoisyWordsPartition = new ConcurrentQueue<ConcurrentDictionary<BigInteger, NoisyWord>>();
            LightWordsPartition = new ConcurrentQueue<ConcurrentDictionary<BigInteger, LightWord>>();
            PartitionSize = partitionSize;
        }
    }
}
