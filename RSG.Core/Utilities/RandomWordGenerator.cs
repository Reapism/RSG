using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Result;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Models.Result;
using RSG.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
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
    public sealed class RandomWordGenerator : IGeneratorEvents
    {
        private readonly IDictionaryService dictionaryService;
        private readonly IThreadCount threadService;
        private readonly DictionaryConfiguration dictionaryConfiguration;
        private readonly ICharacterSetService characterSetService;
        private RsgDictionary dictionary; // Lazy instantiated in the generate results.
        private int maxValue;
        private int progressPercentage;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomWordGenerator"/>
        /// class thats able to generate random words.
        /// </summary>
        /// <param name="dictionaryService">A service for retrieving dictionaries.</param>
        /// <param name="characterSetService">A service for getting a character list.</param>
        /// <param name="threadService">A service for getting the number of threads.</param>
        /// <param name="dictionaryConfiguration">The dictionary configuration settings.</param>
        public RandomWordGenerator(
            IDictionaryService dictionaryService,
            ICharacterSetService characterSetService,
            IThreadCount threadService,
            DictionaryConfiguration dictionaryConfiguration)
        {
            // Set member dependencies.
            this.dictionaryService = dictionaryService;
            this.threadService = threadService;
            this.dictionaryConfiguration = dictionaryConfiguration;
            this.characterSetService = characterSetService;
            progressPercentage = 0;
        }

        /// <summary>
        /// Event for when the <see cref="GenerateAsync"/> function is completed.
        /// <para>Fired when it's cancelled, errored, or completed successfully.</para>
        /// </summary>
        public event Completed GenerateCompleted;

        /// <summary>
        /// Event for when the <see cref="ProgressChanged"/> function is in progress.
        /// <para>Fired when progress has changed.</para>
        /// </summary>
        public event ProgressChanged GenerateChanged;

        /// <summary>
        /// Runs the <see cref="GenerateAsync(BigInteger)"/> routine.
        /// <para>Subscribe to <see cref="GenerateChanged"/> and
        /// <see cref="GenerateCompleted"/> events.</para>
        /// </summary>
        /// <param name="numberOfIterations">The number of words to generate.</param>
        /// <returns>Returns an empty task.</returns>
        public async Task GenerateAsync(BigInteger numberOfIterations)
        {
            progressPercentage = 0;
            try
            {
                await LazyInitialization();

                var partitionInfo = PartitionInfo.Get(numberOfIterations, threadService.GetThreadsCount(numberOfIterations));

                FireGenerateChanged(new ProgressChangedEventArgs(progressPercentage += 5, $"Created {partitionInfo.NumberOfPartitions} partitions."));

                GenerateWords(partitionInfo);
            }
            catch (Exception e)
            {
                FireGenerateCompleted(new DictionaryEventArgs(e, false, this, null));
            }
        }

        private async Task LazyInitialization()
        {
            dictionary = await dictionaryService.SelectedAsync();
        }

        private void GenerateWords(PartitionInfo partitionInfo)
        {
            var partitionedWords = new ConcurrentQueue<IDictionary<int, IGeneratedWord>>();
            var useNoise = dictionaryConfiguration.UseNoise;
            var startTime = DateTime.Now;
            maxValue = dictionary.WordList.Count;

            FireGenerateChanged(new ProgressChangedEventArgs(10, String.Empty));

            // Multi-Threaded block
            var tasks = new Task[partitionInfo.NumberOfPartitions];
            var index = 0;
            for (; index < partitionInfo.NumberOfPartitions; index++)
            {
                tasks[index] = Task.Run(() =>
                {
                    var iterations = index == partitionInfo.NumberOfPartitions - 1 ?
                        partitionInfo.LastPartitionSize :
                        partitionInfo.FullPartitionSize;

                    var wordsPartition = useNoise ?
                        GeneratePartitionedWordsWithNoise(iterations) :
                        GeneratePartitionedWords(iterations);

                    partitionedWords.Enqueue(wordsPartition);
                });
            }

            try
            {
                Task.WaitAll(tasks);

                var words = new WordContainer(dictionaryConfiguration.UseNoise)
                {
                    PartitionedWords = partitionedWords
                };

                var result = new DictionaryResult()
                {
                    Dictionary = dictionary,
                    StartTime = startTime,
                    EndTime = DateTime.Now,
                    Iterations = partitionInfo.TotalIterations,
                    RandomizationType = RandomProvider.SelectedRandomizationType,
                    Words = words
                };
                FireGenerateChanged(new ProgressChangedEventArgs(100, null));
                FireGenerateCompleted(new DictionaryEventArgs(null, false, null, result));
            }
            catch (AggregateException ae)
            {
                FireGenerateCompleted(new DictionaryEventArgs(ae.Flatten(), false, ae, null));
            }
        }

        private IDictionary<int, IGeneratedWord> GeneratePartitionedWords(int iterations)
        {
            var words = new Dictionary<int, IGeneratedWord>(1);

            for (var i = 0; i < iterations; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };

                var progress = 100.0 * i / iterations;
                words.Add(i, generatedWord);
                if (progress % 10 == 0)
                {
                    FireGenerateChanged(new ProgressChangedEventArgs((int)progress, null));
                }
            }

            return words;
        }

        private IDictionary<int, IGeneratedWord> GeneratePartitionedWordsWithNoise(int iterations)
        {
            var words = new Dictionary<int, IGeneratedWord>(1);

            for (var i = 0; i < iterations; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };

                generatedWord.NoisyCharacterPositions = GenerateNoisyCharacterPositions(generatedWord.Word.Length);

                words.Add(i, generatedWord);
            }

            return words;
        }

        private string GenerateRandomWord()
        {
            var rndValue = RandomProvider.Random.Value.Next(0, maxValue);

            return dictionary.WordList[rndValue];
        }

        private SortedDictionary<int, IPositionCharacterPair> GenerateNoisyCharacterPositions(int wordLength)
        {
            var percentage = dictionaryConfiguration.NoiseFrequency;
            var chance = RandomProvider.Random.Value.Next(100) + 1;
            var noisePositions = new SortedDictionary<int, IPositionCharacterPair>();
            var characterSet = characterSetService.CharacterList;

            // Chance is in range of the percentage.
            if (chance <= percentage)
            {
                var numberOfRandoms = RandomProvider.Random.Value.Next(wordLength) + 1;

                // attempt a few insertions.
                for (var i = 0; i < numberOfRandoms;)
                {
                    var position = RandomProvider.Random.Value.Next(wordLength);
                    var character = characterSet[RandomProvider.Random.Value.Next(characterSet.Length)];
                    var pair = new PositionCharacterPair()
                    {
                        Character = character,
                        Position = position
                    };
                    noisePositions.Add(i, pair);
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
