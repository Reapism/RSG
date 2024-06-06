﻿namespace RSG
{
    public interface IGenerator<TRequest, TResult>
    {
        Task<TResult> GenerateAsync(TRequest request, CancellationToken cancellationToken);
    }
}