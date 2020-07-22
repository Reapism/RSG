using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    /// <summary>
    /// Responsible for creating a <see cref="DictionaryConfiguration"/> instance.
    /// </summary>
    public class LoadDictionaryConfiguration : ILoad<DictionaryConfiguration>
    {
        public DictionaryConfiguration LoadJson(string file, bool isInternal)
        {
            if (isInternal)
            {
                var dictionaryConfiguration = ResourceUtility.GetResourceStream(file);
                return SerializationUtility.DeserializeJson<DictionaryConfiguration>(dictionaryConfiguration);
            }

            return SerializationUtility.DeserializeJson<DictionaryConfiguration>(file);
        }

        /// <summary>
        /// Loads an instance of <see cref="DictionaryConfiguration"/> based on
        /// an existing file.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <param name="isInternal">Whether the file is embedded internally in the assembly
        /// or exists standalone on the file system.</param>
        /// <returns>Returns an instance of <see cref="DictionaryConfiguration"/.></returns>
        public async Task<DictionaryConfiguration> LoadJsonAsync(string file, bool isInternal)
        {
            if (isInternal)
            {
                var dictionaryConfiguration = ResourceUtility.GetResourceStream(file);
                return await SerializationUtility.DeserializeJsonAsync<DictionaryConfiguration>(dictionaryConfiguration);
            }

            return await SerializationUtility.DeserializeJsonAsync<DictionaryConfiguration>(file);
        }
    }
}
