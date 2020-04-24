using System;
using System.Collections.Generic;
using System.Linq;
using RSG.Core.Interfaces;

namespace RSG.Core.Services
{
    public class WordListService : IWordList
    {
        public WordListService()
        {
            WordLists = new SortedDictionary<string, IEnumerable<string>>();
        }

        /// <summary>
        /// Gets or sets a value that represents loaded word lists.
        /// <para>Represents the loaded word lists. Use <see cref="Load(string)"/>
        /// to load a wordlists.</para>
        /// </summary>
        public IDictionary<string, IEnumerable<string>> WordLists { get; set; }

        /// <summary>
        /// Given the name of a wordlist, returns the wordlist.
        /// </summary>
        /// <param name="wordListName">The name of the wordlist to get.</param>
        /// <returns>The wordlist if found.</returns>
        public IEnumerable<string> this[string wordListName] => WordLists.FirstOrDefault(keyValue => keyValue.Key == wordListName).Value;

        public void Load(string wordListName)
        {
            if (!this[wordListName].Any())
                throw new ArgumentException($"The wordlist {wordListName} was not found!");

            
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }
    }
}
