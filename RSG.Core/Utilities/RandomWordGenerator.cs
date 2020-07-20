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
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly char[] characterSet;
        private RsgDictionary dictionary; // Lazy instantiated in the generate results.
        private int minWordIndex;
        private int maxWordIndex;

        /// <summary>
        /// Event for when the <see cref="GenerateRandomWordsResult"/> function is completed.
        /// <para>Fired when it's cancelled, errored, or completed successfully.</para>
        /// </summary>
        public event GenerateRandomWordsResultCompletedEventHandler GenerateRandomWordsResultCompleted;

        public event GenerateRandomWordsResultProgressChangedEventHandler GenerateRandomWordsResultProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomWordGenerator"/>
        /// class thats able to generate random words.
        /// </summary>
        /// <param name="dictionaryService"></param>
        /// <param name="threadService"></param>
        /// <param name="dictionaryConfiguration"></param>
        /// <param name="characterSetService"></param>
        public RandomWordGenerator(
            DictionaryService dictionaryService,
            IThreadService threadService,
            IDictionaryConfiguration dictionaryConfiguration,
            CharacterSetService characterSetService)
        {
            // Set member dependencies.
            this.dictionaryService = dictionaryService;
            this.threadService = threadService;
            this.dictionaryConfiguration = dictionaryConfiguration;
            this.characterSetService = characterSetService;

            characterSet = characterSetService.CharacterList;
            minWordIndex = 0;

            // Subscribe to the handlers
            GenerateRandomWordsResultProgressChanged += HandleGenerateRandomWordsResultProgressChanged;
            GenerateRandomWordsResultCompleted += HandleGenerateRandomWordsResultCompleted;
        }

        public async Task GenerateRandomWordsResult(BigInteger numberOfIterations)
        {
            try
            {
                await LazyInitialization();

                FireGenerateRandomWordsResultProgressChanged(new ProgressChangedEventArgs(5, this));

                maxWordIndex = dictionary.WordList.Count();
                var startTime = DateTime.Now;
                var words = await GenerateWords(numberOfIterations);

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

                FireGenerateRandomWordsResultProgressChanged(new ProgressChangedEventArgs(100, this));
                FireGenerateRandomWordsResultCompleted(new GenerateRandomWordsResultEventArgs(null, false, null, result));
            }
            catch (Exception e)
            {
                FireGenerateRandomWordsResultCompleted(new GenerateRandomWordsResultEventArgs(e, false, this, new DictionaryResult().Empty()));
            }
        }

        private async Task LazyInitialization()
        {
            dictionary = await dictionaryService.GetSelectedDictionaryAsync();
        }

        private async Task<WordContainer> GenerateWords(BigInteger numberOfIterations)
        {
            var partitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
            var partitionInfo = GetPartitionInfo(numberOfIterations);
            var useNoise = dictionaryConfiguration.UseNoise;
            var fullPartitions = partitionInfo.NumberOfPartitions;

            FireGenerateRandomWordsResultProgressChanged(new ProgressChangedEventArgs(10, this));

            var tasks = new Task[partitionInfo.NumberOfPartitions];
            int index = 0;
            for (; index < partitionInfo.NumberOfPartitions; index++)
            {
                tasks[index] = Task.Factory.StartNew(() =>
                {
                    var info = GetPartitionInfo(numberOfIterations, index);

                    var wordsPartition = useNoise ?
                        GeneratePartitionedWordsWithNoise(info) :
                        GeneratePartitionedWords(info);

                    partitionedWords.Enqueue(wordsPartition);
                });
            }

            Task.WaitAll(tasks);

            FireGenerateRandomWordsResultProgressChanged(new ProgressChangedEventArgs(90, this));

            var words = new WordContainer(dictionaryConfiguration.UseNoise)
            {
                PartitionedWords = partitionedWords
            };
            return words;
        }

        private PartitionInfo GetPartitionInfo(in BigInteger numberOfIterations)
        {
            var threadCount = threadService.GetThreadsCount();

            var partitionSize = int.Parse(BigInteger.DivRem(
                numberOfIterations,
                threadCount.ToBigInteger(),
                out var lastPartitionSize).ToString());

            // If there is nothing left over from division,
            // its an even split, last partition should be
            // the same size as all.
            if (lastPartitionSize == BigInteger.Zero)
            {
                lastPartitionSize = partitionSize.ToBigInteger();
            }
            else
            {
                lastPartitionSize += partitionSize.ToBigInteger();
            }

            var partitionInfo = new PartitionInfo()
            {
                FullPartitionSize = partitionSize,
                LastPartitionSize = int.Parse(lastPartitionSize.ToString()),
                NumberOfPartitions = threadCount,
                TotalIterations = numberOfIterations,
                CurrentIndex = 0
            };

            return partitionInfo;
        }

        private PartitionInfo GetPartitionInfo(in BigInteger numberOfIterations, int currentIndex)
        {
            var partitionInfo = GetPartitionInfo(numberOfIterations);
            partitionInfo.CurrentIndex = currentIndex;

            return partitionInfo;
        }

        private ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWords(PartitionInfo partitionInfo)
        {
            var words = new ConcurrentDictionary<int, IGeneratedWord>();
            var partitionSize = partitionInfo.GetPartitionSize();

            for (var i = 0; i < partitionSize; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };

                words.TryAdd(i, generatedWord);
            }

            return words;
        }

        private ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWordsWithNoise(PartitionInfo partitionInfo)
        {
            var words = new ConcurrentDictionary<int, IGeneratedWord>();
            var partitionSize = partitionInfo.GetPartitionSize();

            for (var i = 0; i < partitionSize; i++)
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

        private void FireGenerateRandomWordsResultCompleted(GenerateRandomWordsResultEventArgs args)
        {
            if (GenerateRandomWordsResultCompleted == null)
                return;

            GenerateRandomWordsResultCompleted(this, args);
        }

        private void HandleGenerateRandomWordsResultCompleted(object sender, GenerateRandomWordsResultEventArgs args)
        {
            if (args.Cancelled)
            {
                DebugUtility.Write((ToString(), $"This process has been cancelled"));
                return;
            }

            if (args.Error != null)
            {
                DebugUtility.Write((ToString(), $"An exception has been thrown during the word generator: {args.Error.Message}"));
                return;
            }

            if (args.Result != null)
            {
                DebugUtility.Write((ToString(),
                    $"{sender} EndTime: {args.Result.EndTime} Count: {args.Result.Words.Count}"));
                return;
            }
        }

        private void FireGenerateRandomWordsResultProgressChanged(ProgressChangedEventArgs args)
        {
            if (GenerateRandomWordsResultProgressChanged == null)
            {
                return;
            }

            GenerateRandomWordsResultProgressChanged(this, args);
        }

        private void HandleGenerateRandomWordsResultProgressChanged(object sender, ProgressChangedEventArgs args)
        {

            DebugUtility.Write((ToString(), args.ProgressPercentage.ToString()));
        }
    }
}
