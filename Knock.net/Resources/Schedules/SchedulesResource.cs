using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Schedules
    /// </summary>
    public class SchedulesResource : BaseResource
    {
        /// <summary>
        /// Ctor for schedule methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public SchedulesResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a paginated list of schedules for a workflow
        /// </summary>
        /// <param name="workflowKey">Workflow key</param>
        /// <param name="options">Options filtering (recipients, tenant) and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Schedule response.</returns>
        public async Task<PaginatedResponse<Schedule>> List(string workflowKey, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var query = options == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(options);
            query["workflow"] = workflowKey;

            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Get,
                Options = query,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Schedule>>(request, cancellationToken);
        }

        /// <summary>
        /// Creates schedules for a workflow and set of recipients
        /// </summary>
        /// <param name="options">Schedules creation attributes, including the workflow key</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of created schedules</returns>
        public async Task<List<Schedule>> Create(CreateSchedules options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<List<Schedule>>(request, cancellationToken);
        }

        /// <summary>
        /// Updates schedules
        /// </summary>
        /// <param name="options">Schedules update attributes, including the schedule ids</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of updated schedules</returns>
        public async Task<List<Schedule>> Update(UpdateSchedules options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<List<Schedule>>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes schedules
        /// </summary>
        /// <param name="scheduleIds">Ids from schedules to be deleted</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of deleted schedules</returns>
        public async Task<List<Schedule>> Delete(List<string> scheduleIds, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/schedules",
                Method = HttpMethod.Delete,
                Options = new Dictionary<string, List<string>>
                {
                    { "schedule_ids", scheduleIds },
                },
            };

            return await Client.MakeAPIRequest<List<Schedule>>(request, cancellationToken);
        }

        /// <summary>
        /// Bulk creates schedules, each with a single recipient
        /// </summary>
        /// <param name="options">The schedules to create</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkCreate(BulkCreateSchedulesOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/schedules/bulk/create",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }
    }
}
