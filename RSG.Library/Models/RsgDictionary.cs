using System.Numerics;

namespace RSG.Core.Models
{
    /// <summary>
    /// Represents properties of an RSG dictionary.
    /// </summary>
    public class RsgDictionary
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
        /// Gets or sets the size of the dictionary.
        /// </summary>
        public BigInteger Size { get; set; }

        /// <summary>
        /// Gets or sets the word count of the dictionary.
        /// </summary>
        public BigInteger WordCount { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="WordsCollection"/>.
        /// </summary>
        public WordsCollection Words { get; set; }
    }
}
