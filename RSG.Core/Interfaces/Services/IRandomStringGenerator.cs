using RSG.Core.Interfaces.Result;
using System.Numerics;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomStringGenerator
    {
        IStringResult Generate(int numberOfIterations, int stringLength);

        IStringResult Generate(BigInteger numberOfIterations, BigInteger stringLength);

        Task<IStringResult> GenerateAsync(int numberOfIterations, int stringLength);

        Task<IStringResult> GenerateAsync(BigInteger numberOfIterations, BigInteger stringLength);
    }
}