using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Requests;
using RSG.Core.Interfaces.Result;
using System;

namespace RSG.Core.Models.Results
{
    /// <summary>
    /// A class containing a result.
    /// </summary>
    /// <typeparam name="TRequest">A request instance.</typeparam>
    public class Result<TRequest> : IResult
        where TRequest : class, IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="request">A request instance used to create a result.</param>
        /// <param name="duration">The duration of the generated result.</param>
        public Result(TRequest request, TimeSpan duration)
        {
            Request = Request;
            Duration = duration;
        }

        public TRequest Request { get; }
        public TimeSpan Duration { get; }
    }
}
