using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Models.Result;
using RSG.Core.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private readonly ICharacterSetService characterSetService;
        private char[] characterList;

        public RandomStringGenerator(ICharacterSetService characterSetService)
        {
            this.characterSetService = characterSetService;
            characterList = this.characterSetService.CharacterList;
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The string length to generate.</param>
        /// <returns></returns>
        public IStringResult Generate(int numberOfIterations, int stringLength)
        {
            return Generate(BigInteger.Parse(numberOfIterations.ToString()), BigInteger.Parse(stringLength.ToString()));
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The string length to generate.</param>
        /// <returns></returns>
        public async Task<IStringResult> GenerateAsync(int numberOfIterations, int stringLength)
        {
            return await GenerateAsync(BigInteger.Parse(numberOfIterations.ToString()), BigInteger.Parse(stringLength.ToString()));
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The length of the string to generate.</param>
        /// <returns></returns>
        public IStringResult Generate(BigInteger numberOfIterations, BigInteger stringLength)
        {
            var startTime = DateTime.Now;
            var strings = GenerateRandomStrings(numberOfIterations, stringLength);
            var endTime = DateTime.Now;

            var result = new StringResult()
            {
                Characters = characterList.ToString() ?? string.Empty,
                StringLength = stringLength,
                Iterations = numberOfIterations,
                RandomizationType = RandomProvider.SelectedRandomizationType,
                Strings = strings,
                StartTime = startTime,
                EndTime = endTime
            };

            return result;
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The length of the string to generate.</param>
        /// <returns></returns>
        public async Task<IStringResult> GenerateAsync(BigInteger numberOfIterations, BigInteger stringLength)
        {
            var startTime = DateTime.Now;
            var strings = await Task.Run(() => GenerateRandomStrings(numberOfIterations, stringLength));
            var endTime = DateTime.Now;

            var result = new StringResult()
            {
                Characters = characterList.ToString() ?? string.Empty,
                StringLength = stringLength,
                Iterations = numberOfIterations,
                RandomizationType = RandomProvider.SelectedRandomizationType,
                Strings = strings,
                StartTime = startTime,
                EndTime = endTime
            };

            return result;
        }

        private IEnumerable<string> GenerateRandomStrings(BigInteger numberOfIterations, BigInteger stringLength)
        {
            var strings = new Queue<string>(1000);

            for (var bi = BigInteger.Zero; bi < numberOfIterations; bi++)
            {
                strings.Enqueue(GenerateRandomString(stringLength));
            }

            return strings;
        }

        private string GenerateRandomString(BigInteger length)
        {
            var stringBuilder = new StringBuilder();
            var maxLength = characterList.Length;

            for (var bi = BigInteger.Zero; bi < length; bi++)
            {
                stringBuilder.Append(characterList[RandomProvider.Random.Value.Next(maxLength)]);
            }

            return stringBuilder.ToString();
        }
    }
}
