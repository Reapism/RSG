using RSG.Extensions;
using RSG.Strings;
using System.Numerics;

namespace RSG
{
    public interface ISearch<TRequest, TResult>
    {
        Task<TResult> SearchAsync(TRequest searchRequest, CancellationToken cancellationToken);
    }

    public enum SearchMethod : byte
    {
        /// <summary>
        /// Brute Force method checking every single string combination until reaching 
        /// </summary>
        BruteForce,
        SmartBruteForce,
    }

    public class SearchStringRequest : ICharacterListProvider
    {
        public string Value { get; init; }

        public IList<CharacterSetHolder> CharacterSets { get; init; }

        public SearchMethod Method { get; init; }
    }

    public class SearchStringResult
    {
        public SearchStringRequest Request { get; init; }
        public BigInteger Iterations { get; init; }
        public BigInteger Permutations { get; init; }

        public bool HasCompleted { get; init; }

        public static SearchStringResult Create(SearchStringRequest request, BigInteger iterations)
        {
            return new SearchStringResult()
            {
                HasCompleted = true,
                Iterations = iterations,
                Request = request,
                Permutations = request.CharacterSets.GetTotalCharacterCount()
            };
        }

        public static SearchStringResult Cancelled(SearchStringRequest request, BigInteger iterations)
        {
            return new SearchStringResult()
            {
                HasCompleted = false,
                Iterations = iterations,
                Request = request
            };
        }

        private static BigInteger GetPermutations(in BigInteger value)
        {
            throw new NotImplementedException();
        }
    }


    public class BruteForceSearcher : ISearch<SearchStringRequest, SearchStringResult>
    {
        private BigInteger iterations;
        private string searchString;

        public Task<SearchStringResult> SearchAsync(SearchStringRequest searchRequest, CancellationToken cancellationToken)
        {
            searchString = searchRequest.Value;
            iterations = BigInteger.Zero;

            if (searchRequest.Method == SearchMethod.BruteForce)
            {
                var characterSets = searchRequest.CharacterSets.Where(e => e.Enabled);
                var firstCharacter = characterSets.First().Characters.First().ToString();
                var characters = characterSets.Flatten();
                var found = OriginalSearch(ref firstCharacter, ref characters, cancellationToken);

                if (!found)
                {
                    
                }
                var result = new SearchStringResult()
                {
                    Iterations = iterations,
                    Request = searchRequest
                };
                return Task.FromResult(result);
            }
            return Task.FromResult(new SearchStringResult());
        }

        public bool OriginalSearch(ref string currentSearchString, ref string characterList, in CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return false;

            for (var i = 0; i < characterList.Length; i++)
            {
                if ((currentSearchString + characterList[i]).Equals(searchString))
                    return true;

                iterations += BigInteger.One;
                if (currentSearchString.Length + 1 < searchString.Length)
                {
                    var newSearchString = currentSearchString + characterList[i];
                    if (OriginalSearch(ref newSearchString, ref characterList, cancellationToken))
                        return true;
                }
            }
            return false;
        }

    }
}