using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Core.Models
{
    public class DetailedStatistics : IStatistics, IIterationsFrequency, ICharacterFrequency
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
        public IEnumerable<int> MostFrequentCharacterCount { get; set; }

        /// <inheritdoc/>
        public IEnumerable<int> LeastFrequentCharacterCount { get; set; }

        /// <inheritdoc/>
        public IEnumerable<char> MostFrequentCharacter { get; set; }

        /// <inheritdoc/>
        public IEnumerable<char> LeastFrequentCharacter { get; set; }

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

        public IStringRequest StringRequest => throw new NotImplementedException();

        public IDictionaryRequest DictionaryRequest => throw new NotImplementedException();

        public IStringResult StringResult => throw new NotImplementedException();

        public IDictionaryResult DictionaryResult => throw new NotImplementedException();

        public ICharacterFrequency CharacterFrequency => throw new NotImplementedException();

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
