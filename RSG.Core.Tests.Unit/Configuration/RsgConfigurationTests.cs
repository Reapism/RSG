using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace RSG.Core.Tests.Unit.Configuration
{
    [TestFixture]
    class RsgConfigurationTests
    {
        public const string ExternalConfigurationName = "RSG.json";

        [TestCase(ExternalConfigurationName)]
        public void Serialize(string fileName)
        {
            var rsgConfiguration = CreateConfiguration();
            Assert.DoesNotThrow(() =>
                SerializationUtility.SerializeJson(rsgConfiguration, fileName, new JsonSerializerOptions() { WriteIndented = true })
            );

        }

        [Test]
        public void ConfigurationsAreEqualWhenDeserializing()
        {
            var rsgConfiguration = CreateConfiguration();
            Serialize(ExternalConfigurationName);

            var rsgConfigurationDeserialized = SerializationUtility.DeserializeJson<RsgConfiguration>(ExternalConfigurationName);

            Assert.AreEqual(rsgConfiguration.CheckForUpdatesOnLoad, rsgConfigurationDeserialized.CheckForUpdatesOnLoad);
            Assert.AreEqual(rsgConfiguration.CopySelectionsToClipboard, rsgConfigurationDeserialized.CopySelectionsToClipboard);
            Assert.AreEqual(rsgConfiguration.CurrentVersion, rsgConfigurationDeserialized.CurrentVersion);
            Assert.AreEqual(rsgConfiguration.DictionaryConfigurationSource, rsgConfigurationDeserialized.DictionaryConfigurationSource);
            Assert.AreEqual(rsgConfiguration.FirstTimeUsingCurrentVersion, rsgConfigurationDeserialized.FirstTimeUsingCurrentVersion);
            Assert.AreEqual(rsgConfiguration.NumberOfLaunchesThisVersion, rsgConfigurationDeserialized.NumberOfLaunchesThisVersion);
            Assert.AreEqual(rsgConfiguration.RandomizationType, rsgConfigurationDeserialized.RandomizationType);
            Assert.AreEqual(rsgConfiguration.StringConfigurationSource, rsgConfigurationDeserialized.StringConfigurationSource);
            Assert.AreEqual(rsgConfiguration.UseStickyWindows, rsgConfigurationDeserialized.UseStickyWindows);
        }

        private RsgConfiguration CreateConfiguration()
        {
            var rsgConfiguration = new RsgConfiguration()
            {
                CheckForUpdatesOnLoad = true,
                CopySelectionsToClipboard = true,
                CurrentVersion = "1.0.0.0",
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
