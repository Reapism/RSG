using RSG.Core.Enums;
using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSG.Core.Models
{
    public class StringResult : IStringResult
    {
        public string Characters { get; set; }

        public IEnumerable<string> Strings { get; set; }

        public BigInteger StringLength { get; set; }

        public RandomizationType RandomizationType { get; set; }

        public BigInteger Iterations { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}