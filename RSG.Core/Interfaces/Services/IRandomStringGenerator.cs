using RSG.Core.Interfaces.Result;
using System.Numerics;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomStringGenerator
    {
        IStringResult Generate(int numberOfIterations, int stringLength);

        IStringResult Generate(in BigInteger numberOfIterations, in BigInteger stringLength);
    }
}