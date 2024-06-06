using RSG.Core.Interfaces;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contains methods for generating wordlists from an
    /// <see cref="IRsgDictionary"/>.
    /// </summary>
    public class WordListService : IWordListCreator
    {
        /// <summary>
        /// Creates a sequence of words from an <see cref="IRsgDictionary"/>.
        /// <para>Returns an empty sequence if unable to read/download dictionary from
        /// the source.</para>
        /// </summary>
        /// <param name="dictionary">A contract representing a <see cref="IRsgDictionary"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel this task.</param>
        /// <returns>A new <see cref="IEnumerable{string}"/> containing the new word list.</returns>
        public async Task<IDictionary<int, string>> CreateWordListAsync(IRsgDictionary dictionary, CancellationToken cancellationToken)
        {
            return await CreateAsyncInternal(dictionary, cancellationToken);
        }

        private async Task<IDictionary<int, string>> CreateAsyncInternal(IRsgDictionary dictionary, CancellationToken cancellationToken)
        {
            var wordDictionary = new Dictionary<int, string>();
            var wordList = dictionary.WordListOptions.IsSourceLocal
                ? await CreateWordListFromFile(dictionary, cancellationToken)
                : await CreateWordListFromHttp(dictionary, cancellationToken);

            var index = 0;
            wordDictionary = wordList.ToDictionary(e => { return index++; });

            return wordDictionary;
        }

        private async Task<IEnumerable<string>> CreateWordListFromFile(IRsgDictionary dictionary, CancellationToken cancellationToken)
        {
            try
            {
                var wordList = await IOUtility.ReadLinesAsync(dictionary.WordListOptions.Source, dictionary.WordListOptions.Delimiter);
                return wordList;
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        private async Task<IEnumerable<string>> CreateWordListFromHttp(IRsgDictionary dictionary, CancellationToken cancellationToken)
        {
            try
            {
                var wordListAsString = await DownloadUtility.DownloadFileAsStringAsync(dictionary.WordListOptions.Source, cancellationToken);
                var wordList = wordListAsString.Split(dictionary.WordListOptions.Delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                return wordList;
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
