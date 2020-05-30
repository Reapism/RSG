using System.Net;
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
        /// <exception cref="WebException">Thrown when something exceptional 
        /// happens when downloading the file from the url.</exception>
        public static async Task<string> DownloadFileAsString(string directUrl)
        {
            if (string.IsNullOrEmpty(directUrl))
                return string.Empty;

            using var webClient = new WebClient();
            var reply = await webClient.DownloadStringTaskAsync(directUrl);

            return reply;
        }
    }
}
