using RSG.Library.Constants;
using RSG.Library.Models;
using RSG.Library.Services;
using System.Collections.Generic;

namespace RSG.Library.Factories
{
    internal class CharacterSetServiceFactory
    {
        public CharacterSetService Create()
        {
            var service = new CharacterSetService()
            {
                CharacterSets = new Dictionary<string, CharacterSet>()
            };

            service.CharacterSets.Add(CharacterSetConstants.LOWERCASE, new CharacterSet(CharacterSetConstants.LOWERCASE_SET, true));
            service.CharacterSets.Add(CharacterSetConstants.UPPERCASE, new CharacterSet(CharacterSetConstants.UPPERCASE_SET, true));
            service.CharacterSets.Add(CharacterSetConstants.NUMBERS, new CharacterSet(CharacterSetConstants.NUMBERS_SET, true));
            service.CharacterSets.Add(CharacterSetConstants.PUNCTUATION, new CharacterSet(CharacterSetConstants.PUNCTUATION_SET, true));
            service.CharacterSets.Add(CharacterSetConstants.SPACE, new CharacterSet(CharacterSetConstants.SPACE_SET, true));
            service.CharacterSets.Add(CharacterSetConstants.SYMBOLS, new CharacterSet(CharacterSetConstants.SYMBOLS_SET, true));

            return service;
        }
    }
}
