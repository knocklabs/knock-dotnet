namespace KnockTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Knock;
    using Newtonsoft.Json;
    using Xunit;

    public class WorkflowResourceTest
    {
        private readonly HttpMock httpMock;
        private readonly Dictionary<string, object> mockWorkflow;
        private readonly TriggerWorkflow mockTriggerWorkflow;
        private readonly MethodOptions mockIdempotentOptions;
        private readonly Dictionary<string, object> mockWorkflowResponse;
        private readonly KnockClient client;

        public WorkflowResourceTest()
        {
            this.httpMock = new HttpMock();

            this.client = new KnockClient(new KnockOptions
            {
                ApiKey = "sk_12345",
                HttpClient = this.httpMock.HttpClient,
            });

            this.mockWorkflow = new Dictionary<string, object>
            {
                { "key", "some-workflow" },
            };

            this.mockTriggerWorkflow = new TriggerWorkflow
            {
                Recipients = new List<object> { "user_12345" },
                Actor = "user_12345",
                Data = new Dictionary<string, object> { { "foo", "bar" } },
                Tenant = "tenant_12345",
                CancellationKey = "some-cancellation-key",
            };

            this.mockIdempotentOptions = new MethodOptions
            {
                IdempotencyKey = "some-idempotency-key",
            };

            this.mockWorkflowResponse = new Dictionary<string, object>
            {
                { "workflow_run_id", "e6cb93f4-dcc3-44a6-b95c-2ad88ba37db2" },
            };
        }

        [Fact]
        public async void TriggerWorkflow()
        {
            this.httpMock.MockResponse(
                HttpMethod.Post,
                $"/v1/workflows/{this.mockWorkflow["key"]}/trigger",
                HttpStatusCode.OK,
                RequestUtilities.ToJsonString(this.mockWorkflowResponse));

            var response = await this.client.Workflows.Trigger(this.mockWorkflow["key"] as string, this.mockTriggerWorkflow);

            this.httpMock.AssertRequestWasMade(
                HttpMethod.Post,
                $"/v1/workflows/{this.mockWorkflow["key"]}/trigger");

            Assert.Equal(
                JsonConvert.SerializeObject(this.mockWorkflowResponse),
                JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async void TriggerIdempotentWorkflow()
        {
            this.httpMock.MockResponse(
                HttpMethod.Post,
                $"/v1/workflows/{this.mockWorkflow["key"]}/trigger",
                HttpStatusCode.OK,
                RequestUtilities.ToJsonString(this.mockWorkflowResponse));

            var response = await this.client.Workflows.Trigger(this.mockWorkflow["key"] as string, this.mockTriggerWorkflow, this.mockIdempotentOptions);

            this.httpMock.AssertRequestWasMade(
                HttpMethod.Post,
                $"/v1/workflows/{this.mockWorkflow["key"]}/trigger");

            this.httpMock.AssertRequestHasHeader(
                "Idempotency-Key",
                this.mockIdempotentOptions.IdempotencyKey);

            Assert.Equal(
                JsonConvert.SerializeObject(this.mockWorkflowResponse),
                JsonConvert.SerializeObject(response));
        }
    }
}
