namespace Knock
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains information about a Knock Tenant record.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// The id of the tenant
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The data associated
        /// </summary>
        [JsonProperty("properties")]
        public JToken Properties { get; set; }

        /// <summary>
        /// The created at date for this tenant (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// The updated date for this tenant (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
