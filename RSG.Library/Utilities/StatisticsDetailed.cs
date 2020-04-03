using RSG.Library.Interfaces;
using System;
using System.Numerics;
using System.Text;

namespace RSG.Library.Utilities
{
    public class StatisticsDetailed : IStatistics, IIterationsFrequency, ICharacterFrequency
    {
        public BigInteger StringLength { get; set; }
        public BigInteger Iterations { get; set; }
        public BigInteger Permutations { get; set; }
        public string RandomizationType { get; set; }
        public string CharacterList { get; set; }
        public BigInteger MostFrequentCharacterNumber { get; set; }
        public BigInteger LeastFrequentCharacterNumber { get; set; }
        public char MostFrequentCharacter { get; set; }
        public char LeastFrequentCharacter { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan IterationsPerSecond { get; set; }
        public TimeSpan IterationsPerMinute { get; set; }
        public TimeSpan IterationsPerHour { get; set; }
        public TimeSpan IterationsPerDay { get; set; }
        public TimeSpan IterationsPerYear { get; set; }
        public TimeSpan IterationsPerCentury { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine(Duration.Ticks.ToString("n0"))
                .AppendLine(IterationsPerSecond.Ticks.ToString("n0"))
                .AppendLine(IterationsPerMinute.Ticks.ToString("n0"))
                .AppendLine(IterationsPerHour.Ticks.ToString("n0"))
                .AppendLine(IterationsPerDay.Ticks.ToString("n0"))
                .AppendLine(IterationsPerYear.Ticks.ToString("n0"))
                .AppendLine(IterationsPerCentury.Ticks.ToString("n0"));

            return stringBuilder.ToString();
        }
    }
}
