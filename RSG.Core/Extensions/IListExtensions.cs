using RSG.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RSG.Core.Extensions
{
    internal static class IListExtensions
    {
        /// <summary>
        /// Converts enabled character sets into a single <see cref="char"/>[].
        /// </summary>
        /// <param name="characters">The character sets.</param>
        /// <returns>A <see cref="char"/>[].</returns>
        internal static char[] ToCharArray(this IList<CharacterSet> characters)
        {
            var charStrings = characters.Where(c => c.Enabled)
                .Select(c => c.Characters);
            var charArray = string.Join(string.Empty, charStrings).ToCharArray();

            return charArray;
        }
    }
}
