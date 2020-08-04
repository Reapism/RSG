using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    public class LoadRsgConfiguration : ILoad<RsgConfiguration>
    {
        public const string ConfigurationFileName = "RSG.config";

        public RsgConfiguration LoadJson(string file, bool isInternal)
        {
            if (isInternal)
            {
                var stream = ResourceUtility.GetResourceStream(file);
                return SerializationUtility.DeserializeJson<RsgConfiguration>(stream);
            }

            return SerializationUtility.DeserializeJson<RsgConfiguration>(file);
        }

        public async Task<RsgConfiguration> LoadJsonAsync(string file, bool isInternal)
        {
            if (isInternal)
            {
                var stream = ResourceUtility.GetResourceStream(file);
                return await SerializationUtility.DeserializeJsonAsync<RsgConfiguration>(stream);
            }

            return await SerializationUtility.DeserializeJsonAsync<RsgConfiguration>(file);
        }
    }
}
