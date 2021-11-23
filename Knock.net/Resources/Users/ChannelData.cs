namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock ChannelData record.
    /// </summary>
    public class ChannelData
    {
        /// <summary>
        /// The channel identifier that the data belongs to.
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// The data associated
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
