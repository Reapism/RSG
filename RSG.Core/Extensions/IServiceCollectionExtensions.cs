using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
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
            services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>()
                .AddSingleton<IStringConfiguration, StringConfiguration>()
                .AddSingleton<IDictionaryConfiguration, DictionaryConfiguration>();

            services
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
                .AddTransient<DictionaryThreadService>();

            return services;
        }
    }
}
