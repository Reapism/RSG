using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Represents a generated word in the
    /// Dictionary.
    /// </summary>
    public interface IGeneratedWord
    {
        string Word { get; set; }

        IDictionary<int, IPositionCharacterPair> NoisyCharacterPositions { get; set; }
    }
}
