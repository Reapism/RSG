using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// A utility to serialize and deserialize JSON
    /// objects asynchronously.
    /// <para>Use [JsonIgnore] to ignore properties or members from serialization.</para>
    /// </summary>
    public static class SerializationUtility
    {
        /// <summary>
        /// Deserialize a <typeparamref name="T"/> asynchronously its value
        /// from a local path.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="inputPath">The path to the serialized file.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file is not found.</exception>
        /// <exception cref="JsonException">An error occuring with the deserialization.</exception>
        public static async Task<T> DeserializeJsonAsync<T>(string inputPath, JsonSerializerOptions options = null)
        {
            if (!IOUtility.DoesFileExist(inputPath))
            {
                throw new FileNotFoundException($"Can't find the find at {inputPath}.");
            }

            var fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var obj = await JsonSerializer.DeserializeAsync<T>(fileStream, options);

            return obj;
        }

        /// <summary>
        /// Deserialize a <typeparamref name="T"/> asynchronously its value
        /// from a <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="stream">The stream to deserialize into a <typeparamref name="T"/>.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>A <see cref="Task{T}"/> representing the asynchronous operation.</returns>
        /// <exception cref="JsonException">An error occuring with the deserialization.</exception>
        public static async Task<T> DeserializeJsonAsync<T>(Stream stream, JsonSerializerOptions options = null)
        {
            var obj = await JsonSerializer.DeserializeAsync<T>(stream, options);
            return obj;
        }

        /// <summary>
        /// Serialize a <typeparamref name="T"/> asynchronously to a specific file in
        /// Utf-8 format.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="type">The type to serialize.</param>
        /// <param name="outputPath">The output path to the new serialized file.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SerializeJsonAsync<T>(T type, string outputPath, JsonSerializerOptions options = null)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, type, options);
        }
    }
}
