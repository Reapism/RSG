using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

namespace RSG.Core.Models
{
    // TODO fix the structure of this class. 
    // The interface should be 1to1 with this class.
    // ability to set wordlist outside ctor via method or something.
    /// <summary>
    /// Represents a fully constructed <see cref="IRsgDictionary"/>.
    /// </summary>
    public class RsgDictionary : IRsgDictionary, IDictionaryWordList, IComparer<RsgDictionary>
    {
        private readonly IDictionary<int, string> wordList;

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Description { get; }

        /// <inheritdoc/>
        public bool IsActive { get; }

        [JsonIgnore]
        public IDictionary<int, string> WordList { get; private set; } = new Dictionary<int, string>();

        /// <inheritdoc/>
        public WordListOption WordListOptions { get; }

        public RsgDictionary(string name, string description, IDictionary<int, string> wordList)
        {

        }

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
            if (obj is not RsgDictionary dictionary)
            {
                return false;
            }

            return
                Description == dictionary.Description &&
                IsActive == dictionary.IsActive &&
                WordListOptions.IsSourceLocal == dictionary.WordListOptions.IsSourceLocal &&
                WordListOptions.Source == dictionary.WordListOptions.Source &&
                WordListOptions.NumberOfBytes == dictionary.WordListOptions.NumberOfBytes &&
                WordListOptions.Delimiter == dictionary.WordListOptions.Delimiter &&
                WordListOptions.IsLoaded == dictionary.WordListOptions.IsLoaded &&
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

        public void SetWordList(IDictionary<int, string> wordList)
        {
            if (wordList is not null && wordList.Any())
            {
                WordList = wordList;
                //TODO need to set source
                //WordListOptions.SetSource(...)
            }
        }
    }
}
