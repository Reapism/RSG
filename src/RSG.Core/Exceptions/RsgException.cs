using System;

namespace RSG.Core.Exceptions
{
    public class RsgException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsgException"/> class.
        /// </summary>
        public RsgException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsgException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RsgException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsgException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception</param>
        public RsgException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
