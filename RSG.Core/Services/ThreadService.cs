using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RSG.Core.Services
{
    public class ThreadService
    {
        public IEnumerable<Thread> GetThreadsFor<T>()
            where T : class, IResult
        {
            var threads = new Queue<Thread>();

            return T;
        }
    }
}
