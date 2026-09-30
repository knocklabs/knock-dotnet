namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A member to add to or remove from an audience
    /// </summary>
    public class AudienceMemberOption : BaseOptions
    {
        /// <summary>
        /// The user to add or remove. At minimum must contain an `id`; any other
        /// properties are used to identify the user.
        /// </summary>
        [JsonProperty("user")]
        public Dictionary<string, object> User { get; set; }

        /// <summary>
        /// An optional tenant to scope the membership to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }
    }

    /// <summary>
    /// Options for changing the members of an audience
    /// </summary>
    public class AudienceMembersOptions : BaseOptions
    {
        /// <summary>
        /// The members to add or remove (up to 1,000 per request)
        /// </summary>
        [JsonProperty("members")]
        public List<AudienceMemberOption> Members { get; set; }
    }

    /// <summary>
    /// Options for adding members to an audience
    /// </summary>
    public class AddAudienceMembersOptions : AudienceMembersOptions
    {
    }

    /// <summary>
    /// Options for removing members from an audience
    /// </summary>
    public class RemoveAudienceMembersOptions : AudienceMembersOptions
    {
    }
}
