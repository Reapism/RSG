namespace RSG.Services
{
    public interface IShuffle<T>
    {
        public void Shuffle(T[] array);
    }


    /// <summary>
    /// Implements the Fisher-Yates (Knuth) shuffle algorithm for shuffling arrays.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class KnuthShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnuthShuffle{T}"/> class with the specified random provider.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        public KnuthShuffle(IRandomProvider<int> randomProvider)
        {
            _randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using the Fisher-Yates algorithm.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        public void Shuffle(T[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var j = _randomProvider.Next(i, array.Length); // Select from the unshuffled portion
                (array[j], array[i]) = (array[i], array[j]);
            }
        }
    }

    /// <summary>
    /// Implements a parallel version of the Fisher-Yates (Knuth) shuffle algorithm for shuffling arrays.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class ParallelKnuthShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelKnuthShuffle{T}"/> class with the specified random provider.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        public ParallelKnuthShuffle(IRandomProvider<int> randomProvider)
        {
            _randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using a parallel approach to the Fisher-Yates algorithm.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// Note: The Fisher-Yates shuffle is inherently sequential. This parallel implementation uses locking to prevent race conditions,
        /// which may negate performance benefits and is generally not recommended for shuffling purposes.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            Parallel.For(0, array.Length, i =>
            {
                int j = _randomProvider.Next(i, array.Length);
                // To prevent race conditions, use a lock or other synchronization mechanism.
                lock (array)
                {
                    (array[j], array[i]) = (array[i], array[j]);
                }
            });
        }
    }

    /// <summary>
    /// Implements Sattolo's algorithm for generating cyclic permutations of arrays.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class SattoloShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SattoloShuffle{T}"/> class with the specified random provider.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        public SattoloShuffle(IRandomProvider<int> randomProvider)
        {
            _randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using Sattolo's algorithm to generate a single-cycle permutation.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        public void Shuffle(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                // Select a random index from 0 to i-1
                int j = _randomProvider.Next(0, i);
                // Swap elements at indices i and j
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }

    /// <summary>
    /// Implements the Riffle shuffle algorithm, simulating the way humans shuffle playing cards by interleaving two halves.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class RiffleShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiffleShuffle{T}"/> class with the specified random provider.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        public RiffleShuffle(IRandomProvider<int> randomProvider)
        {
            _randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using the Riffle shuffle algorithm.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// This method splits the array approximately in half and interleaves the two halves.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            // Split the array approximately in half
            int mid = array.Length / 2 + _randomProvider.Next(-array.Length / 10, array.Length / 10);
            mid = Math.Clamp(mid, 1, array.Length - 1);
            T[] left = array.Take(mid).ToArray();
            T[] right = array.Skip(mid).ToArray();

            int i = 0, j = 0, k = 0;

            // Interleave the two halves
            while (i < left.Length && j < right.Length)
            {
                if (_randomProvider.Next(0, 2) == 0)
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            // Copy any remaining elements
            while (i < left.Length)
                array[k++] = left[i++];

            while (j < right.Length)
                array[k++] = right[j++];
        }
    }

    /// <summary>
    /// Implements a random exchange shuffle algorithm that performs a series of random swaps on the array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class RandomExchangeShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;
        private readonly int _swapCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomExchangeShuffle{T}"/> class with the specified random provider and swap count.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        /// <param name="swapCount">The number of random swaps to perform. Defaults to 100.</param>
        public RandomExchangeShuffle(IRandomProvider<int> randomProvider, int swapCount = 100)
        {
            _randomProvider = randomProvider;
            _swapCount = swapCount;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place by performing a series of random swaps.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// The number of swaps determines the level of randomness. A higher swap count increases randomness but may impact performance.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < _swapCount; i++)
            {
                int j = _randomProvider.Next(0, n);
                int k = _randomProvider.Next(0, n);
                if (j != k)
                {
                    (array[j], array[k]) = (array[k], array[j]);
                }
            }
        }
    }

    /// <summary>
    /// Implements the Block shuffle algorithm, which divides the array into blocks and shuffles these blocks.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class BlockShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;
        private readonly int _blockSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockShuffle{T}"/> class with the specified random provider and block size.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        /// <param name="blockSize">The size of each block to shuffle. Defaults to 2.</param>
        public BlockShuffle(IRandomProvider<int> randomProvider, int blockSize = 2)
        {
            _randomProvider = randomProvider;
            _blockSize = blockSize;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place by shuffling blocks of elements.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// This method divides the array into blocks of a specified size and shuffles the order of these blocks.
        /// The order of elements within each block remains unchanged.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            int n = array.Length;
            int numberOfBlocks = n / _blockSize;
            if (n % _blockSize != 0) numberOfBlocks++;

            // Create list of block indices
            var blocks = Enumerable.Range(0, numberOfBlocks).ToList();

            // Shuffle the block indices
            for (int i = 0; i < blocks.Count; i++)
            {
                int j = _randomProvider.Next(i, blocks.Count);
                (blocks[i], blocks[j]) = (blocks[j], blocks[i]);
            }

            // Rearrange blocks in the array
            T[] shuffled = new T[n];
            int index = 0;
            foreach (var block in blocks)
            {
                int start = block * _blockSize;
                for (int k = 0; k < _blockSize && start + k < n; k++)
                {
                    shuffled[index++] = array[start + k];
                }
            }

            // Copy shuffled array back to original array
            Array.Copy(shuffled, array, n);
        }
    }

    /// <summary>
    /// Implements a binary shuffle algorithm that utilizes bitwise operations for shuffling elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class BinaryShuffle<T> : IShuffle<T>
    {
        private readonly IRandomProvider<int> _randomProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryShuffle{T}"/> class with the specified random provider.
        /// </summary>
        /// <param name="randomProvider">The random provider to use for generating random indices.</param>
        public BinaryShuffle(IRandomProvider<int> randomProvider)
        {
            _randomProvider = randomProvider;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using bitwise operations for integer types.
        /// For non-integer types, it falls back to a regular swap.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// This shuffle performs bitwise XOR swaps if the elements are of integer type. Otherwise, it performs standard swaps.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int j = _randomProvider.Next(0, n);
                // Perform a bitwise swap if T is an integer type
                if (typeof(T) == typeof(int))
                {
                    int xi = Convert.ToInt32(array[i]);
                    int xj = Convert.ToInt32(array[j]);
                    array[i] = (T)Convert.ChangeType(xi ^ xj, typeof(T));
                    array[j] = (T)Convert.ChangeType(xi ^ xj, typeof(T));
                }
                else
                {
                    // Fallback to regular swap for non-integer types
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
        }
    }

    /// <summary>
    /// Implements a Gaussian shuffle algorithm that biases the shuffling process based on a Gaussian distribution.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array to shuffle.</typeparam>
    public class GaussianShuffle<T> : IShuffle<T>
    {
        private readonly Random _random;
        private readonly double _mean;
        private readonly double _stdDev;

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianShuffle{T}"/> class with the specified mean and standard deviation.
        /// </summary>
        /// <param name="mean">The mean value for the Gaussian distribution.</param>
        /// <param name="stdDev">The standard deviation for the Gaussian distribution.</param>
        public GaussianShuffle(double mean, double stdDev)
        {
            _random = new Random();
            _mean = mean;
            _stdDev = stdDev;
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place using a Gaussian-distributed approach.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <remarks>
        /// This shuffle biases the selection of swap indices based on a Gaussian distribution, introducing a non-uniform randomness.
        /// </remarks>
        public void Shuffle(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // Generate a Gaussian-distributed random number
                double gaussian = GenerateGaussian(_mean, _stdDev);
                int j = (int)Math.Round(gaussian);
                j = Math.Clamp(j, 0, n - 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        /// <summary>
        /// Generates a Gaussian-distributed random number using the Box-Muller transform.
        /// </summary>
        /// <param name="mean">The mean value for the distribution.</param>
        /// <param name="stdDev">The standard deviation for the distribution.</param>
        /// <returns>A double representing a Gaussian-distributed random number.</returns>
        private double GenerateGaussian(double mean, double stdDev)
        {
            // Using Box-Muller transform
            double u1 = 1.0 - _random.NextDouble(); // Uniform(0,1] random doubles
            double u2 = 1.0 - _random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * randStdNormal;
        }
    }

}
