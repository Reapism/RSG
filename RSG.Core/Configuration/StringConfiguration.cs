using RSG.Core.Constants;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : IStringConfiguration
    {
        public StringConfiguration()
        {
            Characters = new Dictionary<string, SingleCharacterSet>();

        }

        public void GetDefault()
        {
            Characters.Add(
                CharacterSetConstants.Lowercase,
                new SingleCharacterSet(CharacterSetConstants.LowercaseSet, true));
            Characters.Add(
                CharacterSetConstants.Uppercase,
                new SingleCharacterSet(CharacterSetConstants.UppercaseSet, true));
            Characters.Add(
                CharacterSetConstants.Numbers,
                new SingleCharacterSet(CharacterSetConstants.NumbersSet, true));
            Characters.Add(
                CharacterSetConstants.Punctuation,
                new SingleCharacterSet(CharacterSetConstants.PunctuationSet, true));
            Characters.Add(
                CharacterSetConstants.Space,
                new SingleCharacterSet(CharacterSetConstants.SpaceSet, true));
            Characters.Add(
                CharacterSetConstants.Symbols,
                new SingleCharacterSet(CharacterSetConstants.SymbolsSet, true));
        }

        [JsonIgnore]
        public BigInteger StringLength { get; set; }

        public IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}