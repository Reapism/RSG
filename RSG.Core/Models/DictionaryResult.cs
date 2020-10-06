using RSG.Core.Interfaces;

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
