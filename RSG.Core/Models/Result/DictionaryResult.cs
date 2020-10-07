using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;

namespace RSG.Core.Models.Result
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
