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
        /// <summary>
        /// Shuffles an array of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        public void Shuffle(T[] array);
    }
}
