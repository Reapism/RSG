using Xunit.Abstractions;

namespace RSG.Tests
{
    public class RandomStringGeneratorTests
    {
        private readonly IGenerator<StringRequest, StringResult> rsgGenerator;
        private readonly ITestOutputHelper console;

        public RandomStringGeneratorTests(ITestOutputHelper console)
        {
            this.rsgGenerator = new RandomStringGenerator(new SystemRandomProvider(new Random(1)));
            this.console = console;
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(10, 10)]

        public async Task HappyCase(int iterations, int stringLength)
        {
            var request = StringRequest.Default(iterations, stringLength);
            var result = await rsgGenerator.GenerateAsync(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(request, result.Request);

            Assert.True(result.IsCompletedSuccessfully);
            Assert.False(result.IsCancelled);

            Assert.True(result.Duration > TimeSpan.Zero);

            Assert.True(result.Strings.Count() == request.Iterations);

            foreach (var str in result.Strings)
            {
                Assert.True(str.Length == request.StringLength);
            }
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(500000, 1000, true)]
        public async Task HandlesCancellation(int iterations, int stringLength, bool cancel)
        {
            var request = StringRequest.Default(iterations, stringLength);
            var cts = new CancellationTokenSource();
            var fireAndForget = rsgGenerator.GenerateAsync(request, cts.Token);

            if (cancel)
            {
                cts.CancelAfter(10); //ms
            }

            var result = await fireAndForget;
           
            console.WriteLine("Count: {0}", result.Strings.Count());
            Assert.NotNull(result);
            Assert.Equal(request, result.Request);

            // Conditional assertion on when cancellation occurs.
            if (request.Iterations != result.Strings.Count())
            {
                Assert.Equal(cancel, result.IsCancelled);
            }
            else
            {
                // cancellation requested after request was fufilled, so it shouldn't have cancelled.
                Assert.False(result.IsCancelled);
            }

            Assert.True(result.Duration > TimeSpan.Zero);

            if (!result.IsCancelled)
            {
                Assert.True(result.Strings.Count() == request.Iterations);

                foreach (var str in result.Strings)
                {
                    Assert.True(str.Length == request.StringLength);
                }
            }
        }

        [Fact]
        public async Task HandlesNullRequest()
        {
            StringRequest request = null;
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await rsgGenerator.GenerateAsync(request, CancellationToken.None));
        }
    }
}
