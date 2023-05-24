using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock Schedule record.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// The id of the schedule
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cursor of the schedule, used for pagination
        /// </summary>
        [JsonProperty("__cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// The key of the workflow the schedule is for
        /// </summary>
        [JsonProperty("workflow")]
        public string Workflow { get; set; }

        /// <summary>
        /// The recipient of the schedule
        /// </summary>
        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        /// <summary>
        /// The repeat rules for the schedule
        /// </summary>
        [JsonProperty("repeats")]
        public ScheduleRepeatRule[] Repeats { get; set; }

        /// <summary>
        /// The actor of the schedule
        /// </summary>
        [JsonProperty("actor")]
        public object Actor { get; set; }

        /// <summary>
        /// The id of the tenant the schedule belongs to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// The datetime where the last occurrence of the schedule happened
        /// </summary>
        [JsonProperty("last_occurrence_at")]
        public string LastOccurrenceAt { get; set; }

        /// <summary>
        /// The datetime where the next occurrence of the schedule is happening
        /// </summary>
        [JsonProperty("next_occurrence_at")]
        public string NextOccurrenceAt { get; set; }

        /// <summary>
        /// The date this schedule was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string InsertedAt { get; set; }

        /// <summary>
        /// The date this schedule was updated at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// Data used when triggering the schedule's workflow
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
