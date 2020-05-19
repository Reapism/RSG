using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using System.Numerics;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : IStringConfiguration
    {
        public BigInteger StringLength { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ICharacterSets CharacterSet { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}