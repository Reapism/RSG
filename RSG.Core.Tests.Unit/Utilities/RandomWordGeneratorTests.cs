using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;

namespace RSG.Core.Tests.Unit.Utilities
{
    [TestFixture]
    public class RandomWordGeneratorTests
    {
        [Test]
        public void GenerateRandomWords_ShouldGenerateRandomWords()
        {
            var dictionaryConfiguration = new DictionaryConfiguration()
            {
                AliterationCharacter = 'a',
                AliterationFrequency = 10.0D,
                AliterationRange = new System.Range(
                    new System.Index(1),
                    new System.Index(2)
                ),
                UseAliteration = true,
                CapitalizeEachWord = true,
                Dictionaries = new List<RsgDictionary>()
                {
                    new RsgDictionary()
                    {
                        IsActive = true,
                        Count = BigInteger.One,
                        Description = "Test",
                        IsSourceLocal = true,
                        Name = "Test",
                        Source = "source somewhere kill me",
                        WordList = new Dictionary<int, string>(
                            new List<KeyValuePair<int, string>>
                            {
                                new KeyValuePair<int, string>(1, "Word"),
                                new KeyValuePair<int, string>(2, "Kill Me Word 2")
                            }),
                    },
                    new RsgDictionary()
                    {
                        IsActive = true,
                        Count = BigInteger.One,
                        Description = "Test2",
                        IsSourceLocal = true,
                        Name = "Test2",
                        Source = "source somewhere kill me",
                        WordList = new Dictionary<int, string>(
                            new List<KeyValuePair<int, string>>
                            {
                                new KeyValuePair<int, string>(1, "Word2"),
                                new KeyValuePair<int, string>(2, "Kill Me Word 23")
                            }),
                    }
                },
                Source = "Path",
                MaximumThreadCount = 10,
                NoiseFrequency = 10.0D,
                NoisePerWordRange = new System.Range(
                    new System.Index(1),
                    new System.Index(2)
                ),
                Priority = System.Threading.ThreadPriority.Normal,
                UseNoise = true,
                UseSpace = true
            };

            var stringConfig = new StringConfiguration()
            {
                CharacterSet = new CharacterSet(),
                StringLength = BigInteger.One,
            };

            stringConfig.CharacterSet.Characters = new Dictionary<string, SingleCharacterSet>(
                new List<KeyValuePair<string, SingleCharacterSet>>()
                {
                    new KeyValuePair<string, SingleCharacterSet>("Key1", new SingleCharacterSet("ABCDEF", true)),
                    new KeyValuePair<string, SingleCharacterSet>("Key2", new SingleCharacterSet("GHIJKL", false))
                });


            var randomWordGenerator = new RandomWordGenerator(
                new DictionaryService(
                    new WordListService(
                        dictionaryConfiguration.Dictionaries.FirstOrDefault()),
                    dictionaryConfiguration
                    ),
                new ThreadUtility(),
                dictionaryConfiguration,
                new CharacterSetService(
                    stringConfig,
                    stringConfig.CharacterSet,
                    new Scrambler())
                );

            var container = new TestContainer();
            var randomWordGen2 = container.Provider.GetService<RandomWordGenerator>();
            int i = 0;
        }
    }
}
