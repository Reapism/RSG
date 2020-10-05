using RSG.Core.Enums;
using System;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IResult
    {
        RandomizationType RandomizationType { get; set; }

        BigInteger Iterations { get; set; }

        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }

        /// <summary>
        /// Returns an empty <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">An <see cref="IResult"/> type.</typeparam>
        /// <returns>Returns an empty casted <typeparamref name="T"/>.</returns>
        public T As<T>()
            where T : IResult;
    }
}