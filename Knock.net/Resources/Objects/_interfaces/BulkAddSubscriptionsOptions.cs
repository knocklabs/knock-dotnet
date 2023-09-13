namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk adding an object subscription
    /// </summary>
    public class BulkAddSubscriptionsOption : BaseOptions
    {
        /// <summary>
        /// The ID for the object being subscribed to
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// An optional set of properties to apply to each subscription
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// A list of recipients to subscribe to the object. This can be a list
        /// of A) user ids, B) object references, C) dictionaries with data to
        /// identify a user or object, or D) a combination thereof.
        /// </summary>
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }
    }

    /// <summary>
    /// Options for bulk adding object subscriptions
    /// </summary>
    public class BulkAddSubscriptionsOptions : BaseOptions
    {
        /// <summary>
        /// A list of bulk add subscription ops
        /// </summary>
        [JsonProperty("subscriptions")]
        public List<BulkAddSubscriptionsOption> Subscriptions { get; set; }
    }
}
