using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Workflow Recipient Runs
    /// </summary>
    public class WorkflowRecipientRunsResource : BaseResource
    {
        /// <summary>
        /// Ctor for workflow recipient run methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public WorkflowRecipientRunsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a paginated list of workflow recipient runs
        /// </summary>
        /// <param name="options">Options for filtering (workflow, status, tenant, recipient, etc.) and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated WorkflowRecipientRun response.</returns>
        public async Task<PaginatedResponse<WorkflowRecipientRun>> List(Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/workflow_recipient_runs",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<WorkflowRecipientRun>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a workflow recipient run, including its events
        /// </summary>
        /// <param name="workflowRecipientRunId">Workflow recipient run unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A Knock WorkflowRecipientRun record.</returns>
        public async Task<WorkflowRecipientRun> Get(string workflowRecipientRunId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/workflow_recipient_runs/{workflowRecipientRunId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<WorkflowRecipientRun>(request, cancellationToken);
        }
    }
}
