using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Audiences
    /// </summary>
    public class AudiencesResource : BaseResource
    {
        /// <summary>
        /// Ctor for audience methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public AudiencesResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a paginated list of the members of an audience
        /// </summary>
        /// <param name="audienceKey">The key of the audience</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated AudienceMember response.</returns>
        public async Task<PaginatedResponse<AudienceMember>> ListMembers(string audienceKey, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/audiences/{audienceKey}/members",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<AudienceMember>>(request, cancellationToken);
        }

        /// <summary>
        /// Adds members to an audience
        /// </summary>
        /// <param name="audienceKey">The key of the audience</param>
        /// <param name="options">The members to add</param>
        /// <param name="createAudience">Whether to create the audience if it does not exist</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>No response.</returns>
        public async Task AddMembers(string audienceKey, AddAudienceMembersOptions options, bool createAudience = false, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/audiences/{audienceKey}/members",
                Method = HttpMethod.Post,
                Options = options,
            };

            if (createAudience)
            {
                request.QueryParams = new Dictionary<string, object> { { "create_audience", true } };
            }

            await Client.MakeAPIRequest(request, cancellationToken);
        }

        /// <summary>
        /// Removes members from an audience
        /// </summary>
        /// <param name="audienceKey">The key of the audience</param>
        /// <param name="options">The members to remove</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>No response.</returns>
        public async Task RemoveMembers(string audienceKey, RemoveAudienceMembersOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/audiences/{audienceKey}/members",
                Method = HttpMethod.Delete,
                Options = options,
            };

            await Client.MakeAPIRequest(request, cancellationToken);
        }
    }
}
