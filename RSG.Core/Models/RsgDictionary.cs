using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Dictionary must have a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dictionary must contain a description.")]
        public string Description { get; set; }

        public bool IsSourceLocal { get; set; }

        [Required(ErrorMessage = "The source of the dictionary must be specified.")]
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
    }
}
