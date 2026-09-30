namespace KnockTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class AudiencesResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public AudiencesResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task ListMembers()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/audiences/beta/members", new
            {
                entries = new[] { new { user_id = "user_1", tenant = "acme" } },
                page_info = new { page_size = 50 },
            });

            var response = await this.client.Audiences.ListMembers("beta");

            var member = response.entries.Single();
            Assert.Equal("user_1", member.UserId);
            Assert.Equal("acme", member.Tenant);
        }

        [Fact]
        public async Task AddMembers()
        {
            this.httpMock.MockNoContentResponse(HttpMethod.Post, "/v1/audiences/beta/members");

            await this.client.Audiences.AddMembers("beta", BuildMembers<AddAudienceMembersOptions>());

            Assert.Equal(string.Empty, this.httpMock.LastRequestQuery);
            Assert.Equal("user_1", (string)this.httpMock.LastRequestJson["members"][0]["user"]["id"]);
            Assert.Equal("acme", (string)this.httpMock.LastRequestJson["members"][0]["tenant"]);
        }

        [Fact]
        public async Task AddMembersCreatingAudience()
        {
            this.httpMock.MockNoContentResponse(HttpMethod.Post, "/v1/audiences/beta/members");

            await this.client.Audiences.AddMembers("beta", BuildMembers<AddAudienceMembersOptions>(), createAudience: true);

            Assert.Equal("?create_audience=true", this.httpMock.LastRequestQuery);
            Assert.Null(this.httpMock.LastRequestJson["create_audience"]);
        }

        [Fact]
        public async Task RemoveMembers()
        {
            this.httpMock.MockNoContentResponse(HttpMethod.Delete, "/v1/audiences/beta/members");

            await this.client.Audiences.RemoveMembers("beta", BuildMembers<RemoveAudienceMembersOptions>());

            Assert.Equal(HttpMethod.Delete, this.httpMock.LastRequest.Method);
            Assert.Equal("user_1", (string)this.httpMock.LastRequestJson["members"][0]["user"]["id"]);
        }

        private static T BuildMembers<T>()
            where T : AudienceMembersOptions, new()
        {
            return new T
            {
                Members = new List<AudienceMemberOption>
                {
                    new AudienceMemberOption
                    {
                        User = new Dictionary<string, object> { { "id", "user_1" } },
                        Tenant = "acme",
                    },
                },
            };
        }
    }
}
