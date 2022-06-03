using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Provides a mechanism for retrieving specific
    /// units of frequencies.
    /// </summary>
    public interface IIterationsFrequencyProvider
    {
        /// <summary>
        /// Gets a value indicating a sequence of <see cref="FrequencyUnit"/>(s).
        /// </summary>
        IEnumerable<FrequencyUnit> FrequencyUnits { get; }
    }

    /// <summary>
    /// Embeds information on computing the number of iterations it would take
    /// to reach an single unit of something.
    /// </summary>
    public class FrequencyUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyUnit"/> class.
        /// </summary>
        /// <param name="unit">The unit to compute to.</param>
        /// <param name="iterationsPerSecond">The number of iterations something completes in.</param>
        /// <param name="iterationsPerUnitFunc">A function to return the number of iterations it would take to reach <paramref name="unit"/></param>
        public FrequencyUnit(string unit, BigInteger iterationsPerSecond, Func<BigInteger> iterationsPerUnitFunc)
        {
            Unit = unit;
            IterationsPerSecond = iterationsPerSecond;
            ComputeIterationsPerUnit = iterationsPerUnitFunc ?? throw new ArgumentNullException(nameof(iterationsPerUnitFunc));
        }

        /// <summary>
        /// Gets a value indicating the unit to compute.
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// Gets a value indicating the number of iterations per second.
        /// </summary>
        public BigInteger IterationsPerSecond { get; }

        /// <summary>
        /// Returns the number of iterations it would take to compute a single
        /// <see cref="Unit"/> given the number of <see cref="IterationsPerSecond"/>.
        /// </summary>
        public Func<BigInteger> ComputeIterationsPerUnit { get; }


        public override string ToString()
        {
            return $"You will iterate {ComputeIterationsPerUnit.Invoke():n0} times per {Unit}.";
        }
    }

    public class Test
    {
        private readonly BigInteger iterationsPerSecond;

        public Test()
        {
            var freqUnit = new FrequencyUnit("Day", iterationsPerSecond, GetIterationsPerDay);
            freqUnit.ToString();
        }

        private BigInteger GetIterationsPerDay()
        {
            return iterationsPerSecond * 60 * 60 * 24;
        }
    }
}
