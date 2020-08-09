using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Provides static utilities functions for
    /// Input/Output operations.
    /// </summary>
    internal static class IOUtility
    {
        /// <summary>
        /// Read lines from a file asynchronously using a specific
        /// delimiter for spliting each string.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="delimiter">The delimiter to split the contents of the file by.</param>
        /// <returns>A sequence of strings delimited by new line characters.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file doesnt exist.</exception>
        public static async Task<IEnumerable<string>> ReadLinesASync(string filePath, string delimiter = "\n")
        {
            if (!DoesFileExist(filePath))
            {
                throw new FileNotFoundException($"Cannot find file located at '{filePath}'.");
            }

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
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
