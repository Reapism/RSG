using System;

namespace RSG.Library
{
    public class GeneratorEvents
    {
        public event EventHandler<GeneratorEventArgs> OnStart;
        public event EventHandler<GeneratorEventArgs> ProgressChanged;
        public event EventHandler<GeneratorEventArgs> OnEnd;
    }
}