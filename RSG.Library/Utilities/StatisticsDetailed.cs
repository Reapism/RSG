using RSG.Core.Interfaces;
using System;
using System.Numerics;
using System.Text;

namespace RSG.Core.Utilities
{
    public class StatisticsDetailed : IStatistics, IIterationsFrequency, ICharacterFrequency
    {
        /// <inheritdoc/>
        public BigInteger StringLength { get; set; }
        /// <inheritdoc/>
        public BigInteger Iterations { get; set; }
        /// <inheritdoc/>
        public BigInteger Permutations { get; set; }
        /// <inheritdoc/>
        public string RandomizationType { get; set; }
        /// <inheritdoc/>
        public string CharacterList { get; set; }
        /// <inheritdoc/>
        public BigInteger MostFrequentCharacterNumber { get; set; }
        /// <inheritdoc/>
        public BigInteger LeastFrequentCharacterNumber { get; set; }
        /// <inheritdoc/>
        public char MostFrequentCharacter { get; set; }
        /// <inheritdoc/>
        public char LeastFrequentCharacter { get; set; }
        /// <inheritdoc/>
        public TimeSpan Duration { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerSecond { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerMinute { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerHour { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerDay { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerYear { get; set; }
        /// <inheritdoc/>
        public TimeSpan IterationsPerCentury { get; set; }

        /// <inheritdoc/>
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
