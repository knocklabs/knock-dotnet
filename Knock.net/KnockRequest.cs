namespace Knock
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    /// <summary>
    /// Describes a request made to the Knock API.
    /// </summary>
    public class KnockRequest
    {
        /// <summary>
        /// The HTTP method.
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// The parameters for the HTTP request.
        /// </summary>
        public object Options { get; set; }

        /// <summary>
        /// The path of the Knock API request.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The access token to use for authentication instead of an API key.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Dictionary of custom Knock headers.
        /// </summary>
        public IDictionary<string, string> KnockHeaders { get; set; }

        /// <summary>
        /// Optional flag to indicate if the request is JSON encoded.
        /// </summary>
        public bool IsJsonContentType { get; set; } = true;
    }
}
