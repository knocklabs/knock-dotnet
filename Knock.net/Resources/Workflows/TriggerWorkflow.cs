namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params to trigger a workflow run
    /// </summary>
    public class TriggerWorkflow : BaseOptions
    {
        /// <summary>
        /// A list of recipients to execute the workflow for
        /// </summary>
        [JsonProperty("recipients")]
        public List<string> Recipients { get; set; }

        /// <summary>
        /// The actor id
        /// </summary>
        [JsonProperty("actor")]
        public string Actor { get; set; }

        /// <summary>
        /// A dictionary of data to pass through
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }


        /// <summary>
        /// An optional tenant identifier
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// A cancellation key
        /// </summary>
        [JsonProperty("cancellation_key")]
        public string CancellationKey { get; set; }
    }
}
