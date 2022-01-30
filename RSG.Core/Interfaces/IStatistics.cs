using RSG.Core.Interfaces.Request;
using RSG.Core.Interfaces.Result;
using System.Numerics;

namespace RSG.Core.Interfaces
{
    public interface IStatistics
    {
        IStringRequest StringRequest { get; }
        IDictionaryRequest DictionaryRequest { get; }

        IStringResult StringResult { get; }
        IDictionaryResult DictionaryResult { get; }

        ICharacterFrequency CharacterFrequency { get; }

    }
}
