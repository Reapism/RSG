using NUnit.Framework;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
