namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params to trigger a workflow run
    /// </summary>
    public class UpdateSchedules : BaseOptions
    {

        /// <summary>
        /// The repeat rules for the schedule
        /// </summary>
        [JsonProperty("schedule_ids")]
        public List<string> ScheduleIds { get; set; }

        /// <summary>
        /// The repeat rules for the schedule
        /// </summary>
        [JsonProperty("repeats")]
        public List<Dictionary<string, object>> Repeats { get; set; }

        /// <summary>
        /// An optional reference to the actor executing the triggerd workflow. This
        /// can be A) a user id, B) an object reference, or C) a dictionary with
        /// data to identify a user or object.
        /// </summary>
        [JsonProperty("actor")]
        public object Actor { get; set; }

        /// <summary>
        /// A dictionary of data to pass through
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }


        /// <summary>
        /// An optional reference to the tenant related to the schedule. This
        /// can be A) a tenant id, B) an object reference without the collection, or 
        /// C) a dictionary with data to identify a tenant.
        /// </summary>
        [JsonProperty("tenant")]
        public object Tenant { get; set; }

        /// <summary>
        /// Date from where the schedule must start
        /// </summary>
        [JsonProperty("scheduled_at")]
        public string ScheduledAt { get; set; }
    }
}
