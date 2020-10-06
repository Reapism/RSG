using System.Numerics;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomWordGenerator
    {
        Task GenerateAsync(BigInteger iterations);
    }
}