using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using RSG.Core.Utilities;
using RSG.View.Managers;
using RSG.View.ViewModels;
using RSG.View.Views;
using RSG.View.Windows;
using System;

namespace RSG.View
{
    public static class IocContainer
    {
        public static SimpleIoc Container { get; }

        static IocContainer()
        {
            Container = SimpleIoc.Default;
        }

        public static void Initialize()
        {
            RegisterRsgCore();
            RegisterViewTypes();
            RegisterViewModels();
            RegisterViewServices();
        }

        private static void RegisterViewServices()
        {
            Container.Register<PageManager>();
        }

        private static void RegisterViewModels()
        {
            Container.Register<AboutViewModel>();
            Container.Register<DialogViewModel>();
            Container.Register<DictionaryDetailsViewModel>();
            Container.Register<DictionaryEditViewModel>();
            Container.Register<SearchDetailsViewModel>();
            Container.Register<SearchEditViewModel>();
            Container.Register<StringDetailsViewModel>();
            Container.Register<StringEditViewModel>();
            Container.Register<SettingsEditViewModel>();
        }

        private static void RegisterViewTypes()
        {
            Container.Register<AboutView>();
            Container.Register<Dialog>();
            Container.Register<DictionaryDetailsView>();
            Container.Register<DictionaryEditView>();
            Container.Register<SearchDetailsView>();
            Container.Register<SearchEditView>();
            Container.Register<StringDetailsView>();
            Container.Register<StringEditView>();
            Container.Register<SettingsEditView>();
        }

        private static void RegisterRsgCore()
        {
            Container.AddRsgCore();
        }
    }
}
