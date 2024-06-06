using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using RSG.View.Managers;
using RSG.View.ViewModels;
using RSG.View.Views;
using System;

namespace RSG.View
{
    public static class IocContainer
    {
        public static IServiceCollection Services { get; }
        public static IServiceProvider ServiceProvider { get; private set; }
        static IocContainer()
        {
            Services = new ServiceCollection();
        }

        public static void Initialize()
        {
            RegisterRsgCore();
            RegisterViewTypes();
            RegisterViewModels();
            RegisterViewServices();

            ServiceProvider = Services.BuildServiceProvider();
        }

        private static void RegisterViewServices()
        {
            Services.AddScoped<PageManager>();
        }

        private static void RegisterViewModels()
        {
            Services.AddScoped<AboutViewModel>();
            Services.AddScoped<DialogViewModel>();
            Services.AddScoped<DictionaryDetailsViewModel>();
            Services.AddScoped<DictionaryEditViewModel>();
            Services.AddScoped<SearchDetailsViewModel>();
            Services.AddScoped<SearchEditViewModel>();
            Services.AddScoped<StringDetailsViewModel>();
            Services.AddScoped<StringEditViewModel>();
            Services.AddScoped<SettingsEditViewModel>();
        }

        private static void RegisterViewTypes()
        {
            Services.AddScoped<AboutView>();
            Services.AddScoped<Dialog>();
            Services.AddScoped<DictionaryDetailsView>();
            Services.AddScoped<DictionaryEditView>();
            Services.AddScoped<SearchDetailsView>();
            Services.AddScoped<SearchEditView>();
            Services.AddScoped<StringDetailsView>();
            Services.AddScoped<StringEditView>();
            Services.AddScoped<SettingsEditView>();
        }

        private static void RegisterRsgCore()
        {
            Services.AddRsgCore();
        }
    }
}
