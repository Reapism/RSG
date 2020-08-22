using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using RSG.View.Managers;
using RSG.View.ViewModels;
using RSG.View.Views;
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

        /// <summary>
        /// Initalizes the container by adding <see cref="RSG.Core"/>
        /// type mappings.
        /// </summary>
        /// <param name="container"></param>
        public static void Initialize(IocContainer container)
        {
            if (container is null)
            {
                container = new IocContainer();
            }

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
                .AddTransient<AboutViewModel>()
                .AddTransient<DialogViewModel>()
                .AddTransient<DictionaryDetailsViewModel>()
                .AddTransient<SearchDetailsViewModel>()
                .AddTransient<SearchEditViewModel>()
                .AddTransient<StringDetailsViewModel>()
                .AddTransient<StringEditViewModel>()
                .AddTransient<AboutView>()
                .AddTransient<Dialog>()
                .AddTransient<DictionaryDetailsView>()
                .AddTransient<SearchDetailsView>()
                .AddTransient<SearchEditView>()
                .AddTransient<SettingsEditView>()
                .AddTransient<StringDetailsView>()
                .AddTransient<StringEditView>();
        }
    }
}
