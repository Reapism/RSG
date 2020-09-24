using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace RSG.Core.Tests.Unit.Configuration
{
    [TestFixture]
    class DictionaryConfigurationTests
    {
        public const string ExternalConfigurationName = "Dictionary.json";

        [TestCase(ExternalConfigurationName)]
        public void Serialize(string fileName)
        {
            var dictionaryConfiguration = CreateConfiguration();
            Assert.DoesNotThrow(() =>
                SerializationUtility.SerializeJson(dictionaryConfiguration, fileName, new JsonSerializerOptions() { WriteIndented = true })
            );

        }

        [Test]
        public void ConfigurationsAreEqualWhenDeserializing()
        {
            var dictionaryConfiguration = CreateConfiguration();
            Serialize(ExternalConfigurationName);

            var dictionaryConfigurationDeserialized = SerializationUtility.DeserializeJson<DictionaryConfiguration>(ExternalConfigurationName);

            Assert.AreEqual(dictionaryConfiguration.AliterationCharacter, dictionaryConfigurationDeserialized.AliterationCharacter);
            Assert.AreEqual(dictionaryConfiguration.AliterationFrequency, dictionaryConfigurationDeserialized.AliterationFrequency);
            Assert.AreEqual(dictionaryConfiguration.AliterationRange, dictionaryConfigurationDeserialized.AliterationRange);
            Assert.AreEqual(dictionaryConfiguration.CapitalizeEachWord, dictionaryConfigurationDeserialized.CapitalizeEachWord);
            Assert.AreEqual(dictionaryConfiguration.MaximumThreadCount, dictionaryConfigurationDeserialized.MaximumThreadCount);
            Assert.AreEqual(dictionaryConfiguration.NoiseFrequency, dictionaryConfigurationDeserialized.NoiseFrequency);
            Assert.AreEqual(dictionaryConfiguration.NoisePerWordRange, dictionaryConfigurationDeserialized.NoisePerWordRange);
            Assert.AreEqual(dictionaryConfiguration.Priority, dictionaryConfigurationDeserialized.Priority);
            Assert.AreEqual(dictionaryConfiguration.Source, dictionaryConfigurationDeserialized.Source);
            Assert.AreEqual(dictionaryConfiguration.UseAliteration, dictionaryConfigurationDeserialized.UseAliteration);
            Assert.AreEqual(dictionaryConfiguration.UseNoise, dictionaryConfigurationDeserialized.UseNoise);
            Assert.AreEqual(dictionaryConfiguration.UseSpace, dictionaryConfigurationDeserialized.UseSpace);

            for(var i = 0; i < dictionaryConfiguration.Dictionaries.Count; i++)
            {
                if (!dictionaryConfiguration.Dictionaries[i].Equals(dictionaryConfigurationDeserialized.Dictionaries[i]))
                {
                    Assert.Fail($"The dictionary at index {i} dont match\nExpected:\n{dictionaryConfiguration.Dictionaries[i]} \n\nActual:\n{dictionaryConfigurationDeserialized.Dictionaries[i]}");
                }
            }
        }

        private DictionaryConfiguration CreateConfiguration()
        {
            var dictionaries = new List<RsgDictionary>
            {
                new RsgDictionary()
                {
                    IsActive = true,
                    Name = "Corncob",
                    Description = "Corncob is a wordlist made by Mieliestronk found at http://www.mieliestronk.com/wordlist.html. Hyphenated words are included without the hypen, and a few word groups included are not delimited.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1WR002XgSPtw37xNob-obmnzzyC6sLmN5"
                },
                new RsgDictionary()
                {
                    IsActive = false,
                    Name = "Jlawler",
                    Description = "Jlawler is a wordlist made by John Lawler http://www-personal.umich.edu/~jlawler/wordlist.html. The wordlist mentions having most of the words are rare, and obsolete. Doesn't include apostrophes.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1BBGxh6-wqRmvRup-AL4lNQ1bjqWLy20L"
                },
                new RsgDictionary()
                {
                    IsActive = false,
                    Name = "MD5",
                    Description = "MD5 is a wordlist made by http://www.md5this.com/tools/wordlists.html. Using this slightly modified English version, with removed a few chunk of nouns. This huge wordlist contains so much words. Doesn't include apostrophes.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1kBLZGbHv18um4X8yof21wFQyXEd1PzVv"
                },
                new RsgDictionary()
                {
                    IsActive = false,
                    Name = "Moby",
                    Description = "Moby is a wordlist made by Grady Ward found at http://www.gutenberg.org/ebooks/3201. This wordlist is gigantic and just has so many words its kinda hard to describe what it contains and doesn't. The webpage doesn't have too much information on it.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1Ro4JtPkTGx6Pub6yUQBrsAGNLXPbBRm4"
                },
                new RsgDictionary()
                {
                    IsActive = false,
                    Name = "Scowl",
                    Description = "Scowl is a wordlist created by (http://wordlist.aspell.net/). Scowl stands for Spell Checker Oriented Word Lists. It's used to test how common a word is and etc. You can also create your own wordlist on this page, maybe worth checking out!",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1CQUFpva_ZENaOocliyrAnZf1e62kSylu"
                },
                new RsgDictionary()
                {
                    IsActive = false,
                    Name = "Ridyhew",
                    Description = "Ridyhew is a wordlist created on the website (http://www.codehappy.net/wordlist.htm). Ridyhew stands for RIDiculouslY Huge English Wordlist. Contains common nouns, plurals, verbs with all legal inflections, foreign words if used in the English literature, compound words, and etc. The full eligibility list is on the website. I'll just say this is the largest default word list with this type of specificity I offer in the program and it contains so many words its incredible.",
                    IsSourceLocal = false,
                    Source = "https://drive.google.com/uc?export=download&id=1z7I_y-3EPoBg_UwzfW34Uv-M8HE3IbxG"
                }
            };
            var dictionaryConfiguration = new DictionaryConfiguration()
            {
                AliterationCharacter = char.MinValue,
                AliterationFrequency = 0.0D,
                AliterationRange = new Range(new Index(0), new Index(0)),
                UseAliteration = false,
                CapitalizeEachWord = false,
                Dictionaries = dictionaries,
                MaximumThreadCount = 8,
                NoiseFrequency = 0.0D,
                NoisePerWordRange = new Range(new Index(0), new Index(0)),
                Priority = ThreadPriority.Normal,
                Source = null, // Will not be serialized.
                UseNoise = false,
                UseSpace = false
            };

            return dictionaryConfiguration;
        }
    }
}
