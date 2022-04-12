using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock MessageContent record.
    /// </summary>
    public class MessageContent
    {
        /// <summary>
        /// The id of the message content
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The date this message event was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string insertedAt { get; set; }

        /// <summary>
        /// Data related to the message event
        /// </summary>
        [JsonProperty("data")]

        public Dictionary<string, object> data { get; set; }
    }
}