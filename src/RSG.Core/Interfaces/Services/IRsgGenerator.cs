using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRsgGenerator<TRequest, TResult>
    {
        Task<TResult> GenerateAsync(TRequest stringRequest, CancellationToken cancellationToken);
    }
}