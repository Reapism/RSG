namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a set of characters and whether its enabled.
    /// </summary>
    public class CharacterSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSet"/> class.
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
        /// Gets a value indicating the name of this <see cref="CharacterSet"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the characters representing this <see cref="CharacterSet"/>.
        /// </summary>
        public string Characters { get; }

        /// <summary>
        /// Gets or sets a value indicating whether whether this set is enabled for use.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
