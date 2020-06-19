using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Configuration;
using RSG.Core.Utilities;
using System.Numerics;

namespace RSG.Core.Configuration
{
    public class StringConfiguration : IStringConfiguration, ILoadConfiguration<StringConfiguration>
    {
        public StringConfiguration()
        {

        }


        public BigInteger StringLength { get; set; }
        public ICharacterSet CharacterSet { get; set; }

        public T Load<T>(string fileName)
        {
            return default(T);
        }
    }
}