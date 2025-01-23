namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains information about a Knock Object record.
    /// </summary>
    public class Object
    {
        /// <summary>
        /// The id of the object
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The collection this object belongs to
        /// </summary>
        [JsonProperty("collection")]
        public string Collection { get; set; }

        /// <summary>
        /// The data associated
        /// </summary>
        [JsonProperty("properties")]
        public JToken Data { get; set; }

        /// <summary>
        /// The created at date for this object (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// The updated date for this object (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
