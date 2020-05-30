using RSG.Core.Interfaces;
using RSG.Core.Models;
using RSG.Core.Services;
using System;
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
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The string length to generate.</param>
        /// <returns></returns>
        public IStringResult GenerateRandomStringsResult(int numberOfIterations, int stringLength)
        {
            return GenerateRandomStringsResult(BigInteger.Parse(numberOfIterations.ToString()), BigInteger.Parse(stringLength.ToString()));
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The length of the string to generate.</param>
        /// <returns></returns>
        public IStringResult GenerateRandomStringsResult(in BigInteger numberOfIterations, in BigInteger stringLength)
        {
            DateTime startTime = DateTime.Now;
            IEnumerable<string> strings = GenerateRandomStrings(numberOfIterations, stringLength);
            DateTime endTime = DateTime.Now;

            var result = new StringResult()
            {
                Characters = _characterList.ToString() ?? string.Empty,
                StringLength = stringLength,
                Iterations = numberOfIterations,
                RandomizationType = RandomProvider.SelectedRandomizationType,
                Strings = strings,
                StartTime = startTime,
                EndTime = endTime
            };

            return result;
        }

        private IEnumerable<string> GenerateRandomStrings(in BigInteger numberOfIterations, in BigInteger stringLength)
        {
            var strings = new Queue<string>(1000);

            for (BigInteger bi = BigInteger.Zero; bi < numberOfIterations; bi++)
            {
                strings.Enqueue(GenerateRandomString(stringLength));
            }

            return strings;
        }

        private string GenerateRandomString(BigInteger length)
        {
            var stringBuilder = new StringBuilder();
            var maxLength = _characterList.Length;

            for (BigInteger bi = BigInteger.Zero; bi < length; bi++)
            {
                stringBuilder.Append(_characterList[RandomProvider.Random.Next(maxLength)]);
            }

            return stringBuilder.ToString();
        }
    }
}
