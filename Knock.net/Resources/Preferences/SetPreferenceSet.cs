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
    }
}
