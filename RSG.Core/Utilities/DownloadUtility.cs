using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace RSG.Core.Utilities
{
    public static class DownloadUtility
    {
        /// <summary>
        /// Downloads a resource as a string in a asynchronous operation.
        /// </summary>
        /// <param name="directUrl">The direct URL to download from.</param>
        /// <returns>The resource as a <see langword="string"/>.</returns>
        public static string DownloadFileAsString(string directUrl)
        {
            using var webClient = new WebClient();
            var reply = webClient.DownloadStringTaskAsync(directUrl);

            return reply.Result;
        }
    }
}
