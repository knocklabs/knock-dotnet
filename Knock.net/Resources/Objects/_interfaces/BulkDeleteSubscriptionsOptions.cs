namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk deleting the subscriptions for a single object
    /// </summary>
    public class BulkDeleteSubscriptionsOption : BaseOptions
    {
        /// <summary>
        /// The ID for the object the recipients are subscribed to
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// A list of recipients to unsubscribe from the object. This can be a
        /// list of A) user ids, B) object references, or C) a combination thereof.
        /// </summary>
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }
    }

    /// <summary>
    /// Options for bulk deleting object subscriptions
    /// </summary>
    public class BulkDeleteSubscriptionsOptions : BaseOptions
    {
        /// <summary>
        /// A list of bulk delete subscription ops
        /// </summary>
        [JsonProperty("subscriptions")]
        public List<BulkDeleteSubscriptionsOption> Subscriptions { get; set; }
    }
}
