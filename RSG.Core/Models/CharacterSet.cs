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
        /// <param name="characters">The characters representing this <see cref="CharacterSet"/>.</param>
        /// <param name="enabled">Whether this set is enabled for use.</param>
        public CharacterSet(string characters, bool enabled)
        {
            Characters = characters;
            Enabled = enabled;
        }

        /// <summary>
        /// Gets or sets the characters representing this <see cref="CharacterSet"/>.
        /// </summary>
        public string Characters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether this set is enabled for use.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
