using RSG.Core.Utilities;

namespace RSG.Core.Interfaces.Services
{
    public interface IGeneratorEvents
    {
        event Completed GenerateCompleted;

        event ProgressChanged GenerateChanged;
    }
}