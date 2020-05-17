using RSG.Core.Models;
using System.IO;
using System.Text;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Provides static utlity functions for exporting
    /// certain objects.
    /// </summary>
    public static class ExportUtilities
    {
        public static void ExportLogFile(Log logFile, string path, Encoding encoding)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            using StreamWriter streamWriter = new StreamWriter(fileStream, encoding);

            streamWriter.WriteLine(logFile.ToString());
        }


    }
}
