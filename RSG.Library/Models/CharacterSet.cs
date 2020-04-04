using System;

namespace RSG.Library.Models
{
    /// <summary>
    /// Represents a set of characters with a name, and whether its enabled.
    /// </summary>
    [Serializable]
    internal class CharacterSet
    {
        /// <summary>
        /// Initialize a parameterized <see cref="CharacterSet"/>.
        /// </summary>
        /// <param name="name">The name representing this <see cref="CharacterSet"/>.</param>
        /// <param name="characters">The characters representing this <see cref="CharacterSet"/>.</param>
        /// <param name="enabled">Whether this set is enabled for use.</param>
        public CharacterSet(string name, string characters, bool enabled)
        {
            Name = name;
            Characters = characters;
            Enabled = enabled;
        }

        /// <summary>
        /// The name representing this <see cref="CharacterSet"/>.
        /// </summary>
        public string Name { get; set; }

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
