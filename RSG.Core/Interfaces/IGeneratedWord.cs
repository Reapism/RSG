using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents a generated word in the
    /// Dictionary.
    /// </summary>
    public interface IGeneratedWord
    {
        /// <summary>
        /// Gets the original, unmodified generated word.
        /// </summary>
        string Word { get; }

        /// <summary>
        /// Gets the locations of additional character positions that were part of the generation settings.
        /// </summary>
        IEnumerable<IPositionalCharacter> AdditionalCharacterPositions { get; }

        /// <summary>
        /// Provides the ability to render the <see cref="Word"/> with potential <see cref="AdditionalCharacterPositions"/>.
        /// </summary>
        /// <returns>A rendered word computed via <see cref="AdditionalCharacterPositions"/></returns>
        string RenderWord();
    }
}
