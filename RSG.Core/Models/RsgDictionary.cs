using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Models
{
    public class RsgDictionary : IRsgDictionary, IWordList, IComparer<RsgDictionary>
    {
        [Required(ErrorMessage = "Dictionary must have a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dictionary must contain a description.")]
        public string Description { get; set; }

        public bool IsSourceLocal { get; set; }

        [Required(ErrorMessage = "The source of the dictionary must be specified.")]
        public string Source { get; set; }

        [JsonIgnore]
        public IEnumerable<string> WordList { get; set; }

        [JsonIgnore]
        public BigInteger Count { get; set; }

        public int Compare([AllowNull] RsgDictionary x, [AllowNull] RsgDictionary y)
        {
            return string.Compare(x.Name, y.Name, true, CultureInfo.InvariantCulture);
        }
    }
}
