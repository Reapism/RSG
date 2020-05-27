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
            base.OnStartup(e);

            Container = await Initialize();
            Container.Provider = Container.Services.BuildServiceProvider();

            if (Container.Provider != null)
            {
                var mainWindow = Current.Windows.OfType<RsgWindow>().FirstOrDefault();
                MessageBox.Show("Loaded asynchronously");
            }
        }

        private static async Task<IocContainer> Initialize()
        {
            Container = new IocContainer()
            {
                Services = new ServiceCollection()
            };

            RegisterTypes(Container);
            RegisterTypesAsync(Container);

            return Container;
        }

        private static async void InitializeAsync()
        {
            RegisterTypesAsync(Container);
        }

        private static void RegisterTypes(IocContainer container)
        {
            // Register singletons types
            container.Services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>()
                .AddSingleton<IStringConfiguration, StringConfiguration>()
                .AddSingleton<IDictionaryConfiguration, DictionaryConfiguration>()
                .AddSingleton<DictionaryServiceFactory>()
                .AddSingleton<DictionaryService>();
            // Register scoped types
            container.Services
                .AddScoped<IRsgDictionary, RsgDictionary>()
                .AddScoped<ICharacterFrequency, CharacterFrequency>()
                .AddScoped<ICharacterSet, CharacterSet>()
                .AddScoped<IResult, Result>()
                .AddScoped<IStringResult, StringResult>()
                .AddScoped<IDictionaryResult, DictionaryResult>()
                .AddScoped<IIterationsFrequency, IterationsFrequency>()
                .AddScoped<IStatistics, Statistics>()
                .AddScoped<IStringResult, StringResult>()
                .AddScoped<IGeneratedWord, GeneratedWord>()
                .AddScoped<IDictionaryWordList, DictionaryWordList>()
                .AddScoped<CharacterSetService>()
                .AddScoped<WordListService>()
                .AddScoped<RandomStringGenerator>()
                .AddScoped<RandomWordGenerator>();


            // Register transients types
        }

        /// <summary>
        /// Register asynchronous types that require IO to be constructed.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private static async void RegisterTypesAsync(IocContainer container)
        {
            
        }
    }
}
