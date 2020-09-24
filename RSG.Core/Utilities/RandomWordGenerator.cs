using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Contains methods for generating random words synchronously or
    /// asynchronously.
    /// </summary>
    /// <remarks>
    /// This class cannot be inherited.
    /// </remarks>
    public sealed class RandomWordGenerator
    {
        private readonly DictionaryService dictionaryService;
        private readonly IThreadService threadService;
        private readonly IDictionaryConfiguration dictionaryConfiguration;
        private readonly CharacterSetService characterSetService;
        private RsgDictionary dictionary; // Lazy instantiated in the generate results.
        private int minWordIndex;
        private int maxWordIndex;

        /// <summary>
        /// Event for when the <see cref="Generate"/> function is completed.
        /// <para>Fired when it's cancelled, errored, or completed successfully.</para>
        /// </summary>
        public event Completed GenerateCompleted;

        /// <summary>
        /// Event for when the <see cref="ProgressChanged"/> function is in progress.
        /// <para>Fired when progress has changed.</para>
        /// </summary>
        public event ProgressChanged GenerateChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomWordGenerator"/>
        /// class thats able to generate random words.
        /// </summary>
        /// <param name="dictionaryService"></param>
        /// <param name="characterSetService"></param>
        /// <param name="threadService"></param>
        /// <param name="dictionaryConfiguration"></param>
        public RandomWordGenerator(
            DictionaryService dictionaryService,
            CharacterSetService characterSetService,
            IThreadService threadService,
            IDictionaryConfiguration dictionaryConfiguration)
        {
            // Set member dependencies.
            this.dictionaryService = dictionaryService;
            this.threadService = threadService;
            this.dictionaryConfiguration = dictionaryConfiguration;
            this.characterSetService = characterSetService;

            minWordIndex = 0;
        }

        /// <summary>
        /// Runs the <see cref="Generate(BigInteger)"/> routine.
        /// <para>Subscribe to <see cref=""/></para>
        /// </summary>
        /// <param name="numberOfIterations"></param>
        /// <returns></returns>
        public async Task Generate(BigInteger numberOfIterations)
        {
            try
            {
                await LazyInitialization();

                var partitionInfo = PartitionInfo.Get(numberOfIterations, threadService.GetThreadsCount(numberOfIterations));
                maxWordIndex = dictionary.WordList.Count();

                var startTime = DateTime.Now;
                var words = await GenerateWords(partitionInfo);

                var endDate = DateTime.Now;

                var result = new DictionaryResult()
                {
                    Dictionary = this.dictionary,
                    StartTime = startTime,
                    EndTime = endDate,
                    Iterations = numberOfIterations,
                    RandomizationType = RandomProvider.SelectedRandomizationType,
                    Words = words
                };

                FireGenerateChanged(new ProgressChangedEventArgs(100, this));
                FireGenerateCompleted(new DictionaryEventArgs(null, false, null, result));
            }
            catch (Exception e)
            {
                FireGenerateCompleted(new DictionaryEventArgs(e, false, this, ResultBase.Empty() as IDictionaryResult));
            }
        }

        private async Task LazyInitialization()
        {
            dictionary = await dictionaryService.GetSelectedDictionaryAsync();
        }

        private async Task<WordContainer> GenerateWords(PartitionInfo partitionInfo)
        {
            var partitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
            var useNoise = dictionaryConfiguration.UseNoise;

            FireGenerateChanged(new ProgressChangedEventArgs(10, this));

            var tasks = new Task[partitionInfo.NumberOfPartitions];
            var index = 0;
            for (; index < partitionInfo.NumberOfPartitions; index++)
            {
                var iterations = index == partitionInfo.NumberOfPartitions - 1 ?
                    partitionInfo.LastPartitionSize : 
                    partitionInfo.FullPartitionSize;
                tasks[index] = Task.Run(() =>
                {
                    var wordsPartition = useNoise ?
                        GeneratePartitionedWordsWithNoise(iterations) :
                        GeneratePartitionedWords(iterations);

                    partitionedWords.Enqueue(wordsPartition);
                });
            }

            await Task.WhenAll(tasks);

            FireGenerateChanged(new ProgressChangedEventArgs(98, this));

            var words = new WordContainer(dictionaryConfiguration.UseNoise)
            {
                PartitionedWords = partitionedWords
            };

            //FireGenerateCompleted(new DictionaryEventArgs(null, false, null, new DictionaryResult() { Words = words }));
            return words;
        }

        private ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWords(int iterations)
        {
            var words = new ConcurrentDictionary<int, IGeneratedWord>(1, iterations);

            for (var i = 0; i < iterations; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };

                words.TryAdd(i, generatedWord);
            }

            return words;
        }

        private ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWordsWithNoise(int iterations)
        {
            var words = new ConcurrentDictionary<int, IGeneratedWord>(1, iterations);

            for (var i = 0; i < iterations; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };

                generatedWord.NoisyCharacterPositions = GenerateNoisyCharacterPositions(generatedWord.Word.Length);

                words.TryAdd(i, generatedWord);
            }

            return words;
        }

        private string GenerateRandomWord()
        {
            var rndValue = RandomProvider.Random.Next(minWordIndex, maxWordIndex);

            return dictionary.WordList[rndValue];
        }

        private SortedDictionary<int, IPositionCharacterPair> GenerateNoisyCharacterPositions(int wordLength)
        {
            var percentage = dictionaryConfiguration.NoiseFrequency;
            var chance = RandomProvider.Random.Next(100) + 1;
            var noisePositions = new SortedDictionary<int, IPositionCharacterPair>();
            var characterSet = characterSetService.CharacterList;

            // Chance is in range of the percentage.
            if (chance <= percentage)
            {
                var numberOfRandoms = RandomProvider.Random.Next(wordLength) + 1;

                // attempt a few insertions.
                for (var i = 0; i < numberOfRandoms;)
                {
                    var position = RandomProvider.Random.Next(wordLength);
                    var character = characterSet[RandomProvider.Random.Next(characterSet.Length)];
                    var pair = new PositionCharacterPair()
                    {
                        Character = character,
                        Position = position
                    };
                    noisePositions.TryAdd(i, pair);
                }
            }

            return noisePositions;
        }

        private void FireGenerateChanged(ProgressChangedEventArgs args)
        {
            if (!(GenerateChanged is null))
            {
                GenerateChanged(this, args);
            }
        }

        private void FireGenerateCompleted(DictionaryEventArgs args)
        {
            if (!(GenerateCompleted is null))
            {
                GenerateCompleted(this, args);
            }
        }
    }
}
