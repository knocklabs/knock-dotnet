namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A single schedule to create as part of a bulk create
    /// </summary>
    public class BulkCreateScheduleOption : BaseOptions
    {
        /// <summary>
        /// The key of the workflow the schedule triggers
        /// </summary>
        [JsonProperty("workflow")]
        public string Workflow { get; set; }

        /// <summary>
        /// The recipient of the schedule. This can be A) a user id, B) an object
        /// reference, or C) a dictionary with data to identify a user or object.
        /// </summary>
        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        /// <summary>
        /// The repeat rules for the schedule
        /// </summary>
        [JsonProperty("repeats")]
        public List<Dictionary<string, object>> Repeats { get; set; }

        /// <summary>
        /// An optional reference to the actor executing the triggered workflow
        /// </summary>
        [JsonProperty("actor")]
        public object Actor { get; set; }

        /// <summary>
        /// A dictionary of data to pass through
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// An optional reference to the tenant related to the schedule
        /// </summary>
        [JsonProperty("tenant")]
        public object Tenant { get; set; }

        /// <summary>
        /// Date from where the schedule must start
        /// </summary>
        [JsonProperty("scheduled_at")]
        public string ScheduledAt { get; set; }

        /// <summary>
        /// Date when the schedule must stop running
        /// </summary>
        [JsonProperty("ending_at")]
        public string EndingAt { get; set; }
    }

    /// <summary>
    /// Options for bulk creating schedules
    /// </summary>
    public class BulkCreateSchedulesOptions : BaseOptions
    {
        /// <summary>
        /// The schedules to create, each with a single recipient
        /// </summary>
        [JsonProperty("schedules")]
        public List<BulkCreateScheduleOption> Schedules { get; set; }
    }
}
