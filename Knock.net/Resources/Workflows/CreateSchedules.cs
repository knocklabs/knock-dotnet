namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params to trigger a workflow run
    /// </summary>
    public class CreateSchedules : BaseOptions
    {
        /// <summary>
        /// A list of recipients to create schedules for. This can be a list
        /// of A) user ids, B) object references, C) dictionaries with data to
        /// identify a user or object, or D) a combination thereof.
        /// </summary>
        [JsonProperty("workflow")]
        public string Workflow { get; set; }

        /// <summary>
        /// A list of recipients to create schedules for. This can be a list
        /// of A) user ids, B) object references, C) dictionaries with data to
        /// identify a user or object, or D) a combination thereof.
        /// </summary>
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }

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
        /// An optional tenant identifier
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// Date from where the schedule must start
        /// </summary>
        [JsonProperty("scheduled_at")]
        public string ScheduledAt { get; set; }
    }
}
