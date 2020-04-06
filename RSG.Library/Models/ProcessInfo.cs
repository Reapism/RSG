using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace RSG.Library.Models
{
    internal class ProcessInfo
    {
        [DisplayName("Thread Count")]
        public int ThreadCount { get; set; }
        public string Priority { get; set; }
        public string Affinity { get; set; }
        [DisplayName("Working Set")]
        public string WorkingSet { get; set; }
        [DisplayName("Running Time")]
        public string RunningTime { get; set; }
        public IEnumerable<ProcessReference> References { get; set; }
        public IEnumerable<ThreadReference> Threads { get; set; }

        public ProcessInfo(Process process)
        {
            ThreadCount = process.Threads.Count;
            Threads = GetThreads(process);
            Priority = process.PriorityClass.ToString();
            Affinity = process.ProcessorAffinity.ToString("n0");
            WorkingSet = process.WorkingSet64.ToString("n0");
            RunningTime = new DateTime(process.TotalProcessorTime.Ticks).ToLongTimeString();
            References = GetReferences(process);
        }

        public IEnumerable<ProcessReference> GetReferences(in Process process)
        {
            var processModules = process.Modules;
            var queue = new Queue<ProcessReference>();

            for (var i = 0; i < processModules.Count; i++)
            {
                var module = processModules[i];
                var processReference = new ProcessReference()
                {
                    Name = module.ModuleName,
                    RequiredMemoryForStartup = module.ModuleMemorySize
                };

                queue.Enqueue(processReference);
            }

            return queue;
        }

        public IEnumerable<ThreadReference> GetThreads(in Process process)
        {
            var threads = process.Threads;
            var queue = new Queue<ThreadReference>();

            for (var i = 0; i < threads.Count; i++)
            {
                var module = threads[i];
                var processReference = new ThreadReference(module);

                queue.Enqueue(processReference);
            }

            return queue;
        }
        public static Process GetCurrentProcess { get => Process.GetCurrentProcess(); }
    }
}
