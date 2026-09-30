using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    using Response = Dictionary<string, object>;

    /// <summary>
    /// Methods for working with chat provider (Slack, Microsoft Teams) integrations
    /// </summary>
    public class ProvidersResource : BaseResource
    {
        /// <summary>
        /// Ctor for provider methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public ProvidersResource(KnockClient client) : base(client) { }

        #region Slack

        /// <summary>
        /// Checks whether a Slack access token is valid
        /// </summary>
        /// <param name="channelId">The id of the Knock Slack channel</param>
        /// <param name="accessTokenObject">A JSON encoded reference to the recipient holding the access token</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary with the `connection` status.</returns>
        public async Task<Response> CheckSlackAuth(string channelId, string accessTokenObject, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/slack/{channelId}/auth_check",
                Method = HttpMethod.Get,
                Options = WithParam(null, "access_token_object", accessTokenObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Lists the channels in a Slack workspace
        /// </summary>
        /// <param name="channelId">The id of the Knock Slack channel</param>
        /// <param name="accessTokenObject">A JSON encoded reference to the recipient holding the access token</param>
        /// <param name="options">Query options, e.g. `query_options.cursor` or `query_options.limit`</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary with the `slack_channels` and `next_cursor`.</returns>
        public async Task<Response> ListSlackChannels(string channelId, string accessTokenObject, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/slack/{channelId}/channels",
                Method = HttpMethod.Get,
                Options = WithParam(options, "access_token_object", accessTokenObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Revokes Knock's access to a Slack workspace
        /// </summary>
        /// <param name="channelId">The id of the Knock Slack channel</param>
        /// <param name="accessTokenObject">A JSON encoded reference to the recipient holding the access token</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary.</returns>
        public async Task<Response> RevokeSlackAccess(string channelId, string accessTokenObject, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/slack/{channelId}/revoke_access",
                Method = HttpMethod.Put,
                QueryParams = WithParam(null, "access_token_object", accessTokenObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        #endregion

        #region MsTeams

        /// <summary>
        /// Checks whether a Microsoft Teams connection is valid
        /// </summary>
        /// <param name="channelId">The id of the Knock Microsoft Teams channel</param>
        /// <param name="msTeamsTenantObject">A JSON encoded reference to the recipient holding the Microsoft Teams tenant id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary with the `connection` status.</returns>
        public async Task<Response> CheckMsTeamsAuth(string channelId, string msTeamsTenantObject, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/ms-teams/{channelId}/auth_check",
                Method = HttpMethod.Get,
                Options = WithParam(null, "ms_teams_tenant_object", msTeamsTenantObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Lists the teams in a Microsoft Entra tenant
        /// </summary>
        /// <param name="channelId">The id of the Knock Microsoft Teams channel</param>
        /// <param name="msTeamsTenantObject">A JSON encoded reference to the recipient holding the Microsoft Teams tenant id</param>
        /// <param name="options">Query options, e.g. `query_options.$top` or `query_options.$skiptoken`</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary with the `ms_teams_teams` and `skip_token`.</returns>
        public async Task<Response> ListMsTeamsTeams(string channelId, string msTeamsTenantObject, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/ms-teams/{channelId}/teams",
                Method = HttpMethod.Get,
                Options = WithParam(options, "ms_teams_tenant_object", msTeamsTenantObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Lists the channels in a Microsoft Teams team
        /// </summary>
        /// <param name="channelId">The id of the Knock Microsoft Teams channel</param>
        /// <param name="msTeamsTenantObject">A JSON encoded reference to the recipient holding the Microsoft Teams tenant id</param>
        /// <param name="teamId">The id of the Microsoft Teams team</param>
        /// <param name="options">Query options, e.g. `query_options.$filter`</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary with the `ms_teams_channels`.</returns>
        public async Task<Response> ListMsTeamsChannels(string channelId, string msTeamsTenantObject, string teamId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var query = WithParam(options, "ms_teams_tenant_object", msTeamsTenantObject);
            query["team_id"] = teamId;

            var request = new KnockRequest
            {
                Path = $"/providers/ms-teams/{channelId}/channels",
                Method = HttpMethod.Get,
                Options = query,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Revokes Knock's access to a Microsoft Entra tenant
        /// </summary>
        /// <param name="channelId">The id of the Knock Microsoft Teams channel</param>
        /// <param name="msTeamsTenantObject">A JSON encoded reference to the recipient holding the Microsoft Teams tenant id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary.</returns>
        public async Task<Response> RevokeMsTeamsAccess(string channelId, string msTeamsTenantObject, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/providers/ms-teams/{channelId}/revoke_access",
                Method = HttpMethod.Put,
                QueryParams = WithParam(null, "ms_teams_tenant_object", msTeamsTenantObject),
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        #endregion

        private static Dictionary<string, object> WithParam(Dictionary<string, object> options, string key, object value)
        {
            var query = options == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(options);
            query[key] = value;
            return query;
        }
    }
}
