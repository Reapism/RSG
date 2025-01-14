using RSG.Services;
using Sweaj.Patterns.Dates;
using System.Collections.Concurrent;
using System.Numerics;

namespace RSG.Strings
{
    public class RandomStringGenerator(IRandomProvider<int> randomProvider, IDateTimeProvider dateTimeProvider) : IGenerator<StringRequest, StringResult>
    {
        private ConcurrentBag<string> bag;

        public async Task<StringResult> GenerateAsync(StringRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var startTime = dateTimeProvider.Now();
            var isCancelled = false;

            try
            {
                bag = [];
                // Use Task.Run to ensure that the heavy generation work
                // can be scheduled on a thread pool thread.
                await Task.Run(() => GenerateRandomStrings(request, cancellationToken), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                isCancelled = true;
            }

            var endTime = dateTimeProvider.Now();
            var duration = endTime - startTime;

            var isCompletedSuccessfully = !isCancelled && request.Iterations == bag.Count;
            var operationStatus = new OperationStatus(isCompletedSuccessfully, isCancelled, duration);

            var stats = GenerateStats(request, bag);

            return new StringResult(request, operationStatus, bag, stats);
        }

        private StringStatistics? GenerateStats(StringRequest request, ConcurrentBag<string> strings)
        {
            if (!request.GenerateStatistics)
            {
                return null;
            }

            var permutations = CalculatePermutations(request);
            return new StringStatistics(permutations, GetMostAndLeastFrequentCharacter(strings));
        }

        private static BigInteger CalculatePermutations(StringRequest request)
        {
            return BigInteger.Pow(request.CharacterList.Length, request.StringLength);
        }

        private CharacterFrequency[] GetMostAndLeastFrequentCharacter(IEnumerable<string> strings)
        {
            var frequency = strings
                           .SelectMany(s => s)                  // Flatten into a sequence of characters
                           .GroupBy(ch => ch)                   // Group by character
                           .ToDictionary(g => g.Key, g => g.Count()); // Character frequencies

            // Start with sensible initial values.
            char mostFrequent = default;
            char leastFrequent = default;
            var maxCount = int.MinValue;
            var minCount = int.MaxValue;

            foreach (var kvp in frequency)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostFrequent = kvp.Key;
                }
                if (kvp.Value < minCount)
                {
                    minCount = kvp.Value;
                    leastFrequent = kvp.Key;
                }
            }

            return new[]
            {
                new CharacterFrequency(mostFrequent, maxCount, "Most occurring character"),
                new CharacterFrequency(leastFrequent, minCount, "Least occurring character")
            };
        }

        private void GenerateRandomStrings(StringRequest request, CancellationToken cancellationToken)
        {
            bag = new ConcurrentBag<string>();

            // Pre-compute the flat list of characters.
            var characterList = request.CharacterList.ToCharArray();
            var charListLength = characterList.Length;

            // If multithreading options are provided, use Parallel.For.
            if (request.MultiThreadOptions != null)
            {
                var options = request.MultiThreadOptions;
                var parallelOptions = new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = options.MaxThreads ?? Environment.ProcessorCount
                };

                // Partition the iterations using Parallel.For:
                Parallel.For(0, request.Iterations, parallelOptions, i =>
                {
                    // Each iteration generates one random string.
                    var s = GenerateRandomString(request.StringLength, characterList, charListLength, randomProvider);
                    bag.Add(s);
                });
            }
            else
            {
                // Fallback to sequential generation.
                for (var i = 0; i < request.Iterations; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    bag.Add(GenerateRandomString(request.StringLength, characterList, charListLength, randomProvider));
                }
            }
        }

        /// <summary>
        /// Generates a random string in a fast, allocation-aware manner.
        /// </summary>
        /// <param name="stringLength">Length of the string to generate.</param>
        /// <param name="characterList">Pre-cached array of characters to choose from.</param>
        /// <param name="charListLength">The length of the characterList (cached to avoid repeated property lookups).</param>
        /// <param name="randomProvider">A random provider abstraction.</param>
        /// <returns>A random string of the specified length.</returns>
        private static string GenerateRandomString(int stringLength, char[] characterList, int charListLength, IRandomProvider<int> randomProvider)
        {
            // For short strings, use stackalloc to reduce heap allocations.
            Span<char> result = stringLength <= 256
                ? stackalloc char[stringLength]
                : new char[stringLength];

            for (var i = 0; i < stringLength; i++)
            {
                var randomIndex = randomProvider.Next(charListLength);
                result[i] = characterList[randomIndex];
            }
            return new string(result);
        }
    }
}
