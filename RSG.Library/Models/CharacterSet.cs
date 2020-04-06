using System;

namespace RSG.Library.Models
{
    /// <summary>
    /// Represents a set of characters and whether its enabled.
    /// </summary>
    [Serializable]
    public class CharacterSet
    {
        /// <summary>
        /// Initialize a parameterized <see cref="CharacterSet"/>.
        /// </summary>
        /// <param name="characters">The characters representing this <see cref="CharacterSet"/>.</param>
        /// <param name="enabled">Whether this set is enabled for use.</param>
        public CharacterSet(string characters, bool enabled)
        {
            Characters = characters;
            Enabled = enabled;
        }

        /// <summary>
        /// The characters representing this <see cref="CharacterSet"/>.
        /// </summary>
        public string Characters { get; set; }

        /// <summary>
        /// Whether this set is enabled for use.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
