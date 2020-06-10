using NUnit.Framework;
using RSG.Core.Models;

namespace RSG.Core.Tests.Unit.Models
{
    [TestFixture]
    internal class RsgDictionaryTests
    {
        [TestCase("a", "b", -1)]
        [TestCase("b", "a", 1)]
        [TestCase("a", "a", 0)]
        [TestCase("a", null, 1)]
        [TestCase(null, "a", -1)]
        [TestCase(null, null, 0)]
        public void CompareTest(string name1, string name2, int expected)
        {
            var dictionary1 = new RsgDictionary()
            {
                Name = name1
            };

            var dictionary2 = new RsgDictionary()
            {
                Name = name2
            };

            var actual = dictionary1.Compare(dictionary1, dictionary2);

            Assert.AreEqual(actual, expected);
        }
    }
}
