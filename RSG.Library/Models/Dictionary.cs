using System.Collections.Generic;
using System.Numerics;

namespace RSG.Library.Models
{
    internal class Dictionary
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public BigInteger Size { get; internal set; }
        public BigInteger Length { get; internal set; }
        public IEnumerable<string> Words { get; internal set; }
    }
}
