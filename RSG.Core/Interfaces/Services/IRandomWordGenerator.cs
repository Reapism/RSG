using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    public interface IRandomWordGenerator
    {
        void GenerateWords(IDictionaryRequest dictionaryRequest, CancellationToken cancellationToken);

        Task GenerateWordsAsync(IDictionaryRequest dictionaryRequest, CancellationToken cancellationToken);
    }
}
