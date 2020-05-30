using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using RSG.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Utilities
{
    public class RandomWordGenerator
    {
        private readonly DictionaryService dictionaryService;
        private readonly DictionaryThreadService dictionaryThreadService;
        private readonly IDictionaryConfiguration dictionaryConfiguration;
        private readonly CharacterSetService characterSetService;
        private readonly char[] characterSet;
        private readonly RsgDictionary dictionary;
        private int minWordIndex;
        private int maxWordIndex;
        private readonly BigInteger numberOfWords;

        public RandomWordGenerator(
            DictionaryService dictionaryService,
            DictionaryThreadService dictionaryThreadService,
            IDictionaryConfiguration dictionaryConfiguration,
            CharacterSetService characterSetService)
        {
            // Set member dependencies.
            this.dictionaryService = dictionaryService;
            this.dictionaryThreadService = dictionaryThreadService;
            this.dictionaryConfiguration = dictionaryConfiguration;
            this.characterSetService = characterSetService;

            numberOfWords = BigInteger.Zero;
            characterSet = characterSetService.GetNewCharacterList();

            dictionary = dictionaryService.GetSelectedDictionary();
            minWordIndex = 0;
            maxWordIndex = dictionary.WordList.Count();
        }

        public IDictionaryResult GenerateRandomWordsResult(int numberOfIterations)
        {
            return GenerateRandomWordsResult(numberOfIterations.ToBigInteger());
        }

        public IDictionaryResult GenerateRandomWordsResult(in BigInteger numberOfIterations)
        {
            DateTime startTime = DateTime.Now;

            var words = new Words(this, dictionaryConfiguration.UseNoise, dictionaryConfiguration.PartitionSize);

            DateTime endTime = DateTime.Now;
            var result = new DictionaryResult()
            {
                Dictionary = dictionary,
                Iterations = words.Count,
                RandomizationType = RandomProvider.SelectedRandomizationType,
                Words = words,
                StartTime = startTime,
                EndTime = endTime
            };

            return result;
        }

        public ConcurrentDictionary<int, IGeneratedWord> GeneratePartitionedWords(int partitionSize)
        {
            var partitionedWords = new ConcurrentDictionary<int, IGeneratedWord>();
            var useNoise = dictionaryConfiguration.UseNoise;

            for (var i = 0; i < numberOfWords; i++)
            {
                var generatedWord = new GeneratedWord()
                {
                    Word = GenerateRandomWord(),
                };
                if (useNoise)
                    generatedWord.NoisyCharacterPositions = GenerateNoisyCharacterPositions(generatedWord.Word.Length);

            }

            return partitionedWords;
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
