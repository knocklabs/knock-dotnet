namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Identifies the guide step an engagement is recorded against
    /// </summary>
    public class GuideStepOptions : BaseOptions
    {
        /// <summary>
        /// The id of the guide channel
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// The id of the guide
        /// </summary>
        [JsonProperty("guide_id")]
        public string GuideId { get; set; }

        /// <summary>
        /// The key of the guide
        /// </summary>
        [JsonProperty("guide_key")]
        public string GuideKey { get; set; }

        /// <summary>
        /// The ref of the guide step
        /// </summary>
        [JsonProperty("guide_step_ref")]
        public string GuideStepRef { get; set; }

        /// <summary>
        /// An optional tenant the engagement is scoped to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }
    }

    /// <summary>
    /// Options for marking a guide as seen
    /// </summary>
    public class GuideSeenOptions : GuideStepOptions
    {
        /// <summary>
        /// The content of the guide step that was seen
        /// </summary>
        [JsonProperty("content")]
        public Dictionary<string, object> Content { get; set; }

        /// <summary>
        /// Optional data used to render the guide
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }

    /// <summary>
    /// Options for marking a guide as interacted with
    /// </summary>
    public class GuideInteractedOptions : GuideStepOptions
    {
        /// <summary>
        /// Optional metadata about the interaction
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, object> Metadata { get; set; }
    }

    /// <summary>
    /// Options for marking a guide as archived
    /// </summary>
    public class GuideArchivedOptions : GuideStepOptions
    {
        /// <summary>
        /// Whether this is the final step of the guide
        /// </summary>
        [JsonProperty("is_final")]
        public bool? IsFinal { get; set; }

        /// <summary>
        /// Whether the guide is exempt from guide group throttling
        /// </summary>
        [JsonProperty("unthrottled")]
        public bool? Unthrottled { get; set; }
    }

    /// <summary>
    /// Options for unarchiving a guide
    /// </summary>
    public class GuideUnarchivedOptions : BaseOptions
    {
        /// <summary>
        /// The key of the guide
        /// </summary>
        [JsonProperty("guide_key")]
        public string GuideKey { get; set; }

        /// <summary>
        /// An optional tenant the engagement is scoped to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }
    }

    /// <summary>
    /// Options for resetting a user's engagement with a guide
    /// </summary>
    public class GuideResetOptions : BaseOptions
    {
        /// <summary>
        /// The key of the guide
        /// </summary>
        [JsonProperty("guide_key")]
        public string GuideKey { get; set; }

        /// <summary>
        /// An optional tenant the engagement is scoped to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }
    }
}
