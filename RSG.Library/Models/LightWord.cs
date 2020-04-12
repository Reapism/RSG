using RSG.Core.Interfaces;

namespace RSG.Core.Models
{
    public class LightWord : IWord
    {
        /// <inheritdoc/>
        public string Word { get; set; }

        public LightWord(string word)
        {
            Word = word;
        }
    }
}
