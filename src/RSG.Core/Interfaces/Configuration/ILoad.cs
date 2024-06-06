using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// Contains a contract for determining how to create
    /// an instance of <typeparamref name="T"/> with
    /// a given file name and whether it's contained in the
    /// executing assembly or standalone on the filesystem.
    /// </summary>
    /// <typeparam name="T">A class thats instantiable.</typeparam>
    internal interface ILoad<T>
        where T : class, new()
    {
        /// <summary>
        /// Loads an instance of <typeparamref name="T"/> based on
        /// an existing file.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <param name="isInternal">Whether the file is embedded internally in the assembly
        /// or exists standalone on the file system.</param>
        /// <returns>Returns an instance of <typeparamref name="T"/>.</returns>
        T LoadJson(string file, bool isInternal);

        /// <summary>
        /// Loads an instance of <typeparamref name="T"/> asynchronously based on
        /// an existing file.
        /// </summary>
        /// <param name="file">The file name or the full path of the file.</param>
        /// <param name="isInternal">Whether the file is embedded internally in the assembly
        /// or exists standalone on the file system.</param>
        /// <returns>Returns an instance of <typeparamref name="T"/>.</returns>
        Task<T> LoadJsonAsync(string file, bool isInternal);
    }
}