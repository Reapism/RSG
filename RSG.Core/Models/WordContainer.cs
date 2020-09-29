using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a collection of generated words.
    /// </summary>
    public class WordContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordContainer"/> class
        /// and if it's noisy.
        /// </summary>
        /// <param name="isNoisy">Whether this instance is Noisy.</param>
        public WordContainer(bool isNoisy)
        {
            IsNoisy = isNoisy;

            // Queue is number of partitions, Dictionary is number of words K: index, V: IGeneratedWord
            PartitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
        }

        /// <summary>
        /// Gets a value that maps a partitioned collection of <see cref="IGeneratedWord"/>(s).
        /// </summary>
        public ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>> PartitionedWords { get; internal set; }

        /// <summary>
        /// Gets the number of total words stored in this instance.
        /// </summary>
        public BigInteger Count => GetCount();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WordContainer"/>
        /// instance contains noise.
        /// </summary>
        public bool IsNoisy { get; }

        /// <summary>
        /// Clears all of the words stored in this instance.
        /// </summary>
        public void Clear()
        {
            foreach (var dictionary in PartitionedWords)
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
            {
                return emptyQueue;
            }

            return PartitionedWords.ElementAt(partitionIndex);
        }

        public IGeneratedWord GetWordFromIteration(int iteration)
        {
            var partitionIndex = GetPartitionIndexFromIteration(iteration);
            var partition = GetWordsAtIndex(partitionIndex);

            throw new NotImplementedException("Awaiting other functions");

        }

        public bool Contains(string word)
        {
            foreach (var kvp in PartitionedWords)
            {
                if (kvp.Any(e => e.Value.Word.Equals(word, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetNumberOfNoiseAppendedFor(string word)
        {
            throw new NotImplementedException("Awaiting other functions");
        }

        public int GetNumberOfNoiseAppendedFor(IGeneratedWord word)
        {
            throw new NotImplementedException("Awaiting other functions");
        }

        public WordStats GetWordStatsFor(string word)
        {
            return new WordStats()
            {
                NumberOfOccurrences = GetNumberOfOccurencesFor(word)
            };
        }

        public int GetNumberOfOccurencesFor(string word)
        {
            var counter = 0;
            foreach (var kvp in PartitionedWords)
            {
                counter += kvp.Count(e => e.Value.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            }

            return counter;
        }

        public int GetNumberOfOccurencesFor(IGeneratedWord word)
        {
            var counter = 0;
            foreach (var kvp in PartitionedWords)
            {
                counter += kvp.Count(e => e.Value.Word.Equals(word.Word, StringComparison.OrdinalIgnoreCase));
            }

            return counter;
        }

        private int GetPartitionIndexFromIteration(int iteration)
        {
            var count = Count;
            if (iteration >= count)
            {
                throw new IndexOutOfRangeException($"Index out of range! Select between 0 and {count}");
            }

            // Lets say total count is 8007 words from 8 partitions.
            // Part[0-6] is 1k words and Part[7] is 1007 words.
            // Iteration is 3003, return partition index 3.
            var minRange = 0;
            var maxRange = PartitionedWords.First().Count;
            int partitionIndex;
            for (partitionIndex = 1; partitionIndex < PartitionedWords.Count; partitionIndex++)
            {
                minRange += PartitionedWords.ElementAt(partitionIndex).Count;
                if (minRange < iteration && iteration < maxRange)
                {
                    return partitionIndex;
                }

                maxRange += minRange;
            }

            return -1;
        }

        /// <summary>
        /// Gets the new count of all the words stored.
        /// </summary>
        private BigInteger GetCount()
        {
            var count = BigInteger.Zero;

            foreach (var c in PartitionedWords)
            {
                count += c.Count.ToBigInteger();
            }

            return count;
        }
    }

    public class WordStats
    {
        /// <summary>
        /// Gets or sets the generated word.
        /// </summary>
        public IGeneratedWord Word { get; set; }

        /// <summary>
        /// Gets or sets the number of occurences the word was found in the generation.
        /// </summary>
        public int NumberOfOccurrences { get; set; }

        /// <summary>
        /// Gets the number of noise appended to this generated word.
        /// </summary>
        public int NumberOfNoiseAppended { get => Word.NoisyCharacterPositions.Count(); }

        /// <summary>
        /// Gets or sets the probability of this word generating.
        /// </summary>
        public double ProbabilityOfAppearing { get; set; } // use double.ToString("P", CultureInfo.Inv..) to format this

        /// <summary>
        /// Gets or sets the definition of this word.
        /// </summary>
        public string? WordDefinition { get; set; }
    }
}
