using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Result;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Models.Result;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.IO;
using System.Reflection;

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
                .AddTransient<ICharacterSetService, CharacterSetService>()
                .AddTransient<IRandomStringGenerator, RandomStringGenerator>()
                .AddTransient<IGenerator, RandomWordGenerator>()
                .AddTransient<IWordListService, WordListService>()
                .AddTransient<IDictionaryService, DictionaryService>();

            return services;
        }

        private static IServiceCollection RegisterConfigurations(IServiceCollection services)
        {
            try
            {
                var dirInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
                var rsgConfigSrc = Path.Combine(dirInfo.Parent.FullName, LoadRsgConfiguration.ExternalConfigurationName);
                var isRsgConfigInternal = false;
                var useInternalRsgConfig = string.IsNullOrEmpty(rsgConfigSrc) || !IOUtility.DoesFileExist(rsgConfigSrc);

                if (useInternalRsgConfig)
                {
                    rsgConfigSrc = LoadStringConfiguration.InternalConfigurationName;
                    isRsgConfigInternal = true;
                }

                var rsgConfiguration = new LoadRsgConfiguration().LoadJson(rsgConfigSrc, isRsgConfigInternal);
                services
                    .AddSingleton<IRsgConfiguration>(rsgConfiguration);

                var stringConfigSrc = rsgConfiguration.StringConfigurationSource;
                var isStringConfigInternal = false;
                var useInternalStringConfig = string.IsNullOrEmpty(rsgConfiguration.StringConfigurationSource) || !IOUtility.DoesFileExist(stringConfigSrc);
                var stringPathToSerialize = Path.Combine(dirInfo.Parent.FullName, LoadStringConfiguration.ExternalConfigurationName);

                if (useInternalStringConfig)
                {
                    stringConfigSrc = LoadStringConfiguration.InternalConfigurationName;
                    isStringConfigInternal = true;
                }

                var stringConfiguration = new LoadStringConfiguration().LoadJson(stringConfigSrc, isStringConfigInternal);
                services
                    .AddSingleton<IStringConfiguration>(stringConfiguration);

                var dictionaryConfigSrc = rsgConfiguration.DictionaryConfigurationSource;
                var isDictionaryConfigInternal = false;
                var useInternalDictionaryConfig = string.IsNullOrEmpty(rsgConfiguration.DictionaryConfigurationSource) || !IOUtility.DoesFileExist(stringConfigSrc);
                var dictionaryPathToSerialize = Path.Combine(dirInfo.Parent.FullName, LoadDictionaryConfiguration.ExternalConfigurationName);

                if (useInternalDictionaryConfig)
                {
                    dictionaryConfigSrc = LoadDictionaryConfiguration.InternalConfigurationName;
                    isDictionaryConfigInternal = true;
                }

                var dictionaryConfiguration = new LoadDictionaryConfiguration().LoadJson(dictionaryConfigSrc, isDictionaryConfigInternal);
                services
                    .AddSingleton<IDictionaryConfiguration>(dictionaryConfiguration);

                if (useInternalStringConfig)
                {
                    SerializationUtility.SerializeJson(stringConfiguration, stringPathToSerialize);
                }

                if (useInternalDictionaryConfig)
                {
                    SerializationUtility.SerializeJson(dictionaryConfiguration, dictionaryPathToSerialize);
                }

                if (useInternalRsgConfig)
                {
                    // Since we are using the internal rsg configuration file, set the new paths to string and dictionary configurations.
                    rsgConfiguration.StringConfigurationSource = stringPathToSerialize;
                    rsgConfiguration.DictionaryConfigurationSource = dictionaryPathToSerialize;
                    SerializationUtility.SerializeJson(rsgConfiguration, Path.Combine(dirInfo.Parent.FullName, LoadRsgConfiguration.ExternalConfigurationName));
                }
            }
            catch (Exception e)
            {
                LogUtility.Write("Rsg Configuration", $"Unable to load internal/external rsg configuration(s).", e);
                throw e;
            }

            return services;
        }
    }
}
