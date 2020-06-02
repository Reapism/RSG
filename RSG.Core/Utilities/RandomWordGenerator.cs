using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    public class RandomWordGenerator
    {
        private readonly DictionaryService dictionaryService;
        private readonly IThreadService threadService;
        private readonly IDictionaryConfiguration dictionaryConfiguration;
        private readonly CharacterSetService characterSetService;
        private readonly char[] characterSet;
        private RsgDictionary dictionary; // Lazy instantiated in the generate results.
        private int minWordIndex;
        private int maxWordIndex;

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

            characterSet = characterSetService.GetNewCharacterList();

            minWordIndex = 0;
        }

        public async Task<IDictionaryResult> GenerateRandomWordsResult(int numberOfIterations)
        {
            return await GenerateRandomWordsResult(numberOfIterations.ToBigInteger());
        }

        public async Task<IDictionaryResult> GenerateRandomWordsResult(BigInteger numberOfIterations)
        {
            await LazyInitialization();
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

            return result;
        }

        private async Task LazyInitialization()
        {
            dictionary = await dictionaryService.GetSelectedDictionary();

        }

        private async Task<Words> GenerateWords(BigInteger numberOfIterations)
        {
            var partitionedWords = new ConcurrentQueue<ConcurrentDictionary<int, IGeneratedWord>>();
            var partitionInfo = GetPartitionInfo(numberOfIterations);
            var useNoise = dictionaryConfiguration.UseNoise;
            var cancellationToken = new CancellationToken(false);
            var fullPartitions = partitionInfo.NumberOfPartitions;
            cancellationToken.Register(() => { });

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = partitionInfo.NumberOfPartitions,
                CancellationToken = cancellationToken,
                TaskScheduler = TaskScheduler.Current
            };

            if (useNoise)
            {
                var result = Parallel.For(0, partitionInfo.NumberOfPartitions, index =>
                {
                    partitionInfo.CurrentIndex = index;
                    partitionedWords.Enqueue(GeneratePartitionedWordsWithNoise(partitionInfo));

                });
            }
            else
            {
                var result = Parallel.For(0, partitionInfo.NumberOfPartitions, index =>
                {
                    partitionInfo.CurrentIndex = index;

                    var wordsPartition = GeneratePartitionedWords(partitionInfo);
                    partitionedWords.Enqueue(wordsPartition);

                });
            }

            // create words instance
            // determine partition size
            // generate each partition in parallel
            // apply noisy partitions if needed (already applied if using GeneratePartitionedWords)
            // use Parallel.Foreach for each 
            // return words instance

            var words = new Words(dictionaryConfiguration.UseNoise);
            words.PartitionedWords = partitionedWords;
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
                lastPartitionSize += partitionSize.ToBigInteger();

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

        private ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWords(PartitionInfo partitionInfo)
        {
            var words = new ConcurrentDictionary<int, IGeneratedWord>();
            var partitionSize = partitionInfo.GetPartitionSize();
            Console.WriteLine(partitionSize);
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
    }
}
