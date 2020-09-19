using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.DependencyInjection;
using RSG.Core.Extensions;
using System;

namespace RSG.Core.Tests.Unit
{
    public static class TestContainer
    {
        public static SimpleIoc Container { get; }

        static TestContainer()
        {
            Container = SimpleIoc.Default;
            Container.AddRsgCore();
        }
    }
}
