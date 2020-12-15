using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a fully constructed <see cref="IRsgDictionary"/>.
    /// </summary>
    public class RsgDictionary : IRsgDictionary, IDictionaryWordList, IComparer<RsgDictionary>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSourceLocal { get; set; }

        public string Source { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public IDictionary<int, string> WordList { get; set; }

        [JsonIgnore]
        public BigInteger Count { get; set; }

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

            return this.Count == dictionary.Count &&
                this.Description == dictionary.Description &&
                this.IsActive == dictionary.IsActive &&
                this.IsSourceLocal == dictionary.IsSourceLocal &&
                this.Source == dictionary.Source &&
                this.Name == dictionary.Name;
        }

        public override string ToString()
        {
            string words;

            if (WordList is null)
            {
                words = string.Empty;
            }
            else
            {
                words = string.Join(", ", WordList.Values);
            }

            return $"Name: {Name}\nDescription: {Description}\nSource: {Source}\nIsActive: {IsActive}\nIsSourceLocal: {IsSourceLocal}\nCount: {Count}\nWordList: {words}";
        }
    }
}
