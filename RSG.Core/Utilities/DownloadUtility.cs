using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Utilities
{
    /// <summary>
    /// Provides static functions for downloading information
    /// from the web.
    /// </summary>
    public static class DownloadUtility
    {
        /// <summary>
        /// Downloads a resource as a string in a asynchronous operation.
        /// </summary>
        /// <param name="directUrl">The direct URL to download from.</param>
        /// <returns>The resource as a <see langword="string"/>.</returns>
        public static async Task<string> DownloadFileAsString(string directUrl)
        {
            using var webClient = new WebClient();
            var reply = await webClient.DownloadStringTaskAsync(directUrl);

            return reply;
        }
    }
}
