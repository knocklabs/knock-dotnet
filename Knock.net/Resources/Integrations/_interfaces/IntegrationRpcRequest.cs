namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A JSON-RPC request forwarded from a reverse ETL integration
    /// </summary>
    public class IntegrationRpcRequest : BaseOptions
    {
        /// <summary>
        /// The unique identifier for the RPC request
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The JSON-RPC version
        /// </summary>
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        /// <summary>
        /// The method name to execute
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// The parameters for the method
        /// </summary>
        [JsonProperty("params")]
        public Dictionary<string, object> Params { get; set; }
    }
}
