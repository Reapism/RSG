using System.ComponentModel;
using System.Diagnostics;

namespace RSG.Core.Models
{
    internal class ThreadReference
    {
        public ThreadReference(in ProcessThread processThread)
        {
            ProcessThread = processThread;
            Id = processThread.Id;
            State = processThread.ThreadState.ToString();
            Priority = processThread.CurrentPriority;
            BoostProcessWhenFocused = processThread.PriorityBoostEnabled;
        }

        public ProcessThread ProcessThread { get; internal set; }
        public int Id { get; internal set; }
        public string State { get; internal set; }
        public int Priority { get; internal set; }
        [Description("Boost When Focused")]
        public bool BoostProcessWhenFocused { get; set; }
    }
}
