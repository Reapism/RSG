using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Models
{
    public class WordListOption
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance's
        /// source is from a local or web source.
        /// </summary>
        public bool IsSourceLocal { get; }

        /// <summary>
        /// Gets or sets the source path.
        /// <para>Can be a local file path or from a web source.</para>
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Gets the number of bytes the word list is.
        /// </summary>
        public int NumberOfBytes { get; }

        /// <summary>
        /// Gets the delimiter used to seperate words from other words in the word list source.
        /// </summary>
        public string Delimiter { get; }
    }
    /// <summary>
    /// Represents a fully constructed <see cref="IRsgDictionary"/>.
    /// </summary>
    public class RsgDictionary : IRsgDictionary, IDictionaryWordList, IComparer<RsgDictionary>
    {
        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Description { get; }

        /// <inheritdoc/>
        public bool IsActive { get; }

        [JsonIgnore]
        public IDictionary<int, string> WordList { get; }

        [JsonIgnore]
        public BigInteger Count { get; }

        /// <inheritdoc/>
        public WordListOption WordListOptions { get; }

        /// <summary>
        /// Compares the <see cref="Name"/>
        /// property and returns the result.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns the string comparison on the <see cref="Name"/>
        /// property.</returns>
        public int Compare([AllowNull] RsgDictionary x, [AllowNull] RsgDictionary y)
        {
            return string.Compare(x?.Name, y?.Name, true, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Checks if the object is equal to this instance.
        /// <para>Ignores the <see cref="WordList"/> property.</para>
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>Whether the object parameter and this instance are equal.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is RsgDictionary dictionary))
            {
                return false;
            }

            return Count == dictionary.Count &&
                Description == dictionary.Description &&
                IsActive == dictionary.IsActive &&
                WordListOptions.IsSourceLocal == dictionary.WordListOptions.IsSourceLocal &&
                WordListOptions.Source == dictionary.WordListOptions.Source &&
                WordListOptions.NumberOfBytes == dictionary.WordListOptions.NumberOfBytes &&
                WordListOptions.Delimiter == dictionary.WordListOptions.Delimiter &&
                Name == dictionary.Name;
        }

        /// <summary>
        /// Returns the <see cref="Name"/> and <see cref="Description"/>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name: {Name}\nDescription: {Description}";
        }
    }
}
