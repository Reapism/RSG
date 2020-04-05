using System.ComponentModel;

namespace RSG.Library.Models
{
    internal class ProcessReference
    {
        public string Name { get; set; }
        [Description("Required Memory for Startup")]
        public int RequiredMemoryForStartup { get; set; }
    }

}
