using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Tenants
    /// </summary>
    public class TenantsResource : BaseResource
    {
        /// <summary>
        /// Ctor for tenant methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public TenantsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a paginated list of tenants
        /// </summary>
        /// <param name="options">Options filtering and pagination</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A paginated Tenant response.</returns>
        public async Task<PaginatedResponse<Tenant>> List(Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/tenants",
                Method = HttpMethod.Get,
                Options=options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Tenant>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns an tenant
        /// </summary>
        /// <param name="tenantId">Tenant unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Tenant record.</returns>
        public async Task<Tenant> Get(string tenantId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/tenants/{tenantId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Tenant>(request, cancellationToken);
        }

        /// <summary>
        /// Sets an tenant
        /// </summary>
        /// <param name="tenantId">Unique identifier.</param>
        /// <param name="tenantData">Dictionary of params</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Tenant record.</returns>
        public async Task<Tenant> Set(string tenantId, Dictionary<string, object> tenantData, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/tenants/{tenantId}",
                Method = HttpMethod.Put,
                Options = tenantData,
            };

            return await Client.MakeAPIRequest<Tenant>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes an tenant
        /// </summary>
        /// <param name="tenantId">Unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Tenant record.</returns>
        public async Task<Tenant> Delete(string tenantId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/tenants/{tenantId}",
                Method = HttpMethod.Delete,
            };

            return await Client.MakeAPIRequest<Tenant>(request, cancellationToken);
        }
    }
}
