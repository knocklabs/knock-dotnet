﻿namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A PreferenceSet object in Knock
    /// </summary>
    public class PreferenceSet
    {
        /// <summary>
        /// The id of the preference set
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// A dictionary of workflow preferences
        /// </summary>
        [JsonProperty("workflows")]
        public Dictionary<string, object> Workflows { get; set; }

        /// <summary>
        /// A dictionary of channel type preferences
        /// </summary>
        [JsonProperty("channel_types")]
        public Dictionary<string, object> ChannelTypes { get; set; }


        /// <summary>
        /// A dictionary of category preferences
        /// </summary>
        [JsonProperty("categories")]
        public Dictionary<string, object> Categories { get; set; }
    }
}
