using NUnit.Framework;
using RSG.Core.Utilities;

namespace RSG.Core.Tests.Unit.Utilities
{
    [TestFixture]
    public class SerializationUtilityTests
    {
        [Test]
        public void Serialize()
        {
            var obj = 5;
            var json = SerializationUtility.SerializeJson(obj);
            var obj2 = SerializationUtility.DeserializeJson<int>(json);
            Assert.AreEqual(obj, obj2);
        }

    }
}
