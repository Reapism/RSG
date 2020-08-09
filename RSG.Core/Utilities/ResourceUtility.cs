
using RSG.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// Gets the <see cref="RsgConfiguration"/> from instalation location.
        /// </summary>
        /// <returns></returns>
        public static async Task<RsgConfiguration> GetRsgConfigurationFile()
        {
            // Get's the install location of RSG and read the configuration file from within the directory.
            // Need to create an installer that will download the files to a specific directory and create the configuration file.
            var path = Path.Combine(Assembly.GetExecutingAssembly().Location, "RSG.config");
            var config = await SerializationUtility.DeserializeJsonAsync<RsgConfiguration>(path);

            return config;
        }

        /// <summary>
        /// Gets a string from a <see cref="Uri"/> address.
        /// </summary>
        /// <param name="address">The address of the resource.</param>
        /// <returns>A string representation of the resource.</returns>
        public static async Task<string> GetStringAsync(Uri address)
        {
            using (var client = new HttpClient())
            {
                var value = await client.GetStringAsync(address);

                return value;
            }
        }

        public static Stream GetResourceStream(string fileName)
        {
            var resourceName = embeddedResourceNames
                .FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase) ||
                                name.Contains(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
            {
                throw new FileNotFoundException($"Cannot find file {fileName}");
            }

            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            return resourceStream;
        }

        private static IEnumerable<string> GetResourcesNames()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            return names;
        }
    }
}
