namespace KnockTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class ChannelsResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public ChannelsResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task BulkUpdateMessageStatus()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/channels/chan_1/messages/bulk/archive", new { id = "bulk_1" });

            var response = await this.client.Channels.BulkUpdateMessageStatus("chan_1", "archive", new BulkUpdateChannelMessagesOptions
            {
                RecipientIds = new List<string> { "user_1" },
                HasTenant = true,
            });

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("user_1", (string)this.httpMock.LastRequestJson["recipient_ids"][0]);
            Assert.True((bool)this.httpMock.LastRequestJson["has_tenant"]);
            Assert.Null(this.httpMock.LastRequestJson["workflows"]);
        }

        [Fact]
        public async Task BulkUpdateMessageStatusWithoutFilters()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/channels/chan_1/messages/bulk/seen", new { id = "bulk_1" });

            var response = await this.client.Channels.BulkUpdateMessageStatus("chan_1", "seen");

            Assert.Equal("bulk_1", response.Id);
        }
    }
}
