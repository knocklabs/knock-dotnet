namespace KnockTests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class KnockClientTest
    {
        [Fact]
        public void TestEmptyAPIKey()
        {
            Assert.Throws<ArgumentException>(
                () => new KnockClient(new KnockOptions { }));
        }

        [Fact]
        public async Task TestQueryParamsAreSentOnPost()
        {
            var httpMock = new HttpMock();
            var client = new KnockClient(new KnockOptions { ApiKey = "sk_12345", HttpClient = httpMock.HttpClient });
            httpMock.MockJsonResponse(HttpMethod.Post, "/v1/some/path", new { });

            await client.MakeAPIRequest(new KnockRequest
            {
                Path = "/some/path",
                Method = HttpMethod.Post,
                Options = new Dictionary<string, object> { { "body_key", "body_value" } },
                QueryParams = new Dictionary<string, object> { { "query_key", "query_value" } },
            });

            Assert.Equal("?query_key=query_value", httpMock.LastRequestQuery);
            Assert.Equal("body_value", (string)httpMock.LastRequestJson["body_key"]);
        }

        [Fact]
        public async Task TestPutBodyIsNotSentInQueryString()
        {
            var httpMock = new HttpMock();
            var client = new KnockClient(new KnockOptions { ApiKey = "sk_12345", HttpClient = httpMock.HttpClient });
            httpMock.MockJsonResponse(HttpMethod.Put, "/v1/users/jhammond", new { id = "jhammond" });

            await client.Users.Identify("jhammond", new Dictionary<string, object> { { "email", "jhammond@ingen.net" } });

            Assert.Equal(string.Empty, httpMock.LastRequestQuery);
            Assert.Equal("jhammond@ingen.net", (string)httpMock.LastRequestJson["email"]);
        }

        [Fact]
        public async Task TestGetOptionsAreSentInQueryString()
        {
            var httpMock = new HttpMock();
            var client = new KnockClient(new KnockOptions { ApiKey = "sk_12345", HttpClient = httpMock.HttpClient });
            httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users", new { entries = new object[0] });

            await client.Users.List(new Dictionary<string, object> { { "page_size", 10 } });

            Assert.Equal("?page_size=10", httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task TestNoContentResponse()
        {
            var httpMock = new HttpMock();
            var client = new KnockClient(new KnockOptions { ApiKey = "sk_12345", HttpClient = httpMock.HttpClient });
            httpMock.MockNoContentResponse(HttpMethod.Delete, "/v1/some/path");

            await client.MakeAPIRequest(new KnockRequest { Path = "/some/path", Method = HttpMethod.Delete });

            httpMock.AssertRequestWasMade(HttpMethod.Delete, "/v1/some/path");
        }
    }
}
