using RSG.Core.Models;
using RSG.Core.Utilities;
using System;
using System.IO;

namespace RSG.Core.Services
{
    public class WordListService
    {
        public async IAsyncResult CreateWordList(RsgDictionary dictionary)
        {
            return dictionary.IsSourceLocal
                ? CreateWordListFromFile(dictionary)
                : CreateWordListFromHttp(dictionary);
        }

        private async bool CreateWordListFromFile(RsgDictionary dictionary)
        {
            using var fileStream = new FileStream(dictionary.Source, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(dictionary.Source);


        }

        private async bool CreateWordListFromHttp(RsgDictionary dictionary)
        {
            var resource = await DownloadUtility.DownloadFileAsString(dictionary.Source);
            var wordList = resource.Split(Environment.NewLine);
            dictionary.
        }
    }
}
