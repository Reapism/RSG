using NUnit.Framework;
using RSG.Core.Configuration;
using RSG.Core.Constants;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace RSG.Core.Tests.Unit.Configuration
{
    [TestFixture]
    class StringConfigurationTests
    {
        public const string ExternalConfigurationName = "String.json";

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

            foreach (var charSet in stringConfiguration.Characters)
            {
                Assert.AreEqual(charSet.Name, stringConfigurationDeserialized.Characters.First(e => e.Name == charSet.Name).Name);
                Assert.AreEqual(charSet.Characters, stringConfigurationDeserialized.Characters.First(e => e.Characters == charSet.Characters).Characters);
                Assert.AreEqual(charSet.Enabled, stringConfigurationDeserialized.Characters.First(e => e.Enabled == charSet.Enabled).Enabled);
            }
        }

        private StringConfiguration CreateConfiguration()
        {
            var stringConfiguration = new StringConfiguration()
            {
                Characters = StringConfiguration.Default,
                StringLength = BigInteger.Zero
            };

            return stringConfiguration;
        }
    }
}
