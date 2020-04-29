using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    public static class SerializationUtility
    {
        /// <summary>
        /// Deserialize a <typeparamref name="T"/> asynchronously its value
        /// </summary>
        /// <typeparam name="T">The type to deserialize.</typeparam>
        /// <param name="type">T</param>
        /// <param name="inputPath">The path to the serialized file.</param>
        /// <returns>Returns the deserialized object from the json file.</returns>
        public static async Task<T> DeserializeJson<T>(T type, string inputPath)
        {
            if (!IOUtility.DoesFileExist(inputPath))
                throw new FileNotFoundException($"Can't find the find at {inputPath}.");

            var fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var obj = await JsonSerializer.DeserializeAsync<T>(fileStream);

            return obj;
        }

        public static async void SerializeJson<T>(T type, string outputPath)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, type);
        }
    }
}
