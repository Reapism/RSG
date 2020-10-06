using RSG.Core.Constants;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : IStringConfiguration
    {
        public StringConfiguration()
        {
            Characters = new Dictionary<string, SingleCharacterSet>();
            GetDefault(Characters);
        }

        public void GetDefault(IDictionary<string, SingleCharacterSet> characters)
        {
            characters.Add(
                CharacterSetConstants.Lowercase,
                new SingleCharacterSet(CharacterSetConstants.LowercaseSet, true));
            characters.Add(
                CharacterSetConstants.Uppercase,
                new SingleCharacterSet(CharacterSetConstants.UppercaseSet, true));
            characters.Add(
                CharacterSetConstants.Numbers,
                new SingleCharacterSet(CharacterSetConstants.NumbersSet, true));
            characters.Add(
                CharacterSetConstants.Punctuation,
                new SingleCharacterSet(CharacterSetConstants.PunctuationSet, true));
            characters.Add(
                CharacterSetConstants.Space,
                new SingleCharacterSet(CharacterSetConstants.SpaceSet, true));
            characters.Add(
                CharacterSetConstants.Symbols,
                new SingleCharacterSet(CharacterSetConstants.SymbolsSet, true));
        }

        public BigInteger StringLength { get; set; }

        public IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}