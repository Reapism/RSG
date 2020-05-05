using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Factories
{
    public class RsgDictionaryFactory
    {
        /// <summary>
        /// Creates a <see cref="RsgDictionary"/> instance, and populates 
        /// the wordlist with the source.
        /// </summary>
        /// <param name="name">The unique name of the dictionary.</param>
        /// <param name="desc">The description of the dictionary.</param>
        /// <param name="source">The source of the word list local or not.</param>
        /// <param name="isSourceLocal">Specified whether the source is coming from a
        /// local filesystem or a direct url to be downloaded.</param>
        /// <returns>An <see cref="RsgDictionary"/> instance.</returns>
        public static async Task<RsgDictionary> Create(string name, string desc, string source, bool isSourceLocal)
        {
            var dictionary = new RsgDictionary()
            {
                Name = name,
                Description = desc,
                Source = source,
                IsSourceLocal = isSourceLocal,
                WordList = new WordList()
            };

            await SetWordList(dictionary);
            SetSize(dictionary.WordList);

            return dictionary;
        }

        private static async Task SetWordList(IRsgDictionary dictionary)
        {
            if (dictionary.IsSourceLocal)
            {
                dictionary.WordList.Words = await IOUtility.ReadLinesASync(dictionary.Source);
                return;
            }

            var wordsString = await DownloadUtility.DownloadFileAsString(dictionary.Source);
            dictionary.WordList.Words = wordsString.Split("\n");
            dictionary.Count = SetSize(dictionary.WordList);
        }

        private static BigInteger SetSize(IWordList wordList)
        {
            return BigInteger.Parse(wordList.Words.Count().ToString());
        }

    }
}
