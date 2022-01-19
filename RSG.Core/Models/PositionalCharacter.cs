using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    /// <inheritdoc/>
    public class PositionalCharacter : IPositionalCharacter
    {
        /// <inheritdoc/>
        public int Position { get; set; }
        
        /// <inheritdoc/>
        public char Character { get; set; }
    }
}
