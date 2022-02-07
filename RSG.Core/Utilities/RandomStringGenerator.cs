using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models.Results;
using RSG.Core.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private Lazy<char[]> characterList;

        public RandomStringGenerator()
        {
            characterList = new Lazy<char[]>();
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="stringRequest">The request holding information about how to generate strings.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="IStringResult"/> as a result.</returns>
        public IStringResult Generate(IStringRequest stringRequest, CancellationToken cancellationToken)
        {
            return GenerateInternal(stringRequest, cancellationToken);
        }

        private IStringResult GenerateInternal(IStringRequest stringRequest, CancellationToken cancellationToken)
        {
            characterList = new Lazy<char[]>(stringRequest.ToCharArray());

            var startTime = DateTime.Now;
            var strings = GenerateRandomStrings(stringRequest, cancellationToken);
            var endTime = DateTime.Now;

            var duration = endTime - startTime;

            var result = new StringResult(stringRequest, strings, duration);

            return result;
        }

        /// <summary>
        /// Generates <paramref name="numberOfIterations"/> of random string(s) of
        /// <paramref name="stringLength"/>.
        /// </summary>
        /// <param name="numberOfIterations">The number of strings to generate.</param>
        /// <param name="stringLength">The length of the string to generate.</param>
        /// <returns></returns>
        public async Task<IStringResult> GenerateAsync(IStringRequest stringRequest, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            var strings = await Task.Run(() => GenerateRandomStrings(stringRequest, cancellationToken));
            var endTime = DateTime.Now;

            var duration = endTime - startTime;

            var result = new StringResult(stringRequest, strings, duration);

            return result;
        }

        // TODO convert to multithreaded usage
        private IEnumerable<string> GenerateRandomStrings(IStringRequest stringRequest, CancellationToken cancellationToken)
        {
            var strings = new Queue<string>(1000);

            for (var bi = BigInteger.Zero; bi < stringRequest.Iterations; bi++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                strings.Enqueue(GenerateRandomString(stringRequest.StringLength, characterList.Value.Length));
            }

            return strings;
        }

        private string GenerateRandomString(int stringLength, int maxLength)
        {
            var stringBuilder = new StringBuilder();

            for (var i = BigInteger.Zero; i < stringLength; i++)
            {
                stringBuilder.Append(characterList.Value[RandomProvider.Random.Next(maxLength)]);
            }

            return stringBuilder.ToString();
        }
    }
}
