using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// A utility to serialize and deserialize JSON
    /// objects asynchronously.
    /// <para>Use [<see cref="JsonIgnoreAttribute"/>] to ignore properties or members from serialization.</para>
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
        /// Deserialize a <typeparamref name="T"/> its value
        /// from a <see cref="ReadOnlySpan{Byte}"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="utf8Json">Parses the UTF-8 encoded JSON into a <typeparamref name="T"/> instance.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="JsonException">An error occuring with the deserialization.</exception>
        public static T DeserializeJson<T>(ReadOnlySpan<byte> utf8Json, JsonSerializerOptions options = null)
        {
            var obj = JsonSerializer.Deserialize<T>(utf8Json, options);
            return obj;
        }

        /// <summary>
        /// Deserialize a <typeparamref name="T"/> its value
        /// from a <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="stream">A <see cref="Stream"/> containing the serialized utf8 encoded json file.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="JsonException">An error occuring with the deserialization.</exception>
        public static T DeserializeJson<T>(Stream stream, JsonSerializerOptions options = null)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            var bytes = memoryStream.ToArray();
            var readOnlySpan = new ReadOnlySpan<byte>(bytes, 0, bytes.Length);
            return DeserializeJson<T>(readOnlySpan, options);
        }

        /// <summary>
        /// Deserialize a <typeparamref name="T"/> its value
        /// from a physical file path.
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="inputPath">The path to the serialized file.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file is not found.</exception>
        /// <exception cref="JsonException">An error occuring with the deserialization.</exception>
        public static T DeserializeJson<T>(string inputPath, JsonSerializerOptions options = null)
        {
            if (!IOUtility.DoesFileExist(inputPath))
            {
                throw new FileNotFoundException($"Can't find the find at {inputPath}.");
            }

            var bytes = File.ReadAllBytes(inputPath);
            var readOnlySpan = new ReadOnlySpan<byte>(bytes, 0, bytes.Length);
            var obj = JsonSerializer.Deserialize<T>(readOnlySpan, options);

            return obj;
        }

        /// <summary>
        /// Serialize a <typeparamref name="T"/> asynchronously to a specific file in
        /// Utf-8 format.
        /// </summary>
        /// <typeparam name="T">The type to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="outputPath">The output path to the new serialized file.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SerializeJsonAsync<T>(T value, string outputPath, JsonSerializerOptions options = null)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, value, options);
        }

        /// <summary>
        /// Serialize a <typeparamref name="T"/> synchronously to a specific file in
        /// Utf-8 format.
        /// </summary>
        /// <typeparam name="T">The type to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="outputPath">The output path to the new serialized file.</param>
        /// <param name="options">Optional serializer options.</param>
        public static void SerializeJson<T>(T value, string outputPath, JsonSerializerOptions options = null)
        {
            var jsonString = JsonSerializer.Serialize(value, options);
            File.WriteAllText(outputPath, jsonString);
        }

        /// <summary>
        /// Serialize a <typeparamref name="T"/> synchronously into a <see cref="ReadOnlySpan{byte}"/>.
        /// </summary>
        /// <typeparam name="T">The type to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Optional serializer options.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static ReadOnlySpan<byte> SerializeJson<T>(T value, JsonSerializerOptions options = null)
        {
            var jsonString = JsonSerializer.Serialize(value, options);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            var readOnlySpan = new ReadOnlySpan<byte>(bytes, 0, bytes.Length);

            return readOnlySpan;
        }
    }
}
