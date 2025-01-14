using RSG.Extensions;
using RSG.Strings;
using RSG.Words;
using System.Numerics;

namespace RSG.Services
{
    public class StatisticsService
    {
        public static BigInteger Permutations(BigInteger stringLength, int characterLength)
        {
            return BigInteger.Pow(stringLength, characterLength);
        }

        public static BigInteger Permutations(StringRequest stringRequest)
        {
            // stringLength ^ characterLength
            var stringLength = stringRequest.StringLength;
            var characterLength = stringRequest.CharacterList.Length;

            return BigInteger.Pow(stringLength, characterLength);
        }

        public static BigInteger Permutations(WordRequest wordRequest, IRsgContext context)
        {
            // wordCount ^ iterations

            var allWords = context.Dictionaries.Where(e => wordRequest.WordListRequests.Contains(e.ToInternalWordListRequest())).Select(e => e.WordList);
            var wordCount = allWords.Sum(e => e.Words.Count);
            var iterations = wordRequest.Iterations;

            return BigInteger.Pow(wordCount, iterations);
        }
    }
}
