using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    internal class LoadRsgConfiguration : ILoad<RsgConfiguration>
    {
        public const string ExternalConfigurationName = "RSG.json";
        public const string InternalConfigurationName = "DefaultRsgConfiguration.json";

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
