using System.Collections.Generic;
using System.Text;

namespace RSG.Library.Interfaces
{
    internal interface IImportService
    {
        IEnumerable<string> Import(string path, Encoding info);
    }
}
