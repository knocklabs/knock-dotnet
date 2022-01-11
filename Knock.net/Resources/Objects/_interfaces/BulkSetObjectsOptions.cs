namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for bulk setting objects
    /// </summary>
    public class BulkSetObjectsOptions : BaseOptions
    {
        /// <summary>
        /// The list of objects to set
        /// </summary>
        [JsonProperty("objects")]
        public List<Dictionary<string, object>> Objects { get; set; }
    }
}
