using System.Text;

namespace RSG.Strings
{
    /// <summary>
    /// Represents a set of characters and whether its enabled.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CharacterSetHolder"/> class.
    /// </remarks>
    /// <param name="name">The name representing this <see cref="CharacterSetHolder"/>.</param>
    /// <param name="characters">The characters representing this <see cref="CharacterSetHolder"/>.</param>
    /// <param name="enabled">Whether this set is enabled for use.</param>
    public class CharacterSetHolder(string name, string characters, bool enabled)
    {
        /// <summary>
        /// Gets a value indicating the name of this <see cref="CharacterSetHolder"/>.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Gets the characters representing this <see cref="CharacterSetHolder"/>.
        /// </summary>
        public string Characters { get; } = characters;

        /// <summary>
        /// Gets or sets a value indicating whether whether this set is enabled for use.
        /// </summary>
        public bool Enabled { get; set; } = enabled;

        private static char[] ToCharArray(IEnumerable<CharacterSetHolder> characterSets)
        {
            var builder = new StringBuilder();
            foreach (var set in characterSets)
            {
                builder.Append(set.Characters);
            }

            var characters = builder.ToString().ToCharArray();
            return characters;
        }

        public static char[] ToCharArray(IEnumerable<CharacterSetHolder> characterSets, bool onlyEnabled = true)
        {
            if (onlyEnabled)
                return ToCharArray(characterSets.Where(e => e.Enabled == onlyEnabled));

            return ToCharArray(characterSets);
        }

        public static CharacterSetHolder Lowercase() => new(nameof(Lowercase), "abcdefghijklmnopqrstuvwxyz", true);
        public static CharacterSetHolder Uppercase() => new(nameof(Uppercase), "ABCDEFGHIJKLMNOPQRSTUVWXYZ", true);
        public static CharacterSetHolder Numbers() => new(nameof(Numbers), "0123456789", true);
        public static CharacterSetHolder Punctuation() => new(nameof(Punctuation), ".,!?;:", true);

        public static IList<CharacterSetHolder> Default => [Uppercase(), Numbers()];
        public static IList<CharacterSetHolder> AllSets => [Lowercase(), Uppercase(), Numbers(), Punctuation()];
    }
}
