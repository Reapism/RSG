using GalaSoft.MvvmLight.Ioc;
using RSG.Core.Extensions;

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
