using System.IO;
using System.Text;

namespace RSG.Library.Utilities
{
    public static class ExportLog
    {
        public static void ExportLogFile(SimpleStatistics statistics, string path, Encoding encoding)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            using var streamWriter = new StreamWriter(fileStream, encoding);

            streamWriter.WriteLine(statistics.ToString());
        }

    }
}
