using RSG.Core.Interfaces;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
{
    public class RsgStringResult : RsgResult, IStringResult
    {
        public string Characters { get; set; }
        public IEnumerable<string> Strings { get; set; }
        public BigInteger StringLength { get; set; }
    }
}