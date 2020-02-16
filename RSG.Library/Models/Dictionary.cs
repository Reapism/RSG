using System.Collections.Generic;
using System.Numerics;

namespace RSG.Library.Models
{
    internal class Dictionary
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public BigInteger Size { get; private set; }
        public BigInteger Length { get; private set; }
        public IEnumerable<string> Words { get; private set; }
    }
}
