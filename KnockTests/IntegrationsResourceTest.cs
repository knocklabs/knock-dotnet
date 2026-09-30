namespace KnockTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class IntegrationsResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public IntegrationsResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task CensusCustomDestination()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/integrations/census/custom-destination", new { id = "1", result = new { } });

            var response = await this.client.Integrations.CensusCustomDestination(BuildRpcRequest());

            Assert.Equal("1", response["id"]);
            Assert.Equal("2.0", (string)this.httpMock.LastRequestJson["jsonrpc"]);
            Assert.Equal("test_connection", (string)this.httpMock.LastRequestJson["method"]);
        }

        [Fact]
        public async Task HightouchEmbeddedDestination()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/integrations/hightouch/embedded-destination", new { id = "1", result = new { } });

            var response = await this.client.Integrations.HightouchEmbeddedDestination(BuildRpcRequest());

            Assert.Equal("1", response["id"]);
            Assert.Equal("bar", (string)this.httpMock.LastRequestJson["params"]["foo"]);
        }

        private static IntegrationRpcRequest BuildRpcRequest()
        {
            return new IntegrationRpcRequest
            {
                Id = "1",
                Jsonrpc = "2.0",
                Method = "test_connection",
                Params = new Dictionary<string, object> { { "foo", "bar" } },
            };
        }
    }
}
