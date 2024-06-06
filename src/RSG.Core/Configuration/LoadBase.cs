using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    /// <summary>
    /// Load base.
    /// </summary>
    /// <typeparam name="T">An instantiatable class type.</typeparam>
    internal class LoadBase<T> : ILoad<T>
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
        public T LoadJson(string file, bool isInternal)
        {
            if (isInternal)
            {
                var stream = ResourceUtility.GetResourceStream(file);
                return SerializationUtility.DeserializeJson<T>(stream);
            }

            return SerializationUtility.DeserializeJson<T>(file);
        }

        /// <summary>
        /// Loads an instance of <typeparamref name="T"/> asynchronously
        /// based on an existing file.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <param name="isInternal">Whether the file is embedded internally in the assembly
        /// or exists standalone on the file system.</param>
        /// <returns>Returns a task that represents an instance of <typeparamref name="T"/>.</returns>
        public async Task<T> LoadJsonAsync(string file, bool isInternal)
        {
            if (isInternal)
            {
                var stream = ResourceUtility.GetResourceStream(file);
                return await SerializationUtility.DeserializeJsonAsync<T>(stream);
            }

            return await SerializationUtility.DeserializeJsonAsync<T>(file);
        }
    }
}
