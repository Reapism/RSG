namespace RSG.Core.Models
{
    /// <summary>
    /// Represents a character paired with a position.
    /// </summary>
    public class PositionalCharacter
    {
        /// <summary>
        /// Gets the position of the <see cref="Character"/> relative to something.
        /// </summary>
        public int Position { get; init; }

        /// <summary>
        /// Gets the character.
        /// </summary>
        public char Character { get; init; }
    }
}
