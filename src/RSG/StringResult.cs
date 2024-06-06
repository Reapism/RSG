using System.Numerics;

namespace RSG
{
    public class StringRequest
    {
        public StringRequest() { }
        /// <summary>
        /// Gets the number of iterations for this request.
        /// </summary>
        public BigInteger Iterations { get; init; }
        /// <summary>
        /// Gets the length of the string.
        /// </summary>
        public int StringLength { get; init; }

        /// <summary>
        /// Gets the collection of character(s) to be provided.
        /// </summary>
        public IList<CharacterSet> CharacterSets { get; init; }

        public int MaxThreadCount { get; init; } = Environment.ProcessorCount;

        public static StringRequest Default(BigInteger iterations, int stringLength) => new() 
        {
            Iterations = iterations,
            StringLength = stringLength,
            CharacterSets = CharacterSet.AllSets,
            MaxThreadCount = Environment.ProcessorCount
        };
    }

    /// <summary>
    /// <see cref="StringResult"/>
    /// </summary>
    public class StringResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringResult"/> class.
        /// </summary>
        /// <param name="request">The <see cref="IStringRequest"/> used for generating this result.</param>
        /// <param name="strings">The generated string(s).</param>
        /// <param name="duration">The duration of the generation.</param>
        public StringResult(StringRequest request, IEnumerable<string> strings, TimeSpan duration)
        {
            Request = request;
            Strings = strings;
            Duration = duration;
        }
        public StringRequest Request { get; init; }
        public bool IsCompletedSuccessfully { get; init; }
        public bool IsCancelled { get; init; }
        public IEnumerable<string> Strings { get; init; }
        public TimeSpan Duration { get; init; }
    }
}