using RSG.View.Windows;
using System.Linq;
using System.Windows;

namespace RSG.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModelLocator ViewModelLocator { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IocContainer.Initialize();
            ViewModelLocator = Resources["Locator"] as ViewModelLocator;
        }
    }
}
