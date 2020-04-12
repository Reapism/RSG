using RSG.Core.Interfaces;
using System.Numerics;
using System.Text;

namespace RSG.Core.Utilities
{
    public class Statistics : IStatistics, ICharacterFrequency
    {
        /// <inheritdoc/>
        public BigInteger StringLength { get; set; }
        /// <inheritdoc/>
        public BigInteger Iterations { get; set; }
        /// <inheritdoc/>
        public BigInteger Permutations { get; set; }
        /// <inheritdoc/>
        public string RandomizationType { get; set; }
        /// <inheritdoc/>
        public string CharacterList { get; set; }
        /// <inheritdoc/>
        public BigInteger MostFrequentCharacterNumber { get; set; }
        /// <inheritdoc/>
        public BigInteger LeastFrequentCharacterNumber { get; set; }
        /// <inheritdoc/>
        public char MostFrequentCharacter { get; set; }
        /// <inheritdoc/>
        public char LeastFrequentCharacter { get; set; }

        /// <summary>
        /// Return a string representation of the <see cref="Statistics"/> instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine($"String Length: {StringLength.ToString("n0")}")
                .AppendLine($"Iteration(s): {Iterations.ToString("n0")}")
                .AppendLine($"Character List: {CharacterList}")
                .AppendLine($"Least Frequent Character: {LeastFrequentCharacter} ({LeastFrequentCharacterNumber.ToString("n0")})")
                .AppendLine($"Most Frequent Character: {MostFrequentCharacter} ({MostFrequentCharacterNumber.ToString("n0")})")
                .AppendLine($"String Length: {LeastFrequentCharacter}")
                .AppendLine($"String Length: {LeastFrequentCharacterNumber}")
                .AppendLine($"Randomization Type: {RandomizationType}")
                .AppendLine($"Permutations: {Permutations.ToString("n0")}")
                .AppendLine($"Probability: 1/({Permutations.ToString("n0")})");

            return stringBuilder.ToString();
        }
    }
}
