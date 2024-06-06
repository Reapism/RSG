namespace RSG
{
    public interface ISearch<TRequest, TResult>
    {
        Task<TResult> SearchAsync(TRequest searchRequest, CancellationToken cancellationToken);
    }
}