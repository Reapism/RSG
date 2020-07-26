using RSG.Core.Models;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Interfaces.Configuration
{
    /// <summary>
    /// A contract for various configuration settings
    /// for string generation.
    /// </summary>
    public interface IStringConfiguration
    {
        BigInteger StringLength { get; set; }

        IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}
