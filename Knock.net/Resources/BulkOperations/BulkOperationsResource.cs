using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Objects
    /// </summary>
    public class BulkOperationsResource : BaseResource
    {
        /// <summary>
        /// Ctor for object methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public BulkOperationsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a BulkOperation.
        /// </summary>
        /// <param name="bulkOperationId">Bulk operation unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation.</returns>
        public async Task<BulkOperation> Get(string bulkOperationId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/bulk_operations/{bulkOperationId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }
    }
}