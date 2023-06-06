namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock Object record.
    /// </summary>
    public class ObjectSubscription
    {

        /// <summary>
        /// The object that the recipient is subscribed to
        /// </summary>
        [JsonProperty("object")]
        public Object Object { get; set; }

        /// <summary>
        /// The object that the recipient is subscribed to
        /// </summary>
        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        /// <summary>
        /// The data associated
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// The inserted at timestamp of the subscription
        /// </summary>
        [JsonProperty("inserted_at")]
        public string InsertedAt { get; set; }

        /// <summary>
        /// The updated date for this object (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
