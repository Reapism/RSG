using RSG.Core.Interfaces.Services;
using RSG.Core.Utilities;

namespace RSG.Core.Services
{
    public class StringThreadService : IThreadService
    {
        private readonly ThreadUtility threadUtility;

        public StringThreadService(ThreadUtility threadUtility)
        {
            this.threadUtility = threadUtility;
        }

        public int GetThreadsCount()
        {
            var threads = threadUtility.GetThreadsCount();
            return threads;
        }
    }
}
