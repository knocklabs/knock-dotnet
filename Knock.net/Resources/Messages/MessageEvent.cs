using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock MessageEvent record.
    /// </summary>
    public class MessageEvent
    {
        /// <summary>
        /// The id of the message event
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cursor of the message event, used for pagination
        /// </summary>
        [JsonProperty("__cursor")]
        public string cursor { get; set; }

        /// <summary>
        /// The environment id the message event belongs to
        /// </summary>
        [JsonProperty("environment_id")]
        public string environmentId { get; set; }

        /// <summary>
        /// The recipient of the message event
        /// </summary>
        [JsonProperty("recipient")]
        public object recipient { get; set; }

        /// <summary>
        /// The type of the message event
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

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
