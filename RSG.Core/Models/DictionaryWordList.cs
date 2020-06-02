using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a word list for a dictionary.
    /// </summary>
    public class DictionaryWordList : IDictionaryWordList
    {
        public IDictionary<int, string> WordList { get; set; }

        public BigInteger Count { get; set; }
    }
}
