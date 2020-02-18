using System;

namespace RSG.Library
{
    public class GeneratorEvents
    {
        public event EventHandler<GeneratorEventArgs> Start;
        public event EventHandler<GeneratorEventArgs> Progress;
        public event EventHandler<GeneratorEventArgs> End;
    }
}


