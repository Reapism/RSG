namespace RSG.Core.Models
{
    public class CharacterOccurence
    {
        public CharacterOccurence(char character, int count)
        {
            Character = character;
            Count = count;
        }

        public char Character { get; }
        public int Count { get; }
    }
}
