using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents the minimal information needed for a
    /// dictionary.
    /// </summary>
    public interface IRsgDictionary
    {
        /// <summary>
        /// Gets or sets the name of the dictionary.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the dictionary.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance's
        /// source is from a local or web source.
        /// </summary>
        public bool IsSourceLocal { get; set; }

        /// <summary>
        /// Gets or sets the source path.
        /// <para>Can be a local file path or from a web source.</para>
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the number of elements in the word list.
        /// </summary>
        public BigInteger Count { get; set; }

        /// <summary>
        /// Gets or sets the word list.
        /// </summary>
        public IWordList WordList { get; set; }
    }
}