using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Core.Interfaces.Request
{
    public interface IStringRequest : ICharacterSetProvider
    {
        BigInteger StringLength { get; set; }

        BigInteger Iterations { get; set; }
    }

    public interface IDictionaryRequest : IDictionaryProvider
    {
        RsgDictionary Dictionary { get; set; }
        BigInteger Iterations { get; set; }
    }
}
