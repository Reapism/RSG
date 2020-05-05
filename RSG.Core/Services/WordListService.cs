using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contains methods for generating wordlists from an
    /// <see cref="IRsgDictionary"/>.
    /// </summary>
    public class WordListService
    {
        /// <summary>
        /// Creates a wordlist from an <see cref="IRsgDictionary"/>.
        /// </summary>
        /// <param name="dictionary">A contract of type 
        /// <see cref="IRsgDictionary"/>.</param>
        /// <returns>A new <see cref="IWordList"/></returns>
        public async Task<IWordList> CreateWordList(IRsgDictionary dictionary)
        {
            var wordList = dictionary.IsSourceLocal
                ? await CreateWordListFromFile(dictionary.Source)
                : await CreateWordListFromHttp(dictionary.Source);

            return wordList;
        }

        private async Task<IWordList> CreateWordListFromFile(string source)
        {
            using var fileStream = new FileStream(source, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(source);

            var wordList = new WordList();
            var words = await streamReader.ReadToEndAsync();

            wordList.Words = words.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return wordList;
        }

        private async Task<IWordList> CreateWordListFromHttp(string source)
        {
            var resource = await DownloadUtility.DownloadFileAsString(source);
            var wordList = new WordList
            {
                Words = resource.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            };

            return wordList;
        }
    }
}
