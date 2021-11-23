namespace Knock
{
    using System.Net.Http;

    /// <summary>
    /// Describes options for a Client connecting to the Knock API.
    /// </summary>
    public class KnockOptions
    {
        /// <summary>
        /// Base URL for the Knock API.
        /// </summary>
        public string ApiBaseURL { get; set; }

        /// <summary>
        /// The API key to authenticate requests to the Knock API.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The HTTP client to make API requests.
        /// </summary>
        public HttpClient HttpClient { get; set; }
    }
}
