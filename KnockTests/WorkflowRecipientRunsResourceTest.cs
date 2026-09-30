namespace KnockTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Knock;
    using Xunit;

    public class WorkflowRecipientRunsResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly KnockClient client;

        public WorkflowRecipientRunsResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });
        }

        [Fact]
        public async Task List()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/workflow_recipient_runs", new
            {
                entries = new[] { new { id = "wrr_1", workflow = "welcome", status = "completed" } },
                page_info = new { page_size = 50 },
            });

            var options = new Dictionary<string, object> { { "workflow", "welcome" } };
            var response = await this.client.WorkflowRecipientRuns.List(options);

            var run = response.entries.Single();
            Assert.Equal("wrr_1", run.Id);
            Assert.Equal("completed", run.Status);
            Assert.Equal("?workflow=welcome", this.httpMock.LastRequestQuery);
        }

        [Fact]
        public async Task Get()
        {
            this.httpMock.MockJsonResponse(HttpMethod.Get, "/v1/workflow_recipient_runs/wrr_1", new
            {
                id = "wrr_1",
                error_count = 0,
                events = new[] { new { type = "workflow_recipient_run.started" } },
            });

            var response = await this.client.WorkflowRecipientRuns.Get("wrr_1");

            Assert.Equal("wrr_1", response.Id);
            Assert.Equal(0, response.ErrorCount);
            Assert.Equal("workflow_recipient_run.started", response.Events.Single()["type"]);
        }
    }
}
