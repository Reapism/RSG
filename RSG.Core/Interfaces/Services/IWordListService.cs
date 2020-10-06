using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    public interface IWordListService
    {
        Task<IDictionary<int, string>> CreateAsync(IRsgDictionary dictionary);
    }
}