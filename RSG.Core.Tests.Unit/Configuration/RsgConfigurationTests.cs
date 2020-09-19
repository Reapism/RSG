﻿using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RSG.Core.Tests.Unit.Configuration
{
    [TestFixture]
    class RsgConfigurationTests
    {
        public const string ExternalConfigurationName = "RSG.config";

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

            var stringConfigurationDeserialized = SerializationUtility.DeserializeJson<RsgConfiguration>(ExternalConfigurationName);

            Assert.IsTrue(stringConfiguration.Equals(stringConfigurationDeserialized));
        }

        private RsgConfiguration CreateConfiguration()
        {
            var rsgConfiguration = new RsgConfiguration()
            {
                CheckForUpdatesOnLoad = true,
                CopySelectionsToClipboard = true,
                CurrentVersion = new Version(1, 0, 0, 0),
                DictionaryConfigurationSource = null,
                FirstTimeUsingCurrentVersion = true,
                NumberOfLaunchesThisVersion = 1,
                NumberOfLaunchesTotal = 100,
                RandomizationType = Enums.RandomizationType.Pseudorandom,
                StringConfigurationSource = null,
                UseStickyWindows = false
            };

            return rsgConfiguration;
        }
    }
}