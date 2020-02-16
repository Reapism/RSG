using System;

namespace RSG.Library
{
    public class GeneratorEvents
    {
        public event Action<object, GeneratorEventArgs> GenerateStart
        {
            add
            {
                GenerateStart += value;
            }
            remove
            {
                GenerateStart -= value;
            }
        }

    }
}

