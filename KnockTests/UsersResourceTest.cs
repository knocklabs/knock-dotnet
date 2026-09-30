namespace KnockTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
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

        [Fact]
        public async Task ListSubscriptions()
        {
            var subscriptions = new { entries = new[] { new { recipient = "chris", properties = new { } } } };
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/subscriptions", subscriptions);

            var response = await this.client.Users.ListSubscriptions("chris");

            this.httpMock.AssertRequestWasMade(HttpMethod.Get, "/v1/users/chris/subscriptions");
            Assert.Single(response.entries);
        }

        [Fact]
        public async Task GetPreferencesWithTenant()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/preferences/default", new { id = "default" });

            var options = new Dictionary<string, object> { { "tenant", "acme" } };
            var response = await this.client.Users.GetPreferences("chris", "default", options);

            Assert.Equal("?tenant=acme", this.httpMock.LastRequestQuery);
            Assert.Equal("default", response.Id);
        }

        [Fact]
        public async Task UnsetPreferences()
        {
            this.httpMock.MockNoContentResponse(HttpMethod.Delete, "/v1/users/chris/preferences/default");

            await this.client.Users.UnsetPreferences("chris");

            this.httpMock.AssertRequestWasMade(HttpMethod.Delete, "/v1/users/chris/preferences/default");
        }

        [Fact]
        public async Task GetFeedItems()
        {
            var feed = new { entries = new[] { new { id = "item_1" } }, meta = new { unread_count = 1 }, vars = new { } };
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/feeds/channel_1", feed);

            var triggerData = new Dictionary<string, object> { { "type", "trex" } };
            var options = new Dictionary<string, object> { { "trigger_data", triggerData } };
            var response = await this.client.Users.GetFeedItems("chris", "channel_1", options);

            Assert.Equal("?trigger_data={\"type\":\"trex\"}", this.httpMock.LastRequestQuery);
            Assert.Equal("item_1", response.entries.First().Id);
            Assert.Equal(1L, response.Meta["unread_count"]);
        }

        [Fact]
        public async Task GetFeedSettings()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/feeds/channel_1/settings", new { features = new { } });

            var response = await this.client.Users.GetFeedSettings("chris", "channel_1");

            this.httpMock.AssertRequestWasMade(HttpMethod.Get, "/v1/users/chris/feeds/channel_1/settings");
            Assert.True(response.ContainsKey("features"));
        }

        [Fact]
        public async Task GetGuides()
        {
            var guides = new { entries = new[] { new { key = "welcome" } }, guide_groups = new object[0] };
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/guides/channel_1", guides);

            var options = new Dictionary<string, object> { { "tenant", "acme" } };
            var response = await this.client.Users.GetGuides("chris", "channel_1", options);

            Assert.Equal("?tenant=acme", this.httpMock.LastRequestQuery);
            Assert.Equal("welcome", response.Entries.First()["key"]);
        }

        [Fact]
        public async Task MarkGuideMessageAsSeen()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/users/chris/guides/messages/seen", new { status = "ok" });

            var options = new GuideSeenOptions
            {
                ChannelId = "channel_1",
                GuideId = "guide_1",
                GuideKey = "welcome",
                GuideStepRef = "step_1",
                Content = new Dictionary<string, object> { { "title", "Hi" } },
            };
            var response = await this.client.Users.MarkGuideMessageAsSeen("chris", options);

            Assert.Equal("ok", response.Status);
            Assert.Equal("welcome", (string)this.httpMock.LastRequestJson["guide_key"]);
            Assert.Equal("step_1", (string)this.httpMock.LastRequestJson["guide_step_ref"]);
        }

        [Fact]
        public async Task MarkGuideMessageAsInteracted()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/users/chris/guides/messages/interacted", new { status = "ok" });

            var options = new GuideInteractedOptions { ChannelId = "channel_1", GuideId = "guide_1", GuideKey = "welcome", GuideStepRef = "step_1" };
            await this.client.Users.MarkGuideMessageAsInteracted("chris", options);

            this.httpMock.AssertRequestWasMade(HttpMethod.Put, "/v1/users/chris/guides/messages/interacted");
        }

        [Fact]
        public async Task MarkGuideMessageAsArchived()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/users/chris/guides/messages/archived", new { status = "ok" });

            var options = new GuideArchivedOptions { ChannelId = "channel_1", GuideId = "guide_1", GuideKey = "welcome", GuideStepRef = "step_1", IsFinal = true };
            await this.client.Users.MarkGuideMessageAsArchived("chris", options);

            Assert.True((bool)this.httpMock.LastRequestJson["is_final"]);
            Assert.Null(this.httpMock.LastRequestJson["unthrottled"]);
        }

        [Fact]
        public async Task UnarchiveGuideMessage()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Delete, "/v1/users/chris/guides/messages/archived", new { status = "ok" });

            await this.client.Users.UnarchiveGuideMessage("chris", new GuideUnarchivedOptions { GuideKey = "welcome" });

            Assert.Equal("welcome", (string)this.httpMock.LastRequestJson["guide_key"]);
        }

        [Fact]
        public async Task ResetGuideEngagements()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/users/chris/guides/engagements/reset", new { status = "ok" });

            await this.client.Users.ResetGuideEngagements("chris", new GuideResetOptions { GuideKey = "welcome", Tenant = "acme" });

            Assert.Equal("acme", (string)this.httpMock.LastRequestJson["tenant"]);
        }

        [Fact]
        public async Task GetPreferenceCenterConfig()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/users/chris/preference_center/config", new { enabled = true });

            var response = await this.client.Users.GetPreferenceCenterConfig("chris");

            Assert.True((bool)response["enabled"]);
        }

        [Fact]
        public async Task GeneratePreferenceCenterSignedUrl()
        {
            var signedUrl = new { url = "https://example.com/prefs?token=abc", token = "abc" };
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/users/chris/preference_center/signed_url", signedUrl);

            var response = await this.client.Users.GeneratePreferenceCenterSignedUrl("chris");

            Assert.Equal("abc", response.Token);
            Assert.Equal("https://example.com/prefs?token=abc", response.Url);
        }
    }
}
