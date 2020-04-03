using System.Numerics;

namespace RSG.Library.Interfaces
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
