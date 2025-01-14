using Microsoft.Extensions.DependencyInjection;
using RSG.Services;
using RSG.Strings;
using RSG.Words;
using Xunit.Abstractions;

namespace RSG.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IServiceProvider ServiceProvider;
        private readonly ITestOutputHelper TestOutput;

        public TestBase()
        {
            var sc = new ServiceCollection();
            ConfigureServices(sc);
            ServiceProvider = sc.BuildServiceProvider();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRandomProvider<int>, SystemRandomProvider>();
            services.AddSingleton<IRsgContext, RsgContext>();
            services.AddScoped<IGenerator<StringRequest, StringResult>, RandomStringGenerator>();
            services.AddScoped<IGenerator<WordRequest, WordResult>, RandomWordGenerator>();
            services.AddScoped<Services.ExecutionContext>();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
