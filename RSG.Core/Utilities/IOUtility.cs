using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Provides static utilities functions for
    /// Input/Output operations.
    /// </summary>
    public static class IOUtility
    {
        /// <summary>
        /// Read lines a 
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="delimiter">The delimiter to split the contents of the file by.</param>
        /// <returns>A sequence of strings delimited by new line characters.</returns>
        public static async Task<IEnumerable<string>> ReadLinesASync(string filePath, string delimiter = "\n")
        {
            if (!DoesFileExist(filePath))
                throw new FileNotFoundException($"Cannot find file located at '{filePath}'.");

            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);

            var fileText = await streamReader.ReadToEndAsync();

            return fileText.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Determines whether a file exists in a given path.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <returns>Whether the file exists.</returns>
        public static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Determines whether a directory exists in a given path.
        /// </summary>
        /// <param name="directoryPath">The path of the directory.</param>
        /// <returns>Whether the directory exists.</returns>
        public static bool DoesDirectoryExist(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
    }
}
