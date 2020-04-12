using RSG.Core.Services;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSG.Core.Utilities
{
    public class RandomStringGenerator
    {
        private CharacterSetService _characterSetService;
        private char[] _characterList;

        public RandomStringGenerator(CharacterSetService characterSetService)
        {
            _characterSetService = characterSetService;
            _characterList = _characterSetService.GetNewCharacterList();
            var test = new string(_characterList);
        }

        public IEnumerable<string> GenerateRandomStrings(int numberOfIterations, int length)
        {
            return GenerateRandomStrings(BigInteger.Parse(numberOfIterations.ToString()), BigInteger.Parse(length.ToString()));
        }

        public IEnumerable<string> GenerateRandomStrings(BigInteger numberOfIterations, BigInteger length)
        {
            var queue = new Queue<string>();

            for (BigInteger bi = 0; bi < numberOfIterations; bi++)
            {
                queue.Enqueue(GenerateRandomString(length));
            }

            return queue;
        }

        private string GenerateRandomString(BigInteger length)
        {
            var stringBuilder = new StringBuilder();
            var maxLength = _characterList.Length;

            for (var bi = BigInteger.Zero; bi < length; bi++)
            {
                stringBuilder.Append(_characterList[RandomProvider.Random.Next(maxLength)]);
            }

            return stringBuilder.ToString();
        }
    }
}
