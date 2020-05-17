using RSG.Core.Extensions;
using RSG.Core.Models;
using RSG.Core.Services;
using System.Linq;
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
            RsgDictionary dictionary = new RsgDictionary()
            {
                Name = name,
                Description = desc,
                Source = source,
                IsSourceLocal = isSourceLocal
            };

            dictionary.WordList = await WordListService.CreateWordList(dictionary);
            dictionary.Count = dictionary.WordList.Count().ToBigInteger();

            return dictionary;
        }
    }
}
