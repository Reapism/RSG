using RSG.Core.Enums;
using RSG.Core.Models;
using System;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IResult
    {
        RandomizationType RandomizationType { get; set; }

        BigInteger Iterations { get; set; }

        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }

    public interface IDictionaryResult : IResult
    {
        RsgDictionary RsgDictionary { get; set; }
    }
}