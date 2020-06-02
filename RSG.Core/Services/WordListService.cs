using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contains methods for generating wordlists from an
    /// <see cref="IRsgDictionary"/>.
    /// </summary>
    public class WordListService
    {
        public IRsgDictionary rsgDictionary;

        public WordListService(IRsgDictionary dictionary)
        {
            rsgDictionary = dictionary;
        }

        /// <summary>
        /// Creates a key/value pair wordlist which, maps indexes to words.
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public IDictionary<int, string> CreateIndexedWordList(IEnumerable<string> words)
        {
            var wordCount = words.Count();
            var dictionary = new Dictionary<int, string>(wordCount);
            var index = 0;

            foreach(var word in words)
            {
                dictionary.Add(index, word);
                index++;
            }

            return dictionary;
        }

        /// <summary>
        /// Creates a sequence of words from an <see cref="IRsgDictionary"/>.
        /// <para>Returns an empty sequence if unable to read/download dictionary from
        /// the source.</para>
        /// </summary>
        /// <param name="dictionary">A contract representing a
        /// <see cref="IRsgDictionary"/>.</param>
        /// <returns>A new <see cref="IEnumerable{string}"/> containing the new word list.</returns>
        public async Task<IEnumerable<string>> CreateWordList(IRsgDictionary dictionary)
        {
            IEnumerable<string> wordList = dictionary.IsSourceLocal
                ? await CreateWordListFromFile(dictionary.Source)
                : await CreateWordListFromHttp(dictionary.Source);

            return wordList;
        }

        private async Task<IEnumerable<string>> CreateWordListFromFile(string source)
        {
            try
            {
                IEnumerable<string> wordList = await IOUtility.ReadLinesASync(source);
                return wordList;
            }
            catch
            {
                return new string[] { };
            }
        }

        private async Task<IEnumerable<string>> CreateWordListFromHttp(string source)
        {
            try
            {
                var resource = await DownloadUtility.DownloadFileAsString(source);
                var wordList = resource.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                return wordList;
            }
            catch
            {
                return new string[] { };
            }
        }
    }
}
