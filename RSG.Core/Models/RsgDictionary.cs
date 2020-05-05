using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    public class RsgDictionary : IRsgDictionary, IComparer<RsgDictionary>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSourceLocal { get; set; }

        public string Source { get; set; }

        [JsonIgnore]
        public IWordList WordList { get; set; }

        [JsonIgnore]
        public BigInteger Count { get; set; }

        public int Compare([AllowNull] RsgDictionary x, [AllowNull] RsgDictionary y)
        {
            return string.Compare(x.Name, y.Name, true, CultureInfo.InvariantCulture);
        }
    }
}
