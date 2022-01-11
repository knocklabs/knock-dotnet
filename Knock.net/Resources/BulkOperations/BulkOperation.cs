namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock BulkOperation record.
    /// </summary>
    public class BulkOperation
    {
        /// <summary>
        /// The id of the bulk operation
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the operation
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The estimated number of rows that will be modified
        /// </summary>
        [JsonProperty("estimated_total_rows")]
        public string EstimatedTotalRows { get; set; }

        /// <summary>
        /// The number of rows that have been processed
        /// </summary>
        [JsonProperty("processed_rows")]
        public string ProcessedRows { get; set; }

        /// <summary>
        /// The current status of the operation, one of: `queued`, `processing`, `completed`, `failed`
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The date the operation started (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("started_at")]
        public string StartedAt { get; set; }

        /// <summary>
        /// The date the operation finished (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("completed_at")]
        public string CompletedAt { get; set; }

        /// <summary>
        /// The date the operation failed (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("failed_at")]
        public string FailedAt { get; set; }
    }
}
