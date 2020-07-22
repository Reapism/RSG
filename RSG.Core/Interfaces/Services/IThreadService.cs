using System.Numerics;

namespace RSG.Core.Interfaces.Services
{
    public interface IThreadService
    {
        int GetThreadsCount(BigInteger numberOfIterations);
    }
}
