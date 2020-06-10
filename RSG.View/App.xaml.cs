using Microsoft.Extensions.DependencyInjection;
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
        public static IocContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container = new IocContainer()
            {
                Services = new ServiceCollection()
            };

            IocContainer.Initialize(Container);

            if (Container.Provider != null)
            {
                var mainWindow = Current.Windows.OfType<RsgWindow>().FirstOrDefault();
            }
        }
    }
}
