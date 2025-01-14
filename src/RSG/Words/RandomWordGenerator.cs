using Ardalis.GuardClauses;
using RSG.Services;

namespace RSG.Words
{
    public class RandomWordGenerator : IGenerator<WordRequest, WordResult>
    {
        private readonly IRsgContext context;
        private readonly WordListService wordListService;

        public RandomWordGenerator(IRsgContext context, WordListService wordListService)
        {
            this.context = context;
            this.wordListService = wordListService;
        }
        public async Task<WordResult> GenerateAsync(WordRequest request, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(request);
            var combinedWordList = request.WordListRequests
                .SelectMany(e => wordListService.FromInternalRequest(e).Words)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var numThreads = context.ExecutionContext.ThreadCount;
            int chunkSize = request.Iterations / numThreads;

            // Create tasks based on chunked iterations
            var tasks = new List<Task<Queue<string>>>();
            for (int i = 0; i < numThreads; i++)
            {
                int iterationsForThisTask = (i == numThreads - 1) ? request.Iterations - chunkSize * i : chunkSize;
                tasks.Add(Task.Run(() => GenerateWordsInChunk(combinedWordList, iterationsForThisTask)));
            }

            // Wait for all tasks to complete
            var queueArray = await Task.WhenAll(tasks);

            // Combine all results into a single list
            var wordsOutput =  queueArray.SelectMany(r => r).ToArray();
            var wordResult = new WordResult(request, []);

            throw new NotImplementedException();
        }

        private Queue<string> GenerateWordsInChunk(Dictionary<int, string> combinedWordList, int iterations)
        {
            var generatedWords = new Queue<string>(iterations);

            for (var i = 0; i < iterations; i++)
            {
                var randomKey = context.RandomProvider.Next(0, combinedWordList.Count);
                generatedWords.Enqueue(combinedWordList[randomKey]);
            }

            return generatedWords;
        }
    }
}
