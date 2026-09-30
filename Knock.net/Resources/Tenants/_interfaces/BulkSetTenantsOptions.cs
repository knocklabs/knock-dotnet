namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk setting tenants
    /// </summary>
    public class BulkSetTenantsOptions : BaseOptions
    {
        /// <summary>
        /// The tenants to upsert. Each can be A) a tenant id, or B) a dictionary
        /// with an `id` and the tenant's `name`, `settings`, `channel_data` or
        /// `preferences`.
        /// </summary>
        [JsonProperty("tenants")]
        public List<object> Tenants { get; set; }
    }
}
