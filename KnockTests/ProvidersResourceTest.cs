namespace KnockTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class ProvidersResourceTest
    {
        private const string AccessTokenObject = "{\"object_id\":\"user_1\",\"collection\":\"$users\"}";

        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public ProvidersResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task CheckSlackAuth()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/providers/slack/chan_1/auth_check", new { connection = new { ok = true } });

            var response = await this.client.Providers.CheckSlackAuth("chan_1", AccessTokenObject);

            Assert.NotNull(response["connection"]);
            Assert.Equal($"?access_token_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task ListSlackChannelsWithOptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/providers/slack/chan_1/channels", new { slack_channels = new object[0], next_cursor = (string)null });

            var options = new Dictionary<string, object> { { "query_options", new Dictionary<string, object> { { "limit", 10 } } } };
            await this.client.Providers.ListSlackChannels("chan_1", AccessTokenObject, options);

            Assert.Contains($"access_token_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
            Assert.Contains("query_options[limit]=10", this.httpMock.LastRequestQuery);
            Assert.False(options.ContainsKey("access_token_object"));
        }

        [Fact]
        public async Task RevokeSlackAccessSendsQueryOnPut()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/providers/slack/chan_1/revoke_access", new { ok = "ok" });

            await this.client.Providers.RevokeSlackAccess("chan_1", AccessTokenObject);

            Assert.Equal(HttpMethod.Put, this.httpMock.LastRequest.Method);
            Assert.Equal($"?access_token_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task CheckMsTeamsAuth()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/providers/ms-teams/chan_1/auth_check", new { connection = new { ok = true } });

            await this.client.Providers.CheckMsTeamsAuth("chan_1", AccessTokenObject);

            Assert.Equal($"?ms_teams_tenant_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task ListMsTeamsTeams()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/providers/ms-teams/chan_1/teams", new { ms_teams_teams = new object[0] });

            await this.client.Providers.ListMsTeamsTeams("chan_1", AccessTokenObject);

            Assert.Equal($"?ms_teams_tenant_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task ListMsTeamsChannelsSetsTeamId()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/providers/ms-teams/chan_1/channels", new { ms_teams_channels = new object[0] });

            await this.client.Providers.ListMsTeamsChannels("chan_1", AccessTokenObject, "team_1");

            Assert.Contains($"ms_teams_tenant_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
            Assert.Contains("team_id=team_1", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task RevokeMsTeamsAccessSendsQueryOnPut()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/providers/ms-teams/chan_1/revoke_access", new { ok = "ok" });

            await this.client.Providers.RevokeMsTeamsAccess("chan_1", AccessTokenObject);

            Assert.Equal($"?ms_teams_tenant_object={AccessTokenObject}", this.httpMock.LastRequestQuery);
        }
    }
}
