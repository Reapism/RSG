
using System;
using System.Collections.Generic;
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

        public static IEnumerable<string> GetResourcesNames()
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            return names;
        }

        public static async Task<Stream> GetResourceStream(string fileName)
        {
            var resourceName = embeddedResourceNames.FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new FileNotFoundException($"Cannot find file {fileName}");

            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            return resourceStream;
        }
    }
}
