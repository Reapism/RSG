using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Services;
using RSG.Core.Services;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Internal utilties for scrambling strings.
    /// </summary>
    public class Scrambler : IShuffle<char>
    {
        private readonly IRandom random;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scrambler"/> class.
        /// </summary>
        public Scrambler()
        {
            random = RandomProvider.Random;
        }

        /// <summary>
        /// Shuffles an array.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        public void Shuffle<T>(T[] array)
        {
            KnuthShuffle(array, random);
        }

        /// <summary>
        /// Performs a Knuth Shuffle on the <typeparamref name="T"/> array.
        /// </summary>
        /// <typeparam name="T">A Type array.</typeparam>
        /// <param name="array">An array of type <typeparamref name="T"/>.</param>
        /// <param name="random">The <see cref="IRandom"/> provider.</param>
        private void KnuthShuffle<T>(T[] array, IRandom random)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var j = random.Next(i, array.Length); // Don't select from the entire array, only what hasn't been shuffled.
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
