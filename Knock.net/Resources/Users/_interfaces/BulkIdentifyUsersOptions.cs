namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk identifying users
    /// </summary>
    public class BulkIdentifyUsersOptions : BaseOptions
    {
        /// <summary>
        /// The list of user dictionaries to identify
        /// </summary>
        [JsonProperty("users")]
        public List<Dictionary<string, object>> Users { get; set; }
    }
}
