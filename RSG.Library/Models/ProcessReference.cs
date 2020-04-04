using System.ComponentModel;

namespace RSG.Library.Models
{
    internal class ProcessReference
    {
        public string Name { get; set; }
        [Description("Required memory for startup")]
        public int RequiredMemoryForStartup { get; set; }
    }

}
