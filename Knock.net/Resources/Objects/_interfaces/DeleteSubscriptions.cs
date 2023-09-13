namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Params to delete subscriptions
    /// </summary>
    public class DeleteSubscriptions : BaseOptions
    {
        [JsonProperty("recipients")]
        public List<object> Recipients { get; set; }
    }
}
