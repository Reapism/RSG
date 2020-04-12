using System.ComponentModel;

namespace RSG.Core.Models
{
    internal class ProcessReference
    {
        public string Name { get; set; }
        [DisplayName("Required Memory for Startup")]
        public int RequiredMemoryForStartup { get; set; }
    }

}
