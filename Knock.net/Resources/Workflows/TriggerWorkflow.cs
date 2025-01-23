namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Params to trigger a workflow run
    /// </summary>
    public class TriggerWorkflow : BaseOptions
    {
        /// <summary>
        /// A list of recipients to execute the workflow for. This can be a list
        /// of A) user ids, B) object references, C) dictionaries with data to
        /// identify a user or object, or D) a combination thereof.
        /// </summary>
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }

        /// <summary>
        /// An optional reference to the actor executing the notification. This
        /// can be A) a user id, B) an object reference, or C) a dictionary with
        /// data to identify a user or object.
        /// </summary>
        [JsonProperty("actor")]
        public object Actor { get; set; }

        /// <summary>
        /// A dictionary of data to pass through
        /// </summary>
        [JsonProperty("data")]
        public JToken Data { get; set; }


        /// <summary>
        /// An optional reference to the tenant for when the workflow runs. This
        /// can be A) a tenant id, B) an object reference without the collection, or 
        /// C) a dictionary with data to identify a tenant.
        /// </summary>
        [JsonProperty("tenant")]
        public object Tenant { get; set; }

        /// <summary>
        /// A cancellation key
        /// </summary>
        [JsonProperty("cancellation_key")]
        public string CancellationKey { get; set; }
    }
}
