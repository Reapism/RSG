using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IDictionaryWordList
    {
        IEnumerable<string> WordList { get; set; }

        BigInteger Count { get; set; }
    }
}
