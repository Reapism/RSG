﻿using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Constants;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace RSG.Core.Tests.Unit.Configuration
{
    [TestFixture]
    class StringConfigurationTests
    {
        public const string ExternalConfigurationName = "String.config";

        [TestCase(ExternalConfigurationName)]
        public void Serialize(string fileName)
        {
            var stringConfiguration = CreateConfiguration();
            Assert.DoesNotThrow(() =>
                SerializationUtility.SerializeJson(stringConfiguration, fileName, new JsonSerializerOptions() { WriteIndented = true })
            );

        }

        [Test]
        public void ConfigurationsAreEqualWhenDeserializing()
        {
            var stringConfiguration = CreateConfiguration();
            Serialize(ExternalConfigurationName);
            
            var stringConfigurationDeserialized = SerializationUtility.DeserializeJson<StringConfiguration>(ExternalConfigurationName);

            Assert.IsTrue(stringConfiguration.Equals(stringConfigurationDeserialized));
        }

        private StringConfiguration CreateConfiguration()
        {
            var characters = new Dictionary<string, SingleCharacterSet>();
            characters.Add(CharacterSetConstants.Lowercase, new SingleCharacterSet(CharacterSetConstants.LowercaseSet, true));
            characters.Add(CharacterSetConstants.Uppercase, new SingleCharacterSet(CharacterSetConstants.UppercaseSet, true));
            characters.Add(CharacterSetConstants.Numbers, new SingleCharacterSet(CharacterSetConstants.NumbersSet, true));
            characters.Add(CharacterSetConstants.Space, new SingleCharacterSet(CharacterSetConstants.SpaceSet, false));
            characters.Add(CharacterSetConstants.Punctuation, new SingleCharacterSet(CharacterSetConstants.PunctuationSet, false));
            characters.Add(CharacterSetConstants.Symbols, new SingleCharacterSet(CharacterSetConstants.SymbolsSet, false));

            var stringConfiguration = new StringConfiguration()
            {
                Characters = characters,
                StringLength = BigInteger.Zero
            };

            return stringConfiguration;
        }
    }
}