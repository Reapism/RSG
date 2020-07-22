using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using System.Numerics;
using System.Text.Json.Serialization;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : IStringConfiguration, ILoad<StringConfiguration>
    {
        public StringConfiguration()
        {

        }

        public string Length => StringLength.ToString();

        [JsonIgnore]
        public BigInteger StringLength { get; set; }

        public ICharacterSet CharacterSet { get; set; }

        public T Load<T>(string fileName)
        {
            return default(T);
        }
    }
}