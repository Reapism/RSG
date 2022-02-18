using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Models
{
    public class IterationsFrequency : IIterationsFrequencyProvider
    {
        public IterationsFrequency()
        {

        }

        public IEnumerable<FrequencyUnit> IterationFrequencies { get; }

        public FrequencyUnit FromUnit(string unit)
        {
            return IterationFrequencies.FirstOrDefault(e => e.Unit == unit);
        }

        public static BigInteger IterationsPerMinute(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(iterationsPerSecond, new BigInteger(60));

        public static BigInteger IterationsPerHour(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerMinute(iterationsPerSecond), new BigInteger(60));

        public static BigInteger IterationsPerDay(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerHour(iterationsPerSecond), new BigInteger(24));

        public static BigInteger IterationsPerYear(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerDay(iterationsPerSecond), new BigInteger(365));

        public static BigInteger IterationsPerDecade(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerYear(iterationsPerSecond), new BigInteger(10));

        public static BigInteger IterationsPerCentury(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerDecade(iterationsPerSecond), new BigInteger(10));

        public static BigInteger IterationsPerMillennium(BigInteger iterationsPerSecond)
            => BigInteger.Multiply(IterationsPerCentury(iterationsPerSecond), new BigInteger(10));
    }
}
