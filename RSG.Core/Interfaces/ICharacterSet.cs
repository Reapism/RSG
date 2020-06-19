using RSG.Core.Models;
using System.Collections.Generic;

namespace RSG.Core.Interfaces
{
    public interface ICharacterSet
    {
        IDictionary<string, SingleCharacterSet> Characters { get; set; }
    }
}
