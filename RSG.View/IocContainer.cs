using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using RSG.View.Managers;
using System;

namespace RSG.View
{
    public class IocContainer
    {
        private IServiceCollection Services { get; set; }

        public IServiceProvider Provider { get; set; }

        public IocContainer()
        {
            Services = new ServiceCollection();
        }

        public static void Initialize(IocContainer container)
        {
            container.Services.AddRsgCore();

            RegisterViewTypes(container);
#if DEBUG
            container.Provider = container.Services.BuildServiceProvider(
                new ServiceProviderOptions()
                {
                    ValidateOnBuild = true,
                    ValidateScopes = true
                });
#else
            container.Provider = container.Services.BuildServiceProvider();
#endif

        }

        private static void RegisterViewTypes(IocContainer container)
        {
            container.Services
                .AddTransient<PageManager>();
        }
    }
}
