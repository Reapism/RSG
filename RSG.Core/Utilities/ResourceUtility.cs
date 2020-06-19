
using RSG.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Provides static functions for getting resources
    /// from the <see cref="Core"/> assembly.
    /// </summary>
    public static class ResourceUtility
    {
        private static IEnumerable<string> embeddedResourceNames;

        static ResourceUtility()
        {
            embeddedResourceNames = GetResourcesNames();
        }

        public static async Task<Stream> GetResourceStream(string fileName)
        {
            var resourceName = embeddedResourceNames.FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
            {
                throw new FileNotFoundException($"Cannot find file {fileName}");
            }

            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            return resourceStream;
        }

        private static IEnumerable<string> GetResourcesNames()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            return names;
        }

        public static string GetRsgConfigurationFile()
        {
            // Get's the install location of RSG and read the configuration file from within the directory.
            // Need to create an installer that will download the files to a specific directory and create the configuration file.
            var configurationFile = Path.Combine(Assembly.GetExecutingAssembly().Location, "RSG.config");
            return configurationFile;
        }
    }
}
