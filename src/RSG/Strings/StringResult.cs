using System.Numerics;

namespace RSG.Strings;
public enum RandomizationMethod
{
    Psuedorandom,
    CryptographicRandom,
    Seeded
}
public interface ICharacterListProvider
{
    IList<CharacterSetHolder> CharacterSets { get; }
}

public record StringRequest(int Iterations, int StringLength, string CharacterList, RandomizationMethod RandomizationMethod, bool GenerateStatistics, MultithreadOptions? MultiThreadOptions );
public record MultithreadOptions(int? MaxThreads, int? MinThreads, int Iterations);
public record StringResult(StringRequest Request, OperationStatus OperationStatus, IEnumerable<string> Strings, StringStatistics? StringStatistics);
public record OperationStatus(bool IsCompletedSuccessfully, bool IsCancelled, TimeSpan Duration);
public record StringStatistics(BigInteger Permutations, CharacterFrequency[] CharacterFrequencies);
public record CharacterFrequency(char Character, int Frequency, string FrequencyDescription);