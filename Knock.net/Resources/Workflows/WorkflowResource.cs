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
        /// <param name="options">Optional parameters for the request</param>
        /// <returns>A response dictionary</returns>
        public async Task<Response> Trigger(string workflowKey, TriggerWorkflow triggerWorkflowOptions, MethodOptions options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/workflows/{workflowKey}/trigger",
                Method = HttpMethod.Post,
                Options = triggerWorkflowOptions,
            };

            if (options != null)
            {
                request.KnockHeaders = new Dictionary<string, string>();
                if (options.IdempotencyKey != null) {
                    request.KnockHeaders.Add("Idempotency-Key", options.IdempotencyKey);
                }
            }

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
                Path = $"/workflows/{workflowKey}/cancel",
                Method = HttpMethod.Post,
                Options = cancelWorkflowOptions,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Create schedules for given workflow and recipients
        /// </summary>
        /// <param name="workflowKey">The key of the workflow</param>
        /// <param name="createSchedulesOptions">Schedules creation attributes</param>
        /// <returns>List of created schedules</returns>
        public async Task<List<Schedule>> CreateSchedules(string workflowKey, CreateSchedules createSchedulesOptions)
        {
            createSchedulesOptions.Workflow = workflowKey;

            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Post,
                Options = createSchedulesOptions,
            };

            return await Client.MakeAPIRequest<List<Schedule>>(request);
        }

        /// <summary>
        /// Update schedules for given workflow and recipients
        /// </summary>
        /// <param name="scheduleIds">Schedule ids</param>
        /// <param name="updateSchedulesOptions">Schedules update attributes</param>
        /// <returns>List of updated schedules</returns>
        public async Task<List<Schedule>> UpdateSchedules(List<String> scheduleIds, UpdateSchedules updateSchedulesOptions)
        {
            updateSchedulesOptions.ScheduleIds = scheduleIds;

            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Put,
                Options = updateSchedulesOptions,
            };

            return await Client.MakeAPIRequest<List<Schedule>>(request);
        }

        /// <summary>
        /// Deletes schedules
        /// </summary>
        /// <param name="scheduleIds">Ids from schedules to be deleted</param>
        /// <returns>List of deleted schedules</returns>
        public async Task<List<Schedule>> DeleteSchedules(List<String> scheduleIds)
        {
            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Delete,
                Options = new Dictionary<String, List<String>> {
                    { "schedule_ids", scheduleIds}
                }

            };

            return await Client.MakeAPIRequest<List<Schedule>>(request);
        }

        /// <summary>
        /// Returns a paginated list of schedules for workflow
        /// </summary>
        /// <param name="workflowKey">Workflow key</param>
        /// <param name="options">Options filtering and pagination</param>
        /// <returns>A paginated Schedule response.</returns>
        public async Task<PaginatedResponse<Schedule>> ListSchedules(String workflowKey, Dictionary<string, object> options = null)
        {
            if (options == null)
            {
                options = new Dictionary<string, object> { { "workflow", workflowKey } };
            } else {
                options.Add("workflow", workflowKey);
            }

            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Schedule>>(request);
        }
    }
}
