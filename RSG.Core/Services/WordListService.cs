using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contains methods for generating wordlists from an
    /// <see cref="IRsgDictionary"/>.
    /// </summary>
    public static class WordListService
    {
        /// <summary>
        /// Creates a wordlist from an <see cref="IRsgDictionary"/>.
        /// <para>Returns an empty sequence if unable to read/download dictionary from
        /// the source.</para>
        /// </summary>
        /// <param name="dictionary">A contract representing a
        /// <see cref="IRsgDictionary"/>.</param>
        /// <returns>A new <see cref="IEnumerable{string}"/> containing the new word list.</returns>
        public static async Task<IEnumerable<string>> CreateWordList(IRsgDictionary dictionary)
        {
            var wordList = dictionary.IsSourceLocal
                ? await CreateWordListFromFile(dictionary.Source)
                : await CreateWordListFromHttp(dictionary.Source);

            return wordList;
        }

        private static async Task<IEnumerable<string>> CreateWordListFromFile(string source)
        {
            try
            {
                var wordList = await IOUtility.ReadLinesASync(source);
                return wordList;
            }
            catch
            {
                return new string[] { };
            }
        }

        private static async Task<IEnumerable<string>> CreateWordListFromHttp(string source)
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
