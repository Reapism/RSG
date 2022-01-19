namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents a character paired with a position.
    /// </summary>
    public interface IPositionalCharacter
    {
        /// <summary>
        /// Gets or sets the position of the <see cref="Character"/> relative to something.
        /// </summary>
        int Position { get; set; }

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        char Character { get; set; }
    }
}