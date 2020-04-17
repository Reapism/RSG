using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    public static class IOUtility
    {
        public static async Task<IEnumerable<string>> ReadLinesASync(string filePath)
        {
            if (!DoesFileExist(filePath))
                throw new FileNotFoundException($"Cannot find file located at '{filePath}'.");

            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);

            var fileText = await streamReader.ReadToEndAsync();

            return fileText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static bool DoesDirectoryExist(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
    }
}
