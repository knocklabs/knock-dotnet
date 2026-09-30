namespace KnockTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class ObjectsResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public ObjectsResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task UnsetPreferences()
        {
            this.httpMock.MockNoContentResponse(HttpMethod.Delete, "/v1/objects/projects/project_1/preferences/default");

            await this.client.Objects.UnsetPreferences("projects", "project_1");

            this.httpMock.AssertRequestWasMade(HttpMethod.Delete, "/v1/objects/projects/project_1/preferences/default");
        }

        [Fact]
        public async Task BulkAddSubscriptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/objects/projects/bulk/subscriptions/add", new { id = "bulk_1" });

            var subscription = new BulkAddSubscriptionsOption { Id = "project_1", Recipients = new List<object> { "chris" } };
            var options = new BulkAddSubscriptionsOptions { Subscriptions = new List<BulkAddSubscriptionsOption> { subscription } };
            var response = await this.client.Objects.BulkAddSubscriptions("projects", options);

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("project_1", (string)this.httpMock.LastRequestJson["subscriptions"][0]["id"]);
        }

        [Fact]
        public async Task BulkDeleteSubscriptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/objects/projects/bulk/subscriptions/delete", new { id = "bulk_1" });

            var subscription = new BulkDeleteSubscriptionsOption { Id = "project_1", Recipients = new List<object> { "chris" } };
            var options = new BulkDeleteSubscriptionsOptions { Subscriptions = new List<BulkDeleteSubscriptionsOption> { subscription } };
            var response = await this.client.Objects.BulkDeleteSubscriptions("projects", options);

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("chris", (string)this.httpMock.LastRequestJson["subscriptions"][0]["recipients"][0]);
        }
    }
}
