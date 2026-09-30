namespace KnockTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class MessagesResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;
        private readonly List<string> messageIds;

        public MessagesResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });

            this.messageIds = new List<string> { "msg_1", "msg_2" };
        }

        [Fact]
        public async Task ListWithTriggerData()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/messages", new { items = new[] { new { id = "msg_1" } } });

            var triggerData = new Dictionary<string, object> { { "type", "trex" } };
            var response = await this.client.Messages.List(new Dictionary<string, object> { { "trigger_data", triggerData } });

            Assert.Equal("?trigger_data={\"type\":\"trex\"}", this.httpMock.LastRequestQuery);
            Assert.Equal("msg_1", response.items.First().Id);
        }

        [Fact]
        public async Task GetDeliveryLogs()
        {
            var logs = new { items = new[] { new { id = "log_1", service_name = "sendgrid" } } };
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/messages/msg_1/delivery_logs", logs);

            var response = await this.client.Messages.GetDeliveryLogs("msg_1");

            Assert.Equal("sendgrid", response.items.First().ServiceName);
        }

        [Fact]
        public async Task MarkAsSeen()
        {
            await this.AssertStatusChange(() => this.client.Messages.MarkAsSeen("msg_1"), HttpMethod.Put, "/v1/messages/msg_1/seen");
        }

        [Fact]
        public async Task MarkAsUnseen()
        {
            await this.AssertStatusChange(() => this.client.Messages.MarkAsUnseen("msg_1"), HttpMethod.Delete, "/v1/messages/msg_1/seen");
        }

        [Fact]
        public async Task MarkAsRead()
        {
            await this.AssertStatusChange(() => this.client.Messages.MarkAsRead("msg_1"), HttpMethod.Put, "/v1/messages/msg_1/read");
        }

        [Fact]
        public async Task MarkAsUnread()
        {
            await this.AssertStatusChange(() => this.client.Messages.MarkAsUnread("msg_1"), HttpMethod.Delete, "/v1/messages/msg_1/read");
        }

        [Fact]
        public async Task Archive()
        {
            await this.AssertStatusChange(() => this.client.Messages.Archive("msg_1"), HttpMethod.Put, "/v1/messages/msg_1/archived");
        }

        [Fact]
        public async Task Unarchive()
        {
            await this.AssertStatusChange(() => this.client.Messages.Unarchive("msg_1"), HttpMethod.Delete, "/v1/messages/msg_1/archived");
        }

        [Fact]
        public async Task MarkAsInteractedWithMetadata()
        {
            var metadata = new Dictionary<string, object> { { "action", "clicked" } };
            await this.AssertStatusChange(() => this.client.Messages.MarkAsInteracted("msg_1", metadata), HttpMethod.Put, "/v1/messages/msg_1/interacted");

            Assert.Equal("clicked", (string)this.httpMock.LastRequestJson["metadata"]["action"]);
        }

        [Fact]
        public async Task MarkAsInteractedWithoutMetadata()
        {
            await this.AssertStatusChange(() => this.client.Messages.MarkAsInteracted("msg_1"), HttpMethod.Put, "/v1/messages/msg_1/interacted");

            Assert.Null(this.httpMock.LastRequestJson["metadata"]);
        }

        [Fact]
        public async Task BatchMarkAsSeen()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchMarkAsSeen(this.messageIds), "seen");
        }

        [Fact]
        public async Task BatchMarkAsUnseen()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchMarkAsUnseen(this.messageIds), "unseen");
        }

        [Fact]
        public async Task BatchMarkAsRead()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchMarkAsRead(this.messageIds), "read");
        }

        [Fact]
        public async Task BatchMarkAsUnread()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchMarkAsUnread(this.messageIds), "unread");
        }

        [Fact]
        public async Task BatchArchive()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchArchive(this.messageIds), "archived");
        }

        [Fact]
        public async Task BatchUnarchive()
        {
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchUnarchive(this.messageIds), "unarchived");
        }

        [Fact]
        public async Task BatchMarkAsInteracted()
        {
            var metadata = new Dictionary<string, object> { { "action", "clicked" } };
            await this.AssertBatchStatusChange(() => this.client.Messages.BatchMarkAsInteracted(this.messageIds, metadata), "interacted");

            Assert.Equal("clicked", (string)this.httpMock.LastRequestJson["metadata"]["action"]);
        }

        [Fact]
        public async Task BatchGetContent()
        {
            var contents = new[] { new { id = "msg_1" }, new { id = "msg_2" } };
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/messages/batch/content", contents);

            var response = await this.client.Messages.BatchGetContent(this.messageIds);

            Assert.Equal("?message_ids[]=msg_1&message_ids[]=msg_2", this.httpMock.LastRequestQuery);
            Assert.Equal(2, response.Count);
        }

        private async Task AssertStatusChange(Func<Task<Message>> call, HttpMethod method, string path)
        {
            this.httpMock.MockJsonResponse(method, path, new { id = "msg_1" });

            var response = await call();

            this.httpMock.AssertRequestWasMade(method, path);
            Assert.Equal("msg_1", response.Id);
        }

        private async Task AssertBatchStatusChange(Func<Task<List<Message>>> call, string status)
        {
            var path = $"/v1/messages/batch/{status}";
            this.httpMock.MockJsonResponse(HttpMethod.Post, path, new[] { new { id = "msg_1" }, new { id = "msg_2" } });

            var response = await call();

            this.httpMock.AssertRequestWasMade(HttpMethod.Post, path);
            Assert.Equal(this.messageIds, this.httpMock.LastRequestJson["message_ids"].ToObject<List<string>>());
            Assert.Equal(2, response.Count);
        }
    }
}
