using System.Collections.Generic;
using RSG.Core.Constants;
using RSG.Core.Models;
using RSG.Core.Services;

namespace RSG.Core.Factories
{
    /// <summary>
    /// Responsible for creating <see cref="CharacterSetService"/> instances.
    /// </summary>
    public class CharacterSetServiceFactory
    {
        /// <summary>
        /// Creates a default <see cref="CharacterSetService"/> instance.
        /// </summary>
        /// <returns>An instance of <see cref="CharacterSetService"/>.</returns>
        public CharacterSetService Create()
        {
            var service = new CharacterSetService()
            {
                CharacterSets = new Dictionary<string, CharacterSet>(),
            };

            service.CharacterSets.Add(CharacterSetNameConstants.LOWERCASE, new CharacterSet(CharacterSetConstants.LOWERCASE, true));
            service.CharacterSets.Add(CharacterSetNameConstants.UPPERCASE, new CharacterSet(CharacterSetConstants.UPPERCASE, true));
            service.CharacterSets.Add(CharacterSetNameConstants.NUMBERS, new CharacterSet(CharacterSetConstants.NUMBERS, true));
            service.CharacterSets.Add(CharacterSetNameConstants.PUNCTUATION, new CharacterSet(CharacterSetConstants.PUNCTUATION, false));
            service.CharacterSets.Add(CharacterSetNameConstants.SPACE, new CharacterSet(CharacterSetConstants.SPACE, false));
            service.CharacterSets.Add(CharacterSetNameConstants.SYMBOLS, new CharacterSet(CharacterSetConstants.SYMBOLS, false));

            return service;
        }
    }
}
