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
        public Log(string name, string description, StatisticsDetailed statistics) : this()
        {

        }
        public Log()
        {
            DateCreated = DateTime.Now;
        }

        /// <summary>
        /// The name of the log.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the log.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time in which this log was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Represents the detailed statics for the log.
        /// </summary>
        public StatisticsDetailed Statistics { get; set; }
    }
}
