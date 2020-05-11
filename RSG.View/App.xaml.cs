using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Factories;
using RSG.Core.Interfaces.Configuration;
using RSG.View.Windows;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml;

namespace RSG.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IocContainer Container { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var container = await Initalize();
            Container = container;

            base.OnStartup(e);
            if (container.Provider != null)
            {
                var window = new RsgWindow();
                window.Show();
                MessageBox.Show("Loaded asynchronously");
            }

        }

        private static async Task<IocContainer> Initalize()
        {
            IocContainer container;
            container = new IocContainer()
            {
                Services = new ServiceCollection()
            };

            RegisterTypes(container);
            container.Provider = await RegisterTypesAsync(container);

            return container;
        }

        private static void RegisterTypes(IocContainer container)
        {
            // Register singletons types
            container.Services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>()
                .AddSingleton<IStringConfiguration, StringConfiguration>()
                .AddSingleton<IDictionaryConfiguration, DictionaryConfiguration>();

            // Register transients types

            // Register scoped types 
        }

        private static async Task<IServiceProvider> RegisterTypesAsync(IocContainer container)
        {
            container.Services
                .AddSingleton(await DictionaryServiceFactory.CreateAsync());

            return container.Services.BuildServiceProvider();
        }
    }
}
