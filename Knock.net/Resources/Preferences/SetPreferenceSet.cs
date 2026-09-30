namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// JSON to set preference set preference options
    /// </summary>
    public class SetPreferenceSet : BaseOptions
    {
        /// <summary>
        /// Workflow preferences to set
        /// </summary>
        [JsonProperty("workflows")]
        public Dictionary<string, object> Workflows { get; set; }

        /// <summary>
        /// Channel type preferences to set
        /// </summary>
        [JsonProperty("channel_types")]
        public Dictionary<string, object> ChannelTypes { get; set; }

        /// <summary>
        /// Category prferences to set
        /// </summary>
        [JsonProperty("categories")]
        public Dictionary<string, object> Categories { get; set; }

        /// <summary>
        /// Per-channel preferences to set
        /// </summary>
        [JsonProperty("channels")]
        public Dictionary<string, object> Channels { get; set; }

        /// <summary>
        /// Whether the recipient is subscribed to commercial communications
        /// </summary>
        [JsonProperty("commercial_subscribed")]
        public object CommercialSubscribed { get; set; }

        /// <summary>
        /// How to persist the preferences, one of: `merge`, `replace` (the default)
        /// </summary>
        [JsonProperty("__persistence_strategy__")]
        public string PersistenceStrategy { get; set; }
    }
}
