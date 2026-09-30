namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The in-app guides a user is eligible for on a channel.
    /// </summary>
    public class GuidesResponse
    {
        /// <summary>
        /// The guides the user is eligible for
        /// </summary>
        [JsonProperty("entries")]
        public List<Dictionary<string, object>> Entries { get; set; }

        /// <summary>
        /// The guide groups that control display ordering and throttling
        /// </summary>
        [JsonProperty("guide_groups")]
        public List<Dictionary<string, object>> GuideGroups { get; set; }

        /// <summary>
        /// When guides in each group were last displayed to the user
        /// </summary>
        [JsonProperty("guide_group_display_logs")]
        public Dictionary<string, object> GuideGroupDisplayLogs { get; set; }

        /// <summary>
        /// The guides the user is not eligible for, with the reason why
        /// </summary>
        [JsonProperty("ineligible_guides")]
        public List<Dictionary<string, object>> IneligibleGuides { get; set; }
    }
}
