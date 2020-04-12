﻿using RSG.Core.Utilities;
using System;
using System.ComponentModel;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a <see cref="Log"/> file
    /// for RSG.
    /// <para>Used for formatting and highlighting certain properties at runtime.</para>
    /// </summary>
    public class Log
    {
        public Log(string name, string description, StatisticsDetailed statistics)
        {
            DateCreated = DateTime.Now;
            Name = name;
            Description = description;
            Statistics = statistics;
        }

        public Log() :
            this(DateTime.Now.ToLongTimeString(), $"AutoGenerated - Log Description", new StatisticsDetailed())
        { }

        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the log.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date and time in which this log was created.
        /// </summary>
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets represents the detailed statics for the log.
        /// </summary>
        public StatisticsDetailed Statistics { get; set; }
    }
}
