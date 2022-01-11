namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk setting user preferences
    /// </summary>
    public class BulkSetUserPreferencesOptions : BaseOptions
    {
        /// <summary>
        /// The list of user ids to apply the preference to
        /// </summary>
        [JsonProperty("user_ids")]
        public List<string> UserIds { get; set; }

        /// <summary>
        /// The preferences to apply
        /// </summary>
        [JsonProperty("preferences")]
        public SetPreferencesOptions Preferences { get; set; }
    }
}
