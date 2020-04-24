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

            service.CharacterSets.Add(CharacterSetConstants.Lowercase, new CharacterSet(CharacterSetConstants.LowercaseSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Uppercase, new CharacterSet(CharacterSetConstants.UppercaseSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Numbers, new CharacterSet(CharacterSetConstants.NumbersSet, true));
            service.CharacterSets.Add(CharacterSetConstants.Punctuation, new CharacterSet(CharacterSetConstants.PunctuationSet, false));
            service.CharacterSets.Add(CharacterSetConstants.Space, new CharacterSet(CharacterSetConstants.SpaceSet, false));
            service.CharacterSets.Add(CharacterSetConstants.Symbols, new CharacterSet(CharacterSetConstants.SymbolsSet, false));

            return service;
        }
    }
}
