namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Options for setting data about preferences
    /// </summary>
    public class SetPreferencesOptions : BaseOptions
    {
        /// <summary>
        /// The id of the preference set (doesn't have to be set)
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// A dictionary of workflow preferences
        /// </summary>
        [JsonProperty("workflows")]
        public Dictionary<string, JToken> Workflows { get; set; }

        /// <summary>
        /// A dictionary of channel type preferences
        /// </summary>
        [JsonProperty("channel_types")]
        public Dictionary<string, JToken> ChannelTypes { get; set; }


        /// <summary>
        /// A dictionary of category preferences (not currently used)
        /// </summary>
        [JsonProperty("categories")]
        public Dictionary<string, JToken> Categories { get; set; }
    }
}
