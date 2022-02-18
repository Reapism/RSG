using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSG.Core.Services
{
    /// <summary>
    /// Contract providing a means for creating word lists
    /// using a <see cref="IRsgDictionary"/>.
    /// </summary>
    public interface IWordListCreator
    {
        Task<IDictionary<int, string>> CreateWordListAsync(IRsgDictionary dictionary, CancellationToken cancellationToken);
    }
}