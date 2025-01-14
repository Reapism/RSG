namespace RSG.Services
{
    public interface IGenerator<TRequest, TResult>
    {
        Task<TResult> GenerateAsync(TRequest request, CancellationToken cancellationToken);
    }
}