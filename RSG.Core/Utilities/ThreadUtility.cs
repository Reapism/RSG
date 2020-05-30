using RSG.Core.Interfaces.Services;
using System;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Contains information about threads on the executing
    /// environment.
    /// </summary>
    public class ThreadUtility : IThreadService
    {
        public int GetThreadsCount()
        {
            return Environment.ProcessorCount;
        }
    }
}
