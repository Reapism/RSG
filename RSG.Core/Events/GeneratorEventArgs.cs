using System;

namespace RSG.Core.Events
{
    public class GeneratorEventArgs : EventArgs
    {
        public long StartTick { get; set; }
        public long EndTick { get; set; }
        public byte Progress { get; set; }
    }
}
