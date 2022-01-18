using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;

namespace RSG.Core.Tests.Unit
{
    public static class TestContainer
    {
        private static IServiceCollection Services;
        static TestContainer()
        {
            Services = new ServiceCollection();
            Services.AddRsgCore();
        }
    }
}
