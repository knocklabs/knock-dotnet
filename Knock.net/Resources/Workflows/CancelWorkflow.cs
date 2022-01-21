namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params for cancelling a workflow run
    /// </summary>
    public class CancelWorkflow : BaseOptions
    {
        /// <summary>
        /// A list of recipient ids
        /// </summary>
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }

        /// <summary>
        /// The cancellation key for the workflow run
        /// </summary>
        [JsonProperty("cancellation_key")]
        public string CancellationKey { get; set; }
    }
}
