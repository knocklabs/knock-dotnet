namespace KnockTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class TenantsResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public TenantsResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task GetWithOptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/tenants/acme", new { id = "acme", name = "Acme" });

            var options = new Dictionary<string, object> { { "resolve_full_preference_settings", true } };
            var response = await this.client.Tenants.Get("acme", options);

            Assert.Equal("?resolve_full_preference_settings=true", this.httpMock.LastRequestQuery);
            Assert.Equal("Acme", response.Name);
        }

        [Fact]
        public async Task SetWithQueryOptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/tenants/acme", new { id = "acme", name = "Acme" });

            var tenantData = new Dictionary<string, object> { { "name", "Acme" } };
            var queryOptions = new Dictionary<string, object> { { "resolve_full_preference_settings", true } };
            await this.client.Tenants.Set("acme", tenantData, queryOptions);

            Assert.Equal("?resolve_full_preference_settings=true", this.httpMock.LastRequestQuery);
            Assert.Equal("Acme", (string)this.httpMock.LastRequestJson["name"]);
            Assert.Null(this.httpMock.LastRequestJson["resolve_full_preference_settings"]);
        }

        [Fact]
        public async Task BulkSet()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/tenants/bulk/set", new { id = "bulk_1" });

            var tenant = new Dictionary<string, object> { { "id", "acme" }, { "name", "Acme" } };
            var response = await this.client.Tenants.BulkSet(new BulkSetTenantsOptions { Tenants = new List<object> { "globex", tenant } });

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("globex", (string)this.httpMock.LastRequestJson["tenants"][0]);
            Assert.Equal("Acme", (string)this.httpMock.LastRequestJson["tenants"][1]["name"]);
        }

        [Fact]
        public async Task BulkDeleteSendsTenantIdsInQuery()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/tenants/bulk/delete", new { id = "bulk_1" });

            var response = await this.client.Tenants.BulkDelete(new List<string> { "acme", "globex" });

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("?tenant_ids[]=acme&tenant_ids[]=globex", this.httpMock.LastRequestQuery);
        }
    }
}
