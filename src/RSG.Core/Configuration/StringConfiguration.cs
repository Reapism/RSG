using RSG.Core.Constants;
using RSG.Core.Extensions;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : ICharacterSetProvider
    {
        private static IList<CharacterSet> DefaultCharacterSet =>
            new List<CharacterSet>()
            {
                new CharacterSet(CharacterSetConstants.Lowercase, CharacterSetConstants.LowercaseSet, true),
                new CharacterSet(CharacterSetConstants.Uppercase, CharacterSetConstants.UppercaseSet, true),
                new CharacterSet(CharacterSetConstants.Numbers, CharacterSetConstants.NumbersSet, true),
                new CharacterSet(CharacterSetConstants.Punctuation, CharacterSetConstants.PunctuationSet, true),
                new CharacterSet(CharacterSetConstants.Space, CharacterSetConstants.SpaceSet, true),
                new CharacterSet(CharacterSetConstants.Symbols, CharacterSetConstants.SymbolsSet, true)
            };

        public StringConfiguration()
        {
            CharacterSets = new List<CharacterSet>();
        }

        /// <summary>
        /// Gets the default character sets.
        /// </summary>
        public static IList<CharacterSet> Default
        {
            get
            {
                return DefaultCharacterSet;
            }
        }

        public BigInteger StringLength { get; set; }

        public BigInteger Iterations { get; set; }

        public IList<CharacterSet> CharacterSets { get; set; }

        public char[] ToCharArray()
        {
            return CharacterSets.ToCharArray();
        }
    }
}