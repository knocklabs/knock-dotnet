namespace KnockTests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Knock;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json.Linq;

    public class HttpMock
    {
        public HttpMock()
        {
            this.MockHandler = new Mock<HttpClientHandler>(MockBehavior.Strict)
            {
                CallBase = true,
            };
            this.HttpClient = new HttpClient(this.MockHandler.Object);
        }

        public Mock<HttpClientHandler> MockHandler { get; }

        public HttpClient HttpClient { get; }

        public HttpRequestMessage LastRequest { get; private set; }

        public string LastRequestBody { get; private set; }

        public string LastRequestQuery => Uri.UnescapeDataString(this.LastRequest.RequestUri.Query);

        public JObject LastRequestJson => JObject.Parse(this.LastRequestBody);

        public void AssertRequestWasMade(HttpMethod method, string path)
        {
            this.MockHandler.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Method == method &&
                        m.RequestUri.AbsolutePath == path),
                    ItExpr.IsAny<CancellationToken>());
        }

        public void AssertRequestHasHeader(string header, string value)
        {
            this.MockHandler.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Headers.GetValues(header).Contains(value)),
                    ItExpr.IsAny<CancellationToken>());
        }

        public void AssertAuthorizationBearerHeader(string value)
        {
            this.MockHandler.Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Headers.Authorization != null &&
                        m.Headers.Authorization.Scheme == "Bearer" &&
                        m.Headers.Authorization.Parameter == value),
                    ItExpr.IsAny<CancellationToken>());
        }

        public void MockResponse(
            HttpMethod method,
            string path,
            HttpStatusCode status,
            string response)
        {
            var responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(response),
                StatusCode = status,
            };

            this.MockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Method == method &&
                        m.RequestUri.AbsolutePath == path),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    this.LastRequest = request;
                    this.LastRequestBody = request.Content?.ReadAsStringAsync().Result;
                })
                .ReturnsAsync(responseMessage);
        }

        public void MockJsonResponse(HttpMethod method, string path, object response)
        {
            this.MockResponse(method, path, HttpStatusCode.OK, RequestUtilities.ToJsonString(response));
        }

        public void MockNoContentResponse(HttpMethod method, string path)
        {
            this.MockResponse(method, path, HttpStatusCode.NoContent, string.Empty);
        }

        public void MockResponseWithAuthorizationHeader(
            HttpMethod method,
            string path,
            HttpStatusCode status,
            string response,
            string bearerToken)
        {
            var responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(response),
                StatusCode = status,
            };

            this.MockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m =>
                        m.Method == method &&
                        m.RequestUri.AbsolutePath == path &&
                        m.Headers.Authorization.Scheme == "Bearer" &&
                        m.Headers.Authorization.Parameter == bearerToken),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);
        }
    }
}
