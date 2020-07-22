using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    /// <summary>
    /// Responsible for creating a <see cref="StringConfiguration"/> instance.
    /// </summary>
    public class LoadStringConfiguration : ILoad<StringConfiguration>
    {
        public StringConfiguration LoadJson(string file, bool isInternal)
        {
            if (isInternal)
            {
                var dictionaryConfiguration = ResourceUtility.GetResourceStream(file);
                return SerializationUtility.DeserializeJson<StringConfiguration>(file);
            }

            return SerializationUtility.DeserializeJson<StringConfiguration>(file);
        }

        /// <summary>
        /// Loads an instance of <see cref="StringConfiguration"/> based on
        /// an existing file.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <param name="isInternal">Whether the file is embedded internally in the assembly
        /// or exists standalone on the file system.</param>
        /// <returns>Returns an instance of <see cref="StringConfiguration"/.></returns>
        public async Task<StringConfiguration> LoadJsonAsync(string file, bool isInternal)
        {
            if (isInternal)
            {
                var dictionaryConfiguration =  ResourceUtility.GetResourceStream(file);
                return await SerializationUtility.DeserializeJsonAsync<StringConfiguration>(dictionaryConfiguration);
            }

            return await SerializationUtility.DeserializeJsonAsync<StringConfiguration>(file);
        }
    }
}
