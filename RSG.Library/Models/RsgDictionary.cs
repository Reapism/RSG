using System.Numerics;

namespace RSG.Core.Models
{
    public class RsgDictionary
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public BigInteger Size { get; internal set; }
        public BigInteger Length { get; internal set; }
        public Words Words { get; internal set; }
    }
}
