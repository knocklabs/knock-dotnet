namespace KnockTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class SchedulesResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public SchedulesResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task ListSetsWorkflowWithoutMutatingOptions()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/schedules", new
            {
                entries = new[] { new { id = "sch_1", workflow = "daily-digest" } },
                page_info = new { page_size = 50 },
            });

            var options = new Dictionary<string, object> { { "tenant", "acme" } };
            var response = await this.client.Schedules.List("daily-digest", options);

            Assert.Equal("sch_1", response.entries.Single().Id);
            Assert.Contains("workflow=daily-digest", this.httpMock.LastRequestQuery);
            Assert.Contains("tenant=acme", this.httpMock.LastRequestQuery);
            Assert.False(options.ContainsKey("workflow"));
        }

        [Fact]
        public async Task Create()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/schedules", new[] { new { id = "sch_1" } });

            var response = await this.client.Schedules.Create(new CreateSchedules
            {
                Workflow = "daily-digest",
                Recipients = new List<object> { "user_1" },
                Repeats = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "frequency", "daily" } },
                },
                EndingAt = "2027-01-01T00:00:00Z",
            });

            Assert.Equal("sch_1", response.Single().Id);
            Assert.Equal("daily-digest", (string)this.httpMock.LastRequestJson["workflow"]);
            Assert.Contains("\"ending_at\":\"2027-01-01T00:00:00Z\"", this.httpMock.LastRequestBody);
        }

        [Fact]
        public async Task Update()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Put, "/v1/schedules", new[] { new { id = "sch_1" } });

            var response = await this.client.Schedules.Update(new UpdateSchedules
            {
                ScheduleIds = new List<string> { "sch_1" },
                Data = new Dictionary<string, object> { { "foo", "bar" } },
            });

            Assert.Equal("sch_1", response.Single().Id);
            Assert.Equal("sch_1", (string)this.httpMock.LastRequestJson["schedule_ids"][0]);
            Assert.Equal(string.Empty, this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task Delete()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Delete, "/v1/schedules", new[] { new { id = "sch_1" } });

            var response = await this.client.Schedules.Delete(new List<string> { "sch_1" });

            Assert.Equal("sch_1", response.Single().Id);
            Assert.Equal("sch_1", (string)this.httpMock.LastRequestJson["schedule_ids"][0]);
        }

        [Fact]
        public async Task BulkCreate()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Post, "/v1/schedules/bulk/create", new { id = "bulk_1" });

            var response = await this.client.Schedules.BulkCreate(new BulkCreateSchedulesOptions
            {
                Schedules = new List<BulkCreateScheduleOption>
                {
                    new BulkCreateScheduleOption { Workflow = "daily-digest", Recipient = "user_1" },
                },
            });

            Assert.Equal("bulk_1", response.Id);
            Assert.Equal("user_1", (string)this.httpMock.LastRequestJson["schedules"][0]["recipient"]);
        }

        [Fact]
        public async Task WorkflowsListSchedulesForwardsToSchedules()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/schedules", new
            {
                entries = new[] { new { id = "sch_1" } },
                page_info = new { page_size = 50 },
            });

            var response = await this.client.Workflows.ListSchedules("daily-digest");

            Assert.Equal("sch_1", response.entries.Single().Id);
            Assert.Equal("?workflow=daily-digest", this.httpMock.LastRequestQuery);
        }
    }
}
