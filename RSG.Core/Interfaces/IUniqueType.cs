namespace RSG.Core.Interfaces
{
    /// <summary>
    /// Defines a certain type to be Unique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUniqueType<T>
        where T : class
    {
        string UniqueChecksum { get; }

        string GenerateUniqueChecksum(T type);
    }
}