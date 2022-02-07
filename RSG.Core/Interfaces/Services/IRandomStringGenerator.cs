using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomStringGenerator
    {
        IStringResult Generate(IStringRequest stringRequest, CancellationToken cancellationToken);

        Task<IStringResult> GenerateAsync(IStringRequest stringRequest, CancellationToken cancellationToken);
    }
}