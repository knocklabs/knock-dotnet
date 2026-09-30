namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a single recipient's run of a workflow.
    /// </summary>
    public class WorkflowRecipientRun
    {
        /// <summary>
        /// The id of the workflow recipient run
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The id of the workflow run this recipient run belongs to
        /// </summary>
        [JsonProperty("workflow_run_id")]
        public string WorkflowRunId { get; set; }

        /// <summary>
        /// The key of the workflow
        /// </summary>
        [JsonProperty("workflow")]
        public string Workflow { get; set; }

        /// <summary>
        /// The current status, one of: `queued`, `processing`, `paused`, `completed`, `cancelled`
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The recipient of the run
        /// </summary>
        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        /// <summary>
        /// The actor of the run
        /// </summary>
        [JsonProperty("actor")]
        public object Actor { get; set; }

        /// <summary>
        /// The id of the tenant the run belongs to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// The number of errors that occurred during the run
        /// </summary>
        [JsonProperty("error_count")]
        public int? ErrorCount { get; set; }

        /// <summary>
        /// Information about what triggered the run
        /// </summary>
        [JsonProperty("trigger_source")]
        public Dictionary<string, object> TriggerSource { get; set; }

        /// <summary>
        /// The events that occurred during the run (only returned when fetching a single run)
        /// </summary>
        [JsonProperty("events")]
        public List<Dictionary<string, object>> Events { get; set; }

        /// <summary>
        /// The date the run was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string InsertedAt { get; set; }

        /// <summary>
        /// The date the run was updated at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
