using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IStatistic<TResult>
        where TResult : class, IResult
    {
        TResult Result { get; }
    }
}
