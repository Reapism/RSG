using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Models.Results;
using RSG.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
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
    public sealed class RandomWordGenerator : IRandomWordGenerator, IGeneratorEvents
    {
        private readonly IDictionaryLoader dictionaryService;
        private readonly IThreadBalancer threadService;
        private readonly DictionaryConfiguration dictionaryConfiguration;
        private readonly char[] characterList;
        private RsgDictionary dictionary; // Lazy instantiated in the generate results.
        private int maxValue;
        private int progressPercentage;
        private IRandom random;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomWordGenerator"/>
        /// class thats able to generate random words.
        /// </summary>
        /// <param name="dictionaryService">A service for retrieving dictionaries.</param>
        /// <param name="characterSetProvider">A service for providing a character list.</param>
        /// <param name="threadService">A service for getting the number of threads.</param>
        /// <param name="dictionaryConfiguration">The dictionary configuration settings.</param>
        public RandomWordGenerator(
            IDictionaryLoader dictionaryService,
            ICharacterSetProvider characterSetProvider,
            IThreadBalancer threadService,
            DictionaryConfiguration dictionaryConfiguration)
        {
            // Set member dependencies.
            this.dictionaryService = dictionaryService;
            this.threadService = threadService;
            this.dictionaryConfiguration = dictionaryConfiguration;

            characterList = characterSetProvider.ToCharArray();
            random = RandomProvider.Random;

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

        public void GenerateWords(IDictionaryRequest dictionaryRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task GenerateWordsAsync(IDictionaryRequest dictionaryRequest, CancellationToken cancellationToken)
        {
            await GenerateAsyncInternal(request, cancellationToken);
        }

        /// <summary>
        /// Runs the <see cref="GenerateAsync(BigInteger)"/> routine.
        /// <para>Subscribe to <see cref="GenerateChanged"/> and
        /// <see cref="GenerateCompleted"/> events.</para>
        /// </summary>
        /// <param name="request">The request used to generate.</param>
        /// <returns>Returns an empty task.</returns>
        public async Task GenerateAsync(IDictionaryRequest request)
        {
            await GenerateAsyncInternal(request);
        }

        private async Task GenerateAsyncInternal(IDictionaryRequest request, CancellationToken cancellationToken)
        {
            progressPercentage = 0;
            try
            {
                await LazyInitialization();

                var partitionInfo = PartitionInfo.Get(request.Iterations, threadService.GetThreadCountByIterations(request.Iterations));

                FireGenerateChanged(new ProgressChangedEventArgs(progressPercentage += 5, $"Created {partitionInfo.NumberOfPartitions} partitions."));

                GenerateWords(request, partitionInfo);
            }
            catch (Exception e)
            {
                FireGenerateCompleted(new DictionaryEventArgs(e, false, this, null));
            }
        }

        private async Task LazyInitialization()
        {
            dictionary = await dictionaryService.GetSelectedDictionaryAsync();
        }

        private void GenerateWords(IDictionaryRequest request, PartitionInfo partitionInfo)
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

                var endTime = DateTime.Now;
                var duration = endTime - startTime;

                var result = new DictionaryResult(request, duration, words)
                {
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
                var generatedWord = new GeneratedWord(GenerateRandomWord(), null);

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
                var word = GenerateRandomWord();
                var additionalCharacterPositions = GenerateNoisyCharacterForWord(word.Length);

                var generatedWord = new GeneratedWord(word, additionalCharacterPositions);

                words.Add(i, generatedWord);
            }

            return words;
        }

        private string GenerateRandomWord()
        {
            var rndValue = RandomProvider.Random.Next(0, maxValue);

            return dictionary.WordList[rndValue];
        }

        // TODO look into the parameter for this method,
        // not sure it should just be wordLength
        // This method will need to be multithreadable
        // maybe we pass the IGeneratedWord or the string word
        // and then return a IGeneratedWord properly.
        // This function should invoke per word.
        // Also IEnumerable might need to be IDictionary
        // the int in the key would represent the order of the generated char
        // since its possible for two or more characters to generate noise in the 
        // same position. But this might not be a problem since list or Queue preserves order anyways.
        // have to choose whether to use IEnumerable and save memory but possibly convey a diff meaning
        // or use dictionary and use more memory but convey and order.
        private IEnumerable<IPositionalCharacter> GenerateNoisyCharacterForWord(int wordLength)
        {
            var noisePositions = new Queue<IPositionalCharacter>();
            var noiseFrequencyPercentage = dictionaryConfiguration.NoiseFrequency;
            var chanceToGenerateRandomNoise = random.Next(100) + 1;
            var noisePerWordRange = dictionaryConfiguration.NoisePerWordRange;

            // Chance is in range of the percentage.
            if (chanceToGenerateRandomNoise <= noiseFrequencyPercentage)
            {
                // TODO attempt a few insertions, must be in the range specified in the config.
                // wordLength + 1 for maxValue is not necessary, since range is a understood type min is inclusive, max is exclusive
                // also check if random.Next vs RandomProvider.Random.Next is any different if we switch random types
                // mid operation.
                // Q: would the private member now reflect the different random type?
                // create test, make classes public for time being if need be.
                // I think we want it to change midway to show its static, or if each generate
                // is atomic and so the generate result will have an accurate randomization type
                // per generate.

                // optimization, maybe does not need to be computed/checked for every generated word.
                var numberOfRandoms = noisePerWordRange.Start.Value;
                if (noisePerWordRange.Start.Value != noisePerWordRange.End.Value)
                {
                    numberOfRandoms = random.Next(noisePerWordRange.Start.Value, noisePerWordRange.End.Value);
                }

                for (var i = 0; i < numberOfRandoms;)
                {
                    var positionInWord = RandomProvider.Random.Next(wordLength);
                    var character = characterList[RandomProvider.Random.Next(characterList.Length)];
                    var pair = new PositionalCharacter()
                    {
                        Character = character,
                        Position = positionInWord
                    };
                    noisePositions.Enqueue(pair);
                }
            }

            return noisePositions;
        }

        private void FireGenerateChanged(ProgressChangedEventArgs args)
        {
            if (!(GenerateChanged is null))
            {
                GenerateChanged(args);
            }
        }

        private void FireGenerateCompleted(DictionaryEventArgs args)
        {
            if (!(GenerateCompleted is null))
            {
                GenerateCompleted(args);
            }
        }
    }
}
