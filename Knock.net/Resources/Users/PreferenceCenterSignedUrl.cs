namespace Knock
{
    using Newtonsoft.Json;

    /// <summary>
    /// A signed URL and token for a user's hosted preference center.
    /// </summary>
    public class PreferenceCenterSignedUrl
    {
        /// <summary>
        /// The signed URL to the user's preference center
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// The signed token used in the URL
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
