using GalaSoft.MvvmLight.Ioc;
using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Interfaces.Services;
using RSG.Core.Models;
using RSG.Core.Services;
using RSG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RSG.Core.Extensions
{
    public static class ISimpleIocExtensions
    {
        public static ISimpleIoc AddRsgCore(this ISimpleIoc container)
        {
            return RegisterCoreTypes(container);
        }

        private static ISimpleIoc RegisterCoreTypes(ISimpleIoc container)
        {
            RegisterConfigurations(container);

            container.Register<IRsgDictionary, RsgDictionary>();
            container.Register<ICharacterFrequency, CharacterFrequency>();
            container.Register<IResult, Result>();
            container.Register<IStringResult, StringResult>();
            container.Register<IDictionaryResult, DictionaryResult>();
            container.Register<IIterationsFrequency, IterationsFrequency>();
            container.Register<IStatistics, Statistics>();
            container.Register<IStringResult, StringResult>();
            container.Register<IGeneratedWord, GeneratedWord>();
            container.Register<IDictionaryWordList, DictionaryWordList>();
            container.Register<IThreadService, ThreadService>();
            container.Register<IShuffle<char>, Scrambler>();
            container.Register<CharacterSetService>();
            container.Register<RandomStringGenerator>();
            container.Register<RandomWordGenerator>();
            container.Register<WordListService>();
            container.Register<DictionaryService>();

            return container;
        }

        private static ISimpleIoc RegisterConfigurations(ISimpleIoc container)
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
                container
                    .Register<IRsgConfiguration>(() => rsgConfiguration);

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
                container
                    .Register<IStringConfiguration>(() => stringConfiguration);

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
                container
                    .Register<IDictionaryConfiguration>(() => dictionaryConfiguration);

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

            return container;
        }
    }
}
