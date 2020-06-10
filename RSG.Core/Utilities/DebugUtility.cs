using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RSG.Core.Utilities
{
    public static class DebugUtility
    {
        public static Dictionary<int, (string, string)> debugKvp;
        private static int index = 0;
        private static object locked = new object();

        static DebugUtility()
        {
            debugKvp = new Dictionary<int, (string, string)>();
        }

        public static void Write((string, string) value)
        {
            lock (locked)
            {
                Interlocked.Increment(ref index);
                debugKvp.Add(index, value);
            }
        }
    }
}
