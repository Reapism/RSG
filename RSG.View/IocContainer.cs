using GalaSoft.MvvmLight.Ioc;
using RSG.Core.Extensions;
using RSG.Core.Interfaces.Services;
using RSG.View.Managers;
using RSG.View.ViewModels;
using RSG.View.Views;

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
            Container.Register(() => new DictionaryDetailsView(Container.GetInstance<IRandomWordGenerator>()));
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
