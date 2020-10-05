using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RSG.Core.Models
{
    /// <summary>
    /// The base class for a Result
    /// </summary>
    public abstract class ResultBase : IResult
    {
        static ResultBase()
        {
            Empty = new Result
            {
                EndTime = DateTime.UnixEpoch,
                StartTime = DateTime.UnixEpoch,
                Iterations = BigInteger.MinusOne,
                RandomizationType = RandomizationType.Pseudorandom
            };

        }

        /// Returns an empty <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">An <see cref="IResult"/> type.</typeparam>
        /// <returns>Returns an empty casted <typeparamref name="T"/>.</returns>
        public T As<T>()
            where T : IResult
        {
            return (T)Empty;
        }

        /// <summary>
        /// Gets an empty representation of <see cref="IResult"/>.
        /// </summary>
        /// <returns>Returns an empty <see cref="IResult"/>.</returns>
        public static IResult Empty { get; private set; }

        /// <summary>
        /// Gets or sets the randomization type used during this generation instance.
        /// </summary>
        public RandomizationType RandomizationType { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations for this generation.
        /// </summary>
        public BigInteger Iterations { get; set; }

        /// <summary>
        /// Gets or sets the start time of this generation.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of this generation.
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
