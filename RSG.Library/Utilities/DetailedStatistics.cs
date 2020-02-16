using System;
using System.Text;

namespace RSG.Library.Utilities
{
    public class DetailedStatistics
    {
        private Statistics instance;

        /// <summary>
        /// Represents the <see cref="Duration"/> of the
        /// generation run given the settings.
        /// </summary>
        public TimeSpan Duration { get; set; }
        public TimeSpan IterationsPerSecond { get; set; }
        public TimeSpan IterationsPerMinute { get; set; }
        public TimeSpan IterationsPerHour { get; set; }
        public TimeSpan IterationsPerDay { get; set; }
        public TimeSpan IterationsPerYear { get; set; }
        public TimeSpan IterationsPerCentury { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder(instance.ToString());

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
