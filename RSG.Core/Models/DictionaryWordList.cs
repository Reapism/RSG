using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Core.Models
{
    public class DictionaryWordList : IDictionaryWordList
    {
        public BigInteger Count { get; set; }

        public IEnumerable<string> WordList { get; set; }
    }
}
