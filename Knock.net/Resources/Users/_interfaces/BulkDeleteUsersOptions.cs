namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk deleting objects
    /// </summary>
    public class BulkDeleteUsersOptions : BaseOptions
    {
        /// <summary>
        /// The list of user ids to delete
        /// </summary>
        [JsonProperty("user_ids")]
        public List<string> UserIds { get; set; }
    }
}
