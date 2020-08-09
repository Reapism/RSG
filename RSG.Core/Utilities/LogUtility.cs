using System;
using System.Collections.Generic;
using System.Threading;

namespace RSG.Core.Utilities
{
    public enum LogType
    {
        Info,
        Warning,
        Failure,
    }
    public static class LogUtility
    {
        public static Dictionary<int, (LogType, string, string, Exception)> Logs { get; set; }

        private static int Index = 0;

        private static object LockObject = new object();

        static LogUtility()
        {
            Logs = new Dictionary<int, (LogType, string, string, Exception)>();
        }

        /// <summary>
        /// Writes a <paramref name="value"/> to the internal
        /// log dictionary.
        /// </summary>
        /// <param name="value">Title, description.</param>
        public static void Write(string title, string value, Exception ex)
        {
            lock (LockObject)
            {
                var logType = LogType.Failure;
                if (ex is null)
                {
                    logType = LogType.Warning;
                }

                Interlocked.Increment(ref Index);
                Logs.Add(Index, (logType, title, value, ex));
            }
        }
    }
}
