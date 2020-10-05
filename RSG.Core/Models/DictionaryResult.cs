using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents properties of an RSG dictionary.
    /// </summary>
    public class DictionaryResult : ResultBase, IDictionaryResult
    {
        public IRsgDictionary Dictionary { get; set; }

        public WordContainer Words { get; set; }
    }
}
