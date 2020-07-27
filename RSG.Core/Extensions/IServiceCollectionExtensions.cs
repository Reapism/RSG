﻿using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;

namespace RSG.Core.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="IServiceCollection"/> contract to add
    /// <see cref="RSG.Core"/> services for dependency injection.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all <see cref="Core"/> required services to use the public API.
        /// <para>The DI container requires a <see cref="IServiceCollection"/> contract
        /// to register contracts/concretes.</para>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> contract used to
        /// register the contracts/concretes.</param>
        /// <returns>Returns the <see cref="IServiceCollection"/> with the new
        /// services from <see cref="Core"/>.</returns>
        public static IServiceCollection AddRsgCore(this IServiceCollection services)
        {
            return RegisterCoreTypes(services);
        }

        private static IServiceCollection RegisterCoreTypes(IServiceCollection services)
        {
            RegisterConfigurations(services);

            services
                .AddTransient<IRsgDictionary, RsgDictionary>()
                .AddTransient<ICharacterFrequency, CharacterFrequency>()
                .AddTransient<IResult, Result>()
                .AddTransient<IStringResult, StringResult>()
                .AddTransient<IDictionaryResult, DictionaryResult>()
                .AddTransient<IIterationsFrequency, IterationsFrequency>()
                .AddTransient<IStatistics, Statistics>()
                .AddTransient<IStringResult, StringResult>()
                .AddTransient<IGeneratedWord, GeneratedWord>()
                .AddTransient<IDictionaryWordList, DictionaryWordList>()
                .AddTransient<IThreadService, ThreadService>()
                .AddTransient<IShuffle<char>, Scrambler>()
                .AddTransient<CharacterSetService>()
                .AddTransient<RandomStringGenerator>()
                .AddTransient<RandomWordGenerator>()
                .AddTransient<WordListService>()
                .AddTransient<DictionaryService>();

            return services;
        }

        private static IServiceCollection RegisterConfigurations(IServiceCollection services)
        {
            var rsgConfiguration = new LoadRsgConfiguration().LoadJson("rsg.json", false);
            services
                .AddSingleton(rsgConfiguration);

            var stringConfig = rsgConfiguration.StringConfigurationSource;
            var isStringConfigInternal = false;

            if (string.IsNullOrEmpty(rsgConfiguration.StringConfigurationSource))
            {
                stringConfig = "DefaultStringConfiguration.json";
                isStringConfigInternal = true;
            }

            var dictionaryConfig = rsgConfiguration.DictionaryConfigurationSource;
            var isDictionaryConfigInternal = false;

            if (string.IsNullOrEmpty(rsgConfiguration.DictionaryConfigurationSource))
            {
                dictionaryConfig = "DefaultDictionaryConfiguration.json";
                isDictionaryConfigInternal = true;
            }

            var stringConfiguration = new LoadStringConfiguration().LoadJson(stringConfig, isStringConfigInternal);
            var dictionaryConfiguration = new LoadDictionaryConfiguration().LoadJson(dictionaryConfig, isDictionaryConfigInternal);

            services
                .AddSingleton(stringConfiguration)
                .AddSingleton(dictionaryConfiguration);

            return services;
        }
    }
}
