namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a member of a Knock Audience.
    /// </summary>
    public class AudienceMember
    {
        /// <summary>
        /// The id of the user that is a member of the audience
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// The user that is a member of the audience
        /// </summary>
        [JsonProperty("user")]
        public Dictionary<string, object> User { get; set; }

        /// <summary>
        /// The tenant the membership is scoped to, if any
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// The date the member was added to the audience (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("added_at")]
        public string AddedAt { get; set; }
    }
}
