namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk deleting objects
    /// </summary>
    public class BulkDeleteObjectsOptions : BaseOptions
    {
        /// <summary>
        /// The list of objects ids to delete
        /// </summary>
        [JsonProperty("object_ids")]
        public List<string> ObjectIds { get; set; }
    }
}
