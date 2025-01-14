using RSG.Words;

namespace RSG.Services
{
    public interface IRsgContext
    {
        public IRandomProvider<int> RandomProvider { get; }

        public ExecutionContext ExecutionContext { get; }
        public List<WordDictionary> Dictionaries { get; }
    }

    public record RsgContext(IRandomProvider<int> RandomProvider, ExecutionContext ExecutionContext, List<WordDictionary> Dictionaries) : IRsgContext;
}
