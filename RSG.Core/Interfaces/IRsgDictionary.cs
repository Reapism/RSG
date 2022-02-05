using RSG.Core.Models;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents the minimal information needed for a
    /// dictionary.
    /// </summary>
    public interface IRsgDictionary
    {
        /// <summary>
        /// Gets the name of the dictionary.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the dictionary.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether this dictionary is active or not.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets a value indicating the options of the word list.
        /// </summary>
        WordListOption WordListOptions { get; }
    }
}