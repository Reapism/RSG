using System.Numerics;

namespace RSG.Core.Interfaces.Configuration
{
    public interface IStringConfiguration
    {
        BigInteger StringLength { get; set; }

        ICharacterSet CharacterSet { get; set; }
    }
}
