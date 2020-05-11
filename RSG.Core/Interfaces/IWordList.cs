using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Core.Interfaces
{
    public interface IWordList
    {
        IEnumerable<string> WordList { get; set; }

        BigInteger Count { get; set; }
    }
}
