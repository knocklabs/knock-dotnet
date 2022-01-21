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
        /// A list of recipients to execute the workflow for. Can be a list of strings,
        /// or object references (or a combination of the two).
        /// </summary>
        [JsonProperty("recipients")]
        public List<Object> Recipients { get; set; }

        /// <summary>
        /// The actor to execute the notification on behalf of (either a string,
        /// or an object reference)
        /// </summary>
        [JsonProperty("actor")]
        public Object Actor { get; set; }

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
