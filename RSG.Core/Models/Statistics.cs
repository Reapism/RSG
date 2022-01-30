using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
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
        public IEnumerable<int> MostFrequentCharacterCount { get; set; }
        /// <inheritdoc/>
        public IEnumerable<int> LeastFrequentCharacterCount { get; set; }
        /// <inheritdoc/>
        public IEnumerable<char> MostFrequentCharacter { get; set; }
        /// <inheritdoc/>
        public IEnumerable<char> LeastFrequentCharacter { get; set; }

        public IStringRequest StringRequest => throw new System.NotImplementedException();

        public IDictionaryRequest DictionaryRequest => throw new System.NotImplementedException();

        public IStringResult StringResult => throw new System.NotImplementedException();

        public IDictionaryResult DictionaryResult => throw new System.NotImplementedException();

        public ICharacterFrequency CharacterFrequency => throw new System.NotImplementedException();
    }
}
