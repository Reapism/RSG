using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using RSG.View.Windows;
using System;
using System.Linq;
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
            Container = Initialize();
            Container.Provider = await InitializeAsync();
            base.OnStartup(e);

            if (Container.Provider != null)
            {
                var mainWindow = Current.Windows.OfType<RsgWindow>().FirstOrDefault();
                MessageBox.Show("Loaded asynchronously");
            }
        }

        private static IocContainer Initialize()
        {
            Container = new IocContainer()
            {
                Services = new ServiceCollection()
            };

            RegisterTypes(Container);

            return Container;
        }

        private static async Task<IServiceProvider> InitializeAsync()
        {
            Container.Provider = await RegisterTypesAsync(Container);
            return Container.Provider;
        }

        private static void RegisterTypes(IocContainer container)
        {
            // Register singletons types
            container.Services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>()
                .AddSingleton<IStringConfiguration, StringConfiguration>()
                .AddSingleton<IDictionaryConfiguration, DictionaryConfiguration>();

            // Register scoped types 

            container.Services
                .AddScoped<IRsgDictionary, RsgDictionary>()
                .AddScoped<ICharacterFrequency, CharacterFrequency>()
                .AddScoped<ICharacterSets, CharacterSet>()
                .AddScoped<IDictionaryResult, DictionaryResult>()
                .AddScoped<IIterationsFrequency, IterationsFrequency>()
                .AddScoped<IStatistics, Statistics>()
                .AddScoped<IStringResult, StringResult>()
                .AddScoped<RsgDictionaryFactory>()
                .AddScoped<DictionaryService>()
                .AddScoped<CharacterSetService>()
                .AddScoped<RandomStringGenerator>()
                .AddScoped<WordListService>()
                .AddScoped<IWordList, WordList>()
                .AddScoped<RandomStringGenerator>()
                .AddScoped<RandomStringGenerator>()


            // Register transients types
            container.Services
                .
        }

        /// <summary>
        /// Register asynchronous types that require IO to be constructed.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private static async Task<IServiceProvider> RegisterTypesAsync(IocContainer container)
        {
            container.Services
                .AddSingleton(await DictionaryServiceFactory.CreateAsync());

            return container.Services.BuildServiceProvider();
        }
    }
}
