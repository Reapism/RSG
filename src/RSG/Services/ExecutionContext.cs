namespace RSG.Services
{
    public sealed class ExecutionContext
    {
        public int ThreadCount { get; set; }
        public string MachineName { get; set; }

        public static ExecutionContext Default => new()
        {
            MachineName = Environment.MachineName,
            ThreadCount = Environment.ProcessorCount
        };
    }
}
