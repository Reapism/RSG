using RSG.Library.Enums;
using System.Numerics;
using System.Text;

namespace RSG.Library
{
    public class SimpleStatistics
    {
        public BigInteger StringLength { get; set; }
        public BigInteger Iterations { get; set; }
        public BigInteger MostFrequentCharacterNumber { get; set; }
        public BigInteger LeastFrequentCharacterNumber { get; set; }
        public BigInteger Permutations { get; set; }
        public char MostFrequentCharacter { get; set; }
        public char LeastFrequentCharacter { get; set; }
        public string CharacterList { get; set; }
        public RandomizationType Randomization { get; set; }

        /// <summary>
        /// Return a string representation of the <see cref="SimpleStatistics"/> instance.
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
                .AppendLine($"Randomization Type: {Randomization.ToString()}")
                .AppendLine($"Permutations: {Permutations.ToString("n0")}")
                .AppendLine($"Probability: 1/({Permutations.ToString("n0")})");

            return stringBuilder.ToString();
        }
    }
}
