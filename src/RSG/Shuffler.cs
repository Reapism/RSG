namespace RSG
{
    /// <summary>
    /// Internal utilties for scrambling strings.
    /// </summary>
    public class Shuffler<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scrambler"/> class.
        /// </summary>
        public Shuffler(IRandomProvider<int> randomProvider)
        {
            this.randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles an array.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        public void Shuffle(T[] array)
        {
            KnuthShuffle(array);
        }

        /// <summary>
        /// Performs a Knuth Shuffle on the <typeparamref name="T"/> array.
        /// </summary>
        /// <typeparam name="T">A Type array.</typeparam>
        /// <param name="array">An array of type <typeparamref name="T"/>.</param>
        /// <param name="random">The <see cref="IRandomProvider"/> provider.</param>
        private void KnuthShuffle(T[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var j = randomProvider.Next(i, array.Length); // Don't select from the entire array, only what hasn't been shuffled.
                (array[j], array[i]) = (array[i], array[j]);
            }
        }
    }
}
