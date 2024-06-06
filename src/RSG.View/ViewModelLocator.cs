using Microsoft.Extensions.DependencyInjection;
using RSG.View.ViewModels;

namespace RSG.View
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

        }

        private T Get<T>()
        {
            return IocContainer.ServiceProvider.GetService<T>();
        }

        public AboutViewModel AboutViewModel => Get<AboutViewModel>();
        public DialogViewModel DialogViewModel => Get<DialogViewModel>();
        public DictionaryEditViewModel DictionaryEditViewModel => Get<DictionaryEditViewModel>();
        public DictionaryDetailsViewModel DictionaryDetailsViewModel => Get<DictionaryDetailsViewModel>();
        public SearchDetailsViewModel SearchDetailsViewModel => Get<SearchDetailsViewModel>();
        public SearchEditViewModel SearchEditViewModel => Get<SearchEditViewModel>();
        public SettingsEditViewModel SettingsEditViewModel => Get<SettingsEditViewModel>();
        public StringDetailsViewModel StringDetailsViewModel => Get<StringDetailsViewModel>();
        public StringEditViewModel StringEditViewModel => Get<StringEditViewModel>();
    }
}
