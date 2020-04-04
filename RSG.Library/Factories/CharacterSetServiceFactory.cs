using RSG.Library.Models;
using RSG.Library.Services;
using System;
using System.Collections.Concurrent;

namespace RSG.Library.Factories
{
    internal class CharacterSetServiceFactory
    {
        public CharacterSetService Create()
        {
            var service = new CharacterSetService()
            {
                CharacterSets = new ConcurrentDictionary<string, CharacterSet>()
            };

            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Lowercase", "abcdefghijklmnopqrstuvwxyz", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Uppercase", "ABCDEFGHIJKLMNAOPQRSTUVWXYZ", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Numbers", "0123456789", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Punctuation", "!,.?", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Space", " ", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Symbols", " !@#$%^&*()-=_+,./;\'\\<>?:\"[]{}|`~", true));
            service.CharacterSets.TryAdd(Guid.NewGuid().ToString(), new CharacterSet("Custom", "", true));

            return service;
        }
    }
}
