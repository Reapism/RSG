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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public StatisticsDetailed Statistics { get; set; }
    }
}
