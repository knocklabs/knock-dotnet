using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock Message record.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The id of the message
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cursor of the message, used for pagination
        /// </summary>
        [JsonProperty("__cursor")]
        public string cursor { get; set; }

        /// <summary>
        /// The id of the channel the message was generated for
        /// </summary>
        [JsonProperty("channel_id")]
        public string channelId { get; set; }

        /// <summary>
        /// The id of the tenant the message belongs to
        /// </summary>
        [JsonProperty("tenant")]
        public string tenant { get; set; }

        /// <summary>
        /// The recipient of the message
        /// </summary>
        [JsonProperty("recipient")]
        public object recipient { get; set; }

        /// <summary>
        /// The status of the message
        /// </summary>
        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// The date this message was read (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("read_at")]
        public string readAt { get; set; }

        /// <summary>
        /// The date this message was seen (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("seen_at")]
        public string seenAt { get; set; }

        /// <summary>
        /// The date this message was archived (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("archived_at")]
        public string archivedAt { get; set; }

        /// <summary>
        /// The date this message was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string insertedAt { get; set; }

        /// <summary>
        /// The date this message was updated at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string updatedAt { get; set; }

        /// <summary>
        /// Data related to the message
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> data { get; set; }

        /// <summary>
        /// The source of the message
        /// </summary>
        [JsonProperty("source")]
        public Dictionary<string, object> source { get; set; }
    }
}
