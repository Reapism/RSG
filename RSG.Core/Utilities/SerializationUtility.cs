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
        /// <param name="type">The instance to deserialize into.</param>
        /// <param name="inputPath">The path to the serialized file.</param>
        /// <returns>Returns the deserialized object from the json file.</returns>
        public static async Task<T> DeserializeJsonASync<T>(T type, string inputPath)
        {
            if (!IOUtility.DoesFileExist(inputPath))
                throw new FileNotFoundException($"Can't find the find at {inputPath}.");

            var fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var obj = await JsonSerializer.DeserializeAsync<T>(fileStream);

            return obj;
        }

        /// <summary>
        /// Deserialize a <typeparamref name="T"/> asynchronously its value
        /// from a <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="stream">The stream to deserialize into a <typeparamref name="T"/>.</param>
        /// <returns>Returns the deserialized object from the json file.</returns>
        public static async Task<T> DeserializeJsonASync<T>(Stream stream)
        {
            var obj = await JsonSerializer.DeserializeAsync<T>(stream);
            return obj;
        }

        /// <summary>
        /// Serialize a <typeparamref name="T"/> asynchronously to a specific file in
        /// Utf-8 format.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="type">The type to serialize.</param>
        /// <param name="outputPath">The output path to the new serialized file.</param>
        public static async void SerializeJsonASync<T>(T type, string outputPath)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, type);
        }
    }
}
