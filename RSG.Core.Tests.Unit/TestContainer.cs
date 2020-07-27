using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using System;

namespace RSG.Core.Tests.Unit
{
    public class TestContainer
    {
        public IServiceProvider Provider { get; }
        public IServiceCollection Services { get; set; }

        public TestContainer()
        {
            Services = new ServiceCollection();
            Services.AddRsgCore();
            Provider = Services.BuildServiceProvider();
        }
    }
}
