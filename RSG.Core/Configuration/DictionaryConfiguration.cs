using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;

namespace RSG.Core.Configuration
{
    public class DictionaryConfiguration : IDictionaryConfiguration
    {
        public IRsgDictionary Dictionary { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool UseSpace { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool CapitalizeEachWord { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public char AliterationCharacter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}