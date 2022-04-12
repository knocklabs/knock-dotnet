using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock Activity record.
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// The id of the activity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cursor of the activity, used for pagination
        /// </summary>
        [JsonProperty("__cursor")]
        public string cursor { get; set; }

        /// <summary>
        /// The recipient of the activity
        /// </summary>
        [JsonProperty("recipient")]
        public object recipient { get; set; }

        /// <summary>
        /// The actor of the activity
        /// </summary>
        [JsonProperty("actor")]
        public object actor { get; set; }

        /// <summary>
        /// The date this activity was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string insertedAt { get; set; }

        /// <summary>
        /// The date this activity was last updated at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string updatedAt { get; set; }

        /// <summary>
        /// Data related to the activity
        /// </summary>
        [JsonProperty("data")]

        public Dictionary<string, object> data { get; set; }
    }
}
