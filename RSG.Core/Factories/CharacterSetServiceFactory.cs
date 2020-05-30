using RSG.Core.Constants;
using RSG.Core.Models;
using RSG.Core.Services;
using System.Collections.Generic;

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
                CharacterSets = new Dictionary<string, CharSet>(),
            };

            service.CharacterSets.Add(CharacterSetConstants.Lowercase, new CharSet(CharacterSetConstants.LowercaseSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Uppercase, new CharSet(CharacterSetConstants.UppercaseSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Numbers, new CharSet(CharacterSetConstants.NumbersSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Punctuation, new CharSet(CharacterSetConstants.PunctuationSet, false));
            service.CharacterSets.Add(CharacterSetConstants.Space, new CharSet(CharacterSetConstants.SpaceSet, false));
            service.CharacterSets.Add(CharacterSetConstants.Symbols, new CharSet(CharacterSetConstants.SymbolsSet, false));

            return service;
        }
    }
}
