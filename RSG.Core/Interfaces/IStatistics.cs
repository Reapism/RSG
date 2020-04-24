using System.Numerics;

namespace RSG.Core.Interfaces
{
    internal interface IStatistics
    {
        BigInteger StringLength { get; set; }
        BigInteger Iterations { get; set; }
        BigInteger Permutations { get; set; }

        string RandomizationType { get; set; }

        string CharacterList { get; set; }
    }
}
