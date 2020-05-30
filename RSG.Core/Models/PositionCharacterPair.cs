using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    public class PositionCharacterPair : IPositionCharacterPair
    {
        public int Position { get; set; }

        public char Character { get; set; }
    }
}
