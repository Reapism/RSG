using System.Numerics;

namespace RSG.Core.Models
{
    public class WordListOption
    {
        /// <summary>
        /// Gets a value indicating whether the word list is loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance's
        /// source is from a local or web source.
        /// </summary>
        public bool IsSourceLocal { get; private set; }

        /// <summary>
        /// Gets the source path.
        /// <para>Can be a local file path or from a web source.</para>
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// Gets the number of bytes the word list is.
        /// </summary>
        public BigInteger WordCount { get; }

        /// <summary>
        /// Gets the number of bytes the word list is.
        /// </summary>
        public int NumberOfBytes { get; }

        /// <summary>
        /// Gets or sets the delimiter used to seperate words from other words in the word list source.
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// Sets the source and whether the source is local or not. Doing so, will require setting
        /// additional values.
        /// </summary>
        /// <param name="source">The source to load the wordlist from.</param>
        /// <param name="isSourceLocal">Specifies whether the <paramref name="source"/> is local or not.</param>
        /// <param name="delimiter">delimiter for the word list.</param>
        public void SetSource(string source, bool isSourceLocal, string delimiter)
        {
            Source = source;
            IsSourceLocal = isSourceLocal;
            Delimiter = delimiter;
        }
    }
}
