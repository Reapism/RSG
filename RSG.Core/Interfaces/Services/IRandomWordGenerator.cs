using RSG.Core.Utilities;
using System.Numerics;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomWordGenerator
    {
        event Completed GenerateCompleted;
        event ProgressChanged GenerateChanged;

        Task GenerateAsync(BigInteger iterations);
    }
}