using RSG.Core.Extensions;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a <see cref="Log"/> file
    /// for RSG.
    /// <para>Used for formatting and highlighting certain properties at runtime.</para>
    /// </summary>
    public class Log<T> : IUniqueType<T>
        where T : class, IResult
    {
        private Guid identifier;
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="name">The name of the log.</param>
        /// <param name="description">The description of the log.</param>
        /// <param name="statistics">An instance of <see cref="StatisticsDetailed"/></param>
        public Log(string name, string description, DetailedStatistics statistics)
        {
            identifier = Guid.NewGuid();
            DateCreated = DateTime.Now;
            Name = name;
            Description = description;
            Statistics = statistics;
        }

        public T Result { get; set; }

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
        public DetailedStatistics Statistics { get; set; }

        public async Task ExportLogFile(string path, Encoding encoding)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            using var streamWriter = new StreamWriter(fileStream, encoding);

            await streamWriter.WriteAsync(ToString());
        }

        public string GenerateUniqueChecksum(T type)
        {
            throw new NotImplementedException();
        }

        public string UniqueChecksum { get; }

        public override string ToString()
        {
            var dictionary = this.GetType().GetPublicProperties<T>();
            var strBuilder = new StringBuilder();

            foreach (var entry in dictionary)
            {
                strBuilder
                    .AppendLine($"{entry.Key}: {GetString(entry.Value.Item1, entry.Value.Item2)}");
            }

            return strBuilder.ToString();
        }

        private string GetString(string typeName, object obj)
        {
            var type = Type.GetType(typeName);
            var isStruct = type.IsValueType && !type.IsEnum;

            if (!isStruct)
            {
                return obj.ToString();
            }

            if (obj.GetType().Name.Equals(nameof(DateTime)))
            {
                return ((DateTime)obj).ToString();
            }
            else if (obj.GetType().Name.Equals(nameof(TimeSpan)))
            {
                var ob = (TimeSpan)obj;
                return Math.Round(ob.TotalSeconds, 2).ToString();
            }

            return string.Empty;
        }
    }
}
