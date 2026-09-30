namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a request made to a provider to deliver a message.
    /// </summary>
    public class MessageDeliveryLog
    {
        /// <summary>
        /// The id of the delivery log
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The environment id the delivery log belongs to
        /// </summary>
        [JsonProperty("environment_id")]
        public string EnvironmentId { get; set; }

        /// <summary>
        /// The name of the provider service the request was made to
        /// </summary>
        [JsonProperty("service_name")]
        public string ServiceName { get; set; }

        /// <summary>
        /// The request made to the provider (method, host, path, headers, query and body)
        /// </summary>
        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }

        /// <summary>
        /// The response received from the provider (status, headers and body)
        /// </summary>
        [JsonProperty("response")]
        public Dictionary<string, object> Response { get; set; }

        /// <summary>
        /// The date this delivery log was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string InsertedAt { get; set; }
    }
}
