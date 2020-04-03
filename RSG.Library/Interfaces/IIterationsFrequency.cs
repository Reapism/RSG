using System;

namespace RSG.Library.Interfaces
{
    internal interface IIterationsFrequency
    {
        TimeSpan Duration { get; set; }
        TimeSpan IterationsPerSecond { get; set; }
        TimeSpan IterationsPerMinute { get; set; }
        TimeSpan IterationsPerHour { get; set; }
        TimeSpan IterationsPerDay { get; set; }
        TimeSpan IterationsPerYear { get; set; }
        TimeSpan IterationsPerCentury { get; set; }
    }
}
