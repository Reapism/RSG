using RSG.Core.Enums;
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
}