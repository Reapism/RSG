﻿using RSG.Core.Interfaces;
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
    public class WordListService : IWordListService
    {
        /// <summary>
        /// Creates a sequence of words from an <see cref="IRsgDictionary"/>.
        /// <para>Returns an empty sequence if unable to read/download dictionary from
        /// the source.</para>
        /// </summary>
        /// <param name="dictionary">A contract representing a
        /// <see cref="IRsgDictionary"/>.</param>
        /// <returns>A new <see cref="IEnumerable{string}"/> containing the new word list.</returns>
        public async Task<IDictionary<int, string>> CreateAsync(IRsgDictionary dictionary)
        {
            var wordDictionary = new Dictionary<int, string>();
            var wordList = dictionary.IsSourceLocal
                ? await CreateWordListFromFile(dictionary.Source)
                : await CreateWordListFromHttp(dictionary.Source);

            var index = 0;
            wordDictionary = wordList.ToDictionary(e => { return index++; });

            return wordDictionary;
        }

        private async Task<IEnumerable<string>> CreateWordListFromFile(string source)
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
