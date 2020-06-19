namespace RSG.Core.Interfaces.Configuration
{
    internal interface ILoadConfiguration<T>
    {
        T Load<T>(string fileName);
    }
}