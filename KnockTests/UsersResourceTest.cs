namespace KnockTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Knock;
    using Newtonsoft.Json;
    using Xunit;

    public class UsersResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly Dictionary<string, object> mockUser;
        private readonly KnockClient client;

        public UsersResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });

            this.mockUser = new Dictionary<string, object>
            {
                { "id", "chris" },
                { "email", "chris@knock.app" },
                { "name", "Chris Bell" },
            };
        }

        [Fact]
        public async void GetUser()
        {
            this.httpMock.MockResponse(
                HttpMethod.Get,
                $"/v1/users/{this.mockUser["id"]}",
                HttpStatusCode.OK,
                RequestUtilities.ToJsonString(this.mockUser));

            var response = await this.client.Users.Get(this.mockUser["id"] as string);

            this.httpMock.AssertRequestWasMade(
                HttpMethod.Get,
                $"/v1/users/{this.mockUser["id"]}");

            Assert.Equal(
                JsonConvert.SerializeObject(this.mockUser),
                JsonConvert.SerializeObject(response));
        }
    }
}
