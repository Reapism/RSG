using System;
using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    public class IterationsFrequency : IIterationsFrequency
    {
        public TimeSpan Duration { get; set; }

        public TimeSpan IterationsPerSecond { get; set; }

        public TimeSpan IterationsPerMinute { get; set; }

        public TimeSpan IterationsPerHour { get; set; }

        public TimeSpan IterationsPerDay { get; set; }

        public TimeSpan IterationsPerYear { get; set; }

        public TimeSpan IterationsPerCentury { get; set; }
    }
}
