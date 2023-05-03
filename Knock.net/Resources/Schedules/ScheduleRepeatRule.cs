using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a Knock Schedule record.
    /// </summary>
    public class ScheduleRepeatRule
    {
        /// <summary>
        /// The frequency of the rule: monthly, weekly, daily
        /// </summary>s
        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        /// <summary>
        /// The inverval of the rule
        /// </summary>
        [JsonProperty("interval")]
        public int Interval { get; set; }

        /// <summary>
        /// List of days when the repeat rules happen
        /// </summary>
        [JsonProperty("days")]
        public string[] Days { get; set; }

        /// <summary>
        /// Day of the month when the repeat rule is applied
        /// </summary>
        [JsonProperty("day_of_month")]
        public int? DayOfMonth { get; set; }

        /// <summary>
        /// Hours on which the repeat rule is applied
        /// </summary>
        [JsonProperty("hours")]
        public int Hours { get; set; }

        /// <summary>
        /// Minutes on which the repeat rule is applied
        /// </summary>
        [JsonProperty("minutes")]
        public int Minutes { get; set; }
    }
}
