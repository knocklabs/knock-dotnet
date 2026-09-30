namespace Knock
{
    using Newtonsoft.Json;

    /// <summary>
    /// The result of recording an engagement with a guide.
    /// </summary>
    public class GuideActionResponse
    {
        /// <summary>
        /// The status of the action
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
