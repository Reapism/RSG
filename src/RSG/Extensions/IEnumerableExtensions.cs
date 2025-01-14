using RSG.Strings;

namespace RSG.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Converts enabled character sets into a single <see cref="char"/>[].
        /// </summary>
        /// <param name="characters">The character sets.</param>
        /// <returns>A <see cref="char"/>[].</returns>
        public static char[] ToCharArray(this IEnumerable<CharacterSetHolder> characters)
        {
            var charStrings = characters.Where(c => c.Enabled)
                .Select(c => c.Characters);
            var charArray = string.Join(string.Empty, charStrings).ToCharArray();

            return charArray;
        }

        public static int GetTotalCharacterCount(this IEnumerable<CharacterSetHolder> characters)
        {
            return Flatten(characters).Length;
        }

        public static string Flatten(this IEnumerable<CharacterSetHolder> characters)
        {
            var charStrings = characters.Where(c => c.Enabled)
                .Select(c => c.Characters);
            var value = string.Join(string.Empty, charStrings);

            return value;
        }
    }
}
