using System;
using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Utilities
{
    public static class LogUtility
    {
        public static Dictionary<int, (string, string, Exception)> DebugByIndex { get; set; }

        private static int Index = 0;

        private static object LockObject = new object();

        static LogUtility()
        {
            DebugByIndex = new Dictionary<int, (string, string, Exception)>();
        }

        /// <summary>
        /// Writes a <paramref name="value"/>to the internal
        /// log dictionary.
        /// </summary>
        /// <param name="value">Title, description.</param>
        public static void Write(string title, string value, Exception ex)
        {
            lock (LockObject)
            {
                Interlocked.Increment(ref Index);
                DebugByIndex.Add(Index, (title, value, ex));
            }
        }
    }
}
