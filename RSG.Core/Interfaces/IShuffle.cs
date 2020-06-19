using System;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Defines a generalized type-specific shuffle method
    /// that a class implements to scramble the arrays instances.
    /// </summary>
    /// <typeparam name="T">A class type.</typeparam>
    public interface IShuffle<T>
        where T : struct
    {
        public void Shuffle<T>(T[] array, Random random);
    }
}
