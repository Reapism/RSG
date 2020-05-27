using System;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Contains information about threads on the executing
    /// environment.
    /// </summary>
    public class ThreadUtility
    {
        public ThreadUtility()
        {
            Threads = GetThreads();
        }

        public int Threads { get; }

        private int GetThreads()
        {
            return Environment.ProcessorCount;
        }
    }
}
