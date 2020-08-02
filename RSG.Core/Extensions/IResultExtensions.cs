using RSG.Core.Interfaces;
using RSG.Core.Models;
using System;
using System.Numerics;

namespace RSG.Core.Extensions
{
    public static class IResultExtensions
    {
        public static IResult Empty(this IResult value)
        {
            var negativeOne = BigInteger.Negate(BigInteger.One);

            return new Result()
            {
                CharacterList = string.Empty,
                EndTime = DateTime.UnixEpoch,
                StartTime = DateTime.UnixEpoch,
                Iterations = negativeOne,
                RandomizationType = Enums.RandomizationType.Pseudorandom,
                StringLength = negativeOne,
                Strings = new string[] { }
            };
        }
    }
}
