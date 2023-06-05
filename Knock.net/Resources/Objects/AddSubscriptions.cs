namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params to add subscriptions to object
    /// </summary>
    public class AddSubscriptions : BaseOptions
    {
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }

        /// <summary>
        /// A dictionary of data to pass through
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }
    }
}
