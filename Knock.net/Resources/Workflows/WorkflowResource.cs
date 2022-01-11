using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    using Response = Dictionary<string, object>;

    /// <summary>
    /// Methods for working with Workflows
    /// </summary>
    public class WorkflowResource : BaseResource
    {
        /// <summary>
        /// Constructor for initializing a workflow resource
        /// </summary>
        /// <param name="client">The knock client</param>
        public WorkflowResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Triggers a given workflow
        /// </summary>
        /// <param name="workflowKey">The key of the workflow to trigger</param>
        /// <param name="triggerWorkflowOptions">The data for the trigger</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A response dictionary</returns>
        public async Task<Response> Trigger(string workflowKey, TriggerWorkflow triggerWorkflowOptions, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/workflows/{workflowKey}/trigger",
                Method = HttpMethod.Post,
                Options = triggerWorkflowOptions,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Cancels a workflow run
        /// </summary>
        /// <param name="workflowKey">The key of the workflow</param>
        /// <param name="cancelWorkflowOptions">The information about the cancellation</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>Response dictionary</returns>
        public async Task<Response> Cancel(string workflowKey, CancelWorkflow cancelWorkflowOptions, CancellationToken cancellationToken = default) {
            var request = new KnockRequest
            {
                Path = $"/workflows/{workflowKey}/trigger",
                Method = HttpMethod.Post,
                Options = cancelWorkflowOptions,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }


    }
}
