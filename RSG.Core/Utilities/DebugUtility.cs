using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Utilities
{
    public static class DebugUtility
    {
        public static Dictionary<int, (string, string)> DebugByIndex { get; set; }

        private static int Index = 0;

        private static object locked = new object();

        static DebugUtility()
        {
            DebugByIndex = new Dictionary<int, (string, string)>();
        }

        public static void Write((string, string) value)
        {
            lock (locked)
            {
                Interlocked.Increment(ref Index);
                DebugByIndex.Add(Index, value);
            }
        }
    }
}
