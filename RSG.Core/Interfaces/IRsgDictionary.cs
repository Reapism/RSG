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
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the dictionary.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance's
        /// source is from a local or web source.
        /// </summary>
        bool IsSourceLocal { get; set; }

        /// <summary>
        /// Gets or sets the source path.
        /// <para>Can be a local file path or from a web source.</para>
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this dictionary is active.
        /// </summary>
        bool IsActive { get; set; }
    }
}