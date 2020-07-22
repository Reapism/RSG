using System.Numerics;
using System.Threading;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// A contract for various configuration settings
    /// for string generation.
    /// </summary>
    public interface IStringConfiguration
    {
        BigInteger StringLength { get; set; }

        ICharacterSet CharacterSet { get; set; }
    }
}
