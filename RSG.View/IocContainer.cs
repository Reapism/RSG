using Microsoft.Extensions.DependencyInjection;
using System;

namespace RSG.View
{
    public class IocContainer
    {
        public ServiceCollection Services { get; set; }

        public IServiceProvider Provider { get; set; }
    }
}
