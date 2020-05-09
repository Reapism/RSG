using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSG.Core
{
    public static class IocContainer
    {
        private static ServiceCollection Services { get; set; }

        static IocContainer()
        {
            Services = new ServiceCollection();

            Initalize();
        }

        public static void Initalize()
        {
            RegisterTypes();

        }

        private static void RegisterTypes()
        {
            // Register singletons types
            Services
                .AddSingleton<IRsgConfiguration, RsgConfiguration>();

            // Register transients types

            // Register scoped types
        }
    }
}
