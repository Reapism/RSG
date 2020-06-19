using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Factories;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using RSG.View.Managers;
using System;

namespace RSG.View
{
    public class IocContainer
    {
        private ServiceCollection Services { get; set; }

        public IServiceProvider Provider { get; set; }

        public IocContainer()
        {
            Services = new ServiceCollection();
        }

        public static void Initialize(IocContainer container)
        {
            RegisterCoreTypes(container);
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

        private static void RegisterCoreTypes(IocContainer container)
        {
            // Register singletons types
            container.Services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>()
                
                // Instantiate the string configuration by calling the Load method and passing in the string configuration source from RsgConfiguration
                .AddSingleton<IStringConfiguration, StringConfiguration>()
                .AddSingleton<IDictionaryConfiguration, DictionaryConfiguration>();
            // Register scoped types

            // Register transients types
            container.Services
                .AddTransient<IRsgDictionary, RsgDictionary>()
                .AddTransient<ICharacterFrequency, CharacterFrequency>()
                .AddTransient<ICharacterSet, CharacterSet>()
                .AddTransient<IResult, Result>()
                .AddTransient<IStringResult, StringResult>()
                .AddTransient<IDictionaryResult, DictionaryResult>()
                .AddTransient<IIterationsFrequency, IterationsFrequency>()
                .AddTransient<IStatistics, Statistics>()
                .AddTransient<IStringResult, StringResult>()
                .AddTransient<IGeneratedWord, GeneratedWord>()
                .AddTransient<IDictionaryWordList, DictionaryWordList>()
                .AddTransient<IThreadService, ThreadUtility>()
                .AddTransient<IShuffle<char>, Scrambler>()
                .AddTransient<CharacterSetService>()
                .AddTransient<RandomStringGenerator>()
                .AddTransient<RandomWordGenerator>()
                .AddTransient<WordListService>()
                .AddTransient<DictionaryService>()
                .AddTransient<DictionaryServiceFactory>()
                .AddTransient<DictionaryThreadService>();
        }

        private static void RegisterViewTypes(IocContainer container)
        {
            container.Services
                .AddTransient<PageManager>();
        }
    }
}
