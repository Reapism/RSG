using RSG.Core.Interfaces;
using RSG.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSG.Core.Factories
{
    public static class StatisticsFactory
    {
        /// <summary>
        /// Creates a <see cref="DetailedStatistics"/> instance based on
        /// a <see cref="RsgResult"/> instance.
        /// </summary>
        /// <param name="result">A <see cref="RsgResult"/> instance used to
        /// construct the <see cref="DetailedStatistics"/> instance.</param>
        /// <returns>A <see cref="DetailedStatistics"/> instance.</returns>
        public static DetailedStatistics CreateDetailedStatistics(in RsgResult result)
        {
            var statistics = new DetailedStatistics()
            {
                CharacterList = result.CharacterList,
                Duration = result.EndTime - result.StartTime,
                Iterations = result.Iterations,
                RandomizationType = result.RandomizationType,
                StringLength = result.StringLength,
            };

            var durationModel = GetDurations(statistics.Duration.Ticks, result.Iterations);

            statistics.IterationsPerSecond = durationModel.IterationsPerSecond;
            statistics.IterationsPerMinute = durationModel.IterationsPerMinute;
            statistics.IterationsPerHour = durationModel.IterationsPerHour;
            statistics.IterationsPerDay = durationModel.IterationsPerDay;
            statistics.IterationsPerYear = durationModel.IterationsPerYear;
            statistics.IterationsPerCentury = durationModel.IterationsPerCentury;

            statistics.Permutations = GetPermutations(
                BigInteger.Parse(result.CharacterList.Length.ToString()),
                result.StringLength);

            var characterModel = GetCommonCharacters(result.Strings);

            statistics.LeastFrequentCharacter = characterModel.LeastFrequentCharacter;
            statistics.LeastFrequentCharacterCount = characterModel.LeastFrequentCharacterCount;
            statistics.MostFrequentCharacter = characterModel.MostFrequentCharacter;
            statistics.MostFrequentCharacterCount = characterModel.MostFrequentCharacterCount;

            return statistics;
        }

        /// <summary>
        /// Creates a <see cref="Statistics"/> instance based on
        /// a <see cref="RsgResult"/> instance.
        /// </summary>
        /// <param name="result">A <see cref="RsgResult"/> instance used to
        /// construct the <see cref="Statistics"/> instance.</param>
        /// <returns>A <see cref="Statistics"/> instance.</returns>
        public static Statistics CreateStatistics(in RsgResult result)
        {
            var statistics = new Statistics()
            {
                CharacterList = result.CharacterList,
                Iterations = result.Iterations,
                RandomizationType = result.RandomizationType,
                StringLength = result.StringLength,
            };

            statistics.Permutations = GetPermutations(
                BigInteger.Parse(result.CharacterList.Length.ToString()),
                result.StringLength);

            var characterModel = GetCommonCharacters(
                new List<string> { result.Strings.FirstOrDefault() });

            statistics.LeastFrequentCharacter = characterModel.LeastFrequentCharacter;
            statistics.LeastFrequentCharacterCount = characterModel.LeastFrequentCharacterCount;
            statistics.MostFrequentCharacter = characterModel.MostFrequentCharacter;
            statistics.MostFrequentCharacterCount = characterModel.MostFrequentCharacterCount;

            return statistics;
        }

        private static BigInteger GetPermutations(BigInteger value, BigInteger exponent)
        {
            if (!int.TryParse(exponent.ToString(), out var exp))
                return BigInteger.Zero;

            return BigInteger.Pow(value, exp);
        }

        private static IIterationsFrequency GetDurations(long currentTicks, BigInteger iterations)
        {
            var model = new IterationsFrequency();
            var timeSpan = new TimeSpan(currentTicks);

            model.IterationsPerSecond = timeSpan.Divide(TimeSpan.TicksPerSecond);
            model.IterationsPerMinute = timeSpan.Divide(TimeSpan.TicksPerMinute);
            model.IterationsPerHour = timeSpan.Divide(TimeSpan.TicksPerHour);
            model.IterationsPerDay = timeSpan.Divide(TimeSpan.TicksPerDay);
            model.IterationsPerYear = timeSpan.Divide(TimeSpan.TicksPerDay * 365D);
            model.IterationsPerCentury = timeSpan.Divide(TimeSpan.TicksPerDay * (365D * 100D));

            return model;
        }

        private static ICharacterFrequency GetCommonCharacters(IEnumerable<string> strings)
        {
            var count = strings.Count();

            var leastFreqCharQueue = new Queue<char>(count);
            var leastFreqCharCountQueue = new Queue<int>(count);
            var mostFreqCharQueue = new Queue<char>(count);
            var mostFreqCharCountQueue = new Queue<int>(count);

            foreach (var str in strings)
            {
                var leastFreqCharCount = str.Min(asciiCharMin => str.Count(count => count == asciiCharMin));
                var leastFreqChar = str.First(asciiCharMin => str.Count(count => count == asciiCharMin) == leastFreqCharCount);
                var mostFreqCharCount = str.Max(asciiCharMax => str.Count(count => count == asciiCharMax));
                var mostFreqChar = str.First(asciiCharMax => str.Count(count => count == asciiCharMax) == mostFreqCharCount);

                leastFreqCharCountQueue.Enqueue(leastFreqCharCount);
                leastFreqCharQueue.Enqueue(leastFreqChar);
                mostFreqCharCountQueue.Enqueue(mostFreqCharCount);
                mostFreqCharQueue.Enqueue(mostFreqChar);
            }

            var model = new CharacterFrequency()
            {
                LeastFrequentCharacter = leastFreqCharQueue,
                LeastFrequentCharacterCount = leastFreqCharCountQueue,
                MostFrequentCharacter = mostFreqCharQueue,
                MostFrequentCharacterCount = mostFreqCharCountQueue,
            };

            return model;
        }

        private static long GetTicksPerIteration(long currentTicks, BigInteger totalIterations)
        {
            if (!long.TryParse(totalIterations.ToString(), out var totalIters))
                return 0L;

            return currentTicks / totalIters;
        }
    }
}
