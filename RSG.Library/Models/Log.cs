using RSG.Library.Utilities;
using System;

namespace RSG.Library.Models
{
    /// <summary>
    /// Represents a <see cref="Log"/> file
    /// for RSG.
    /// <para>Used for formatting and highlighting certain properties at runtime.</para>
    /// </summary>
    public class Log
    {
        public DetailedStatistics statistics { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
