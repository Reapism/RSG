using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IDictionaryWordList
    {
        IDictionary<int, string> WordList { get; set; }

        BigInteger Count { get; set; }
    }
}
