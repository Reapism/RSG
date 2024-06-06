using RSG.Extensions;
using System.Collections.Concurrent;
using System.Numerics;
using System.Text;
using System.Xml.Xsl;

namespace RSG
{
    public class RandomStringGenerator : IGenerator<StringRequest, StringResult>
    {
        private readonly IRandomProvider<int> randomProvider;
        private ConcurrentBag<string> bag;
        public RandomStringGenerator(IRandomProvider<int> randomProvider)
        {
            this.randomProvider = randomProvider;
        }

        public async Task<StringResult> GenerateAsync(StringRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var startTime = DateTime.Now;
            var isCancelled = false;

            try
            {
                bag = new ConcurrentBag<string>();
                await Task.Run(() => GenerateRandomStrings(request, cancellationToken), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                isCancelled = true;
            }

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            return new StringResult(request, bag, duration)
            {
                IsCancelled = isCancelled,
                IsCompletedSuccessfully = !isCancelled && request.Iterations == bag.Count
            };
        }

        private void GenerateRandomStrings(StringRequest request, CancellationToken cancellationToken)
        {
            bag = [];
            for (var i = 0; i < request.Iterations; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                bag.Add(GenerateRandomString(request.StringLength, request.CharacterSets.ToCharArray(), randomProvider));
            }
        }

        private static string GenerateRandomString(in int stringLength, in char[] characterList, IRandomProvider<int> randomProvider)
        {
            var stringBuilder = new StringBuilder(stringLength);
            for (var i = 0; i < stringLength; i++)
            {
                stringBuilder.Append(characterList[randomProvider.Next(characterList.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}
