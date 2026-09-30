using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    using Response = Dictionary<string, object>;

    /// <summary>
    /// Methods for working with reverse ETL integrations (Census, Hightouch)
    /// </summary>
    public class IntegrationsResource : BaseResource
    {
        /// <summary>
        /// Ctor for integration methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public IntegrationsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Processes a Census custom destination RPC request
        /// </summary>
        /// <param name="rpcRequest">The JSON-RPC request</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A JSON-RPC response dictionary.</returns>
        public async Task<Response> CensusCustomDestination(IntegrationRpcRequest rpcRequest, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/integrations/census/custom-destination",
                Method = HttpMethod.Post,
                Options = rpcRequest,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Processes a Hightouch embedded destination RPC request
        /// </summary>
        /// <param name="rpcRequest">The JSON-RPC request</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A JSON-RPC response dictionary.</returns>
        public async Task<Response> HightouchEmbeddedDestination(IntegrationRpcRequest rpcRequest, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/integrations/hightouch/embedded-destination",
                Method = HttpMethod.Post,
                Options = rpcRequest,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }
    }
}
