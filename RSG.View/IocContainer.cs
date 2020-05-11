using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Configuration;
using RSG.Core.Factories;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace RSG.View
{
    public class IocContainer
    {
        public ServiceCollection Services { get; set; }

        public IServiceProvider Provider { get; set; }
    }
}
