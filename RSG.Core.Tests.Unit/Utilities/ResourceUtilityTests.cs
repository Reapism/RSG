﻿using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Constants;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Tests.Unit.Utilities
{
    [TestFixture]
    class ResourceUtilityTests
    {
        [Test]
        public void Test()
        {
            var configurationFile = ResourceUtility.GetRsgConfigurationFile();
        }

        [Test]
        public async Task GenerateConfiguration()
        {
            var rsgConfiguration = new RsgConfiguration()
            {
                CheckForUpdatesOnLoad = true,
                CopySelectionsToClipboard = true,
                CurrentVersion = new Version(1, 0, 0, 0),
                DictionaryConfigurationSource = Path.Combine(Environment.CurrentDirectory, "Dictionary.json"),
                FirstTimeUsingCurrentVersion = true,
                NumberOfLaunchesThisVersion = 0,
                NumberOfLaunchesTotal = 0,
                RandomizationType = Enums.RandomizationType.Pseudorandom,
                StringConfigurationSource = Path.Combine(Environment.CurrentDirectory, "String.json"),
                UseStickyWindows = false
            };
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Rsg.json");
                await SerializationUtility.SerializeJsonAsync(rsgConfiguration, path);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        public async Task GenerateStringConfiguration()
        {
            var dictionary = new Dictionary<string, SingleCharacterSet>();

            var characterSet = new CharacterSet()
            {
                Characters = dictionary
            };

            characterSet.Characters.Add(
                CharacterSetConstants.Lowercase,
                new SingleCharacterSet(CharacterSetConstants.LowercaseSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Uppercase,
                new SingleCharacterSet(CharacterSetConstants.UppercaseSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Numbers,
                new SingleCharacterSet(CharacterSetConstants.NumbersSet, true));
            characterSet.Characters.Add(
                CharacterSetConstants.Punctuation,
                new SingleCharacterSet(CharacterSetConstants.PunctuationSet, false));
            characterSet.Characters.Add(
                CharacterSetConstants.Space,
                new SingleCharacterSet(CharacterSetConstants.SpaceSet, false));
            characterSet.Characters.Add(
                CharacterSetConstants.Symbols,
                new SingleCharacterSet(CharacterSetConstants.SymbolsSet, false));

            var stringConfiguration = new StringConfiguration()
            {
                CharacterSet = characterSet,
                StringLength = BigInteger.Parse("10")
            };

            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "String3.json");
                await SerializationUtility.SerializeJsonAsync(stringConfiguration, path);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        public async Task GenerateDictionaryConfiguration()
        {
            var dictionaries = new List<RsgDictionary>()
            {
                new RsgDictionary()
                {
                    Name = "Corncob",
                    Description = "Corncob is a wordlist made by Mieliestronk found at http://www.mieliestronk.com/wordlist.html. Hyphenated words are included without the hypen, and a few word groups included are not delimited.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1WR002XgSPtw37xNob-obmnzzyC6sLmN5",
                    IsActive = true
                },

                new RsgDictionary()
                {
                    Name = "Jlawler",
                    Description = "Jlawler is a wordlist made by John Lawler http://www-personal.umich.edu/~jlawler/wordlist.html. The wordlist mentions having most of the words are rare, and obsolete. Doesn\u0027t include apostrophes.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1BBGxh6-wqRmvRup-AL4lNQ1bjqWLy20L",
                    IsActive = false
                },
                new RsgDictionary()
                {
                    Name = "MD5",
                    Description = "MD5 is a wordlist made by http://www.md5this.com/tools/wordlists.html. Using this slightly modified English version, with removed a few chunk of nouns. This huge wordlist contains so much words. Doesn\u2019t include apostrophes.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1kBLZGbHv18um4X8yof21wFQyXEd1PzVv",
                    IsActive = false
                },
                new RsgDictionary()
                {
                    Name = "Moby",
                    Description = "Moby is a wordlist made by Grady Ward found at http://www.gutenberg.org/ebooks/3201. This wordlist is gigantic and just has so many words its kinda hard to describe what it contains and doesn\u0027t. The webpage doesn\u0027t have too much information on it.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1Ro4JtPkTGx6Pub6yUQBrsAGNLXPbBRm4",
                    IsActive = false
                },
                new RsgDictionary()
                {
                    Name = "Scowl",
                    Description = "Scowl is a wordlist created by (http://wordlist.aspell.net/). Scowl stands for Spell Checker Oriented Word Lists. It\u0027s used to test how common a word is and etc. You can also create your own wordlist on this page, maybe worth checking out!",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1CQUFpva_ZENaOocliyrAnZf1e62kSylu",
                    IsActive = false
                },
                new RsgDictionary()
                {
                    Name = "Ridyhew",
                    Description = "Ridyhew is a wordlist created on the website (http://www.codehappy.net/wordlist.htm). Ridyhew stands for RIDiculouslY Huge English Wordlist. Contains common nouns, plurals, verbs with all legal inflections, foreign words if used in the English literature, compound words, and etc. The full eligibility list is on the website. I\u0027ll just say this is the largest default word list with this type of specificity I offer in the program and it contains so many words its incredible.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download\u0026id=1z7I_y-3EPoBg_UwzfW34Uv-M8HE3IbxG",
                    IsActive = false
                }
            };

            var dictionaryConfiguration = new DictionaryConfiguration()
            {
                AliterationCharacter = char.MinValue,
                AliterationFrequency = 1.0D,
                CapitalizeEachWord = false,
                Dictionaries = dictionaries,
                MaximumThreadCount = 0,
                NoiseFrequency = 1.0D,
                Priority = System.Threading.ThreadPriority.Normal,
                Source = string.Empty,
                UseNoise = false,
                UseSpace = true
            };

            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Dictionary.json");
                await SerializationUtility.SerializeJsonAsync(dictionaryConfiguration, path);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        public async Task CanDeserialize()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Dictionary.json");

            var dictionaryConfiguration = await SerializationUtility.DeserializeJsonAsync<DictionaryConfiguration>(path);

            var c = 1;
        }
    }
}
