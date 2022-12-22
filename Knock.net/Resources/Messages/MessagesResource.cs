using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Messages
    /// </summary>
    public class MessagesResource : BaseResource
    {
        /// <summary>
        /// Ctor for messages methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public MessagesResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a paginated list of messages
        /// </summary>
        /// <param name="options">Options filtering and pagination</param>
        /// <returns>A paginated Message response.</returns>
        public async Task<PaginatedResponse<Message>> List(Dictionary<string, object> options = null)
        {
            if (options.ContainsKey("trigger_data"))
            {
                var triggerData = (Dictionary<string, object>) options["trigger_data"];
                var triggerDataAsString = Newtonsoft.Json.JsonConvert.SerializeObject(triggerData);
                options.Remove("trigger_data");
                options.Add("trigger_data", triggerDataAsString);
            }

            var request = new KnockRequest
            {
                Path = $"/messages",
                Method = HttpMethod.Get,
                Options=options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Message>>(request);
        }

        /// <summary>
        /// Returns a message
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <returns>A Knock Message record.</returns>
        public async Task<Message> Get(string messageId)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Message>(request);
        }

        /// <summary>
        /// Returns a message's content
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <returns>A Knock MessageContent record.</returns>
        public async Task<MessageContent> GetContent(string messageId)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/content",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<MessageContent>(request);
        }

        /// <summary>
        /// Returns a message's events
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="options">Dictionary of params for filtering and pagination</param>
        /// <returns>A paginated Knock MessageEvent response.</returns>
        public async Task<PaginatedResponse<MessageEvent>> GetEvents(string messageId, Dictionary<string, object> options = null)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/events",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<MessageEvent>>(request);
        }

        /// <summary>
        /// Returns a message's events
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="options">Dictionary of params for filtering and pagination</param>
        /// <returns>A paginated Knock Activity response.</returns>
        public async Task<PaginatedResponse<Activity>> GetActivities(string messageId, Dictionary<string, object> options = null)
        {
            if (options.ContainsKey("trigger_data"))
            {
                var triggerData = (Dictionary<string, object>) options["trigger_data"];
                var triggerDataAsString = Newtonsoft.Json.JsonConvert.SerializeObject(triggerData);
                options.Remove("trigger_data");
                options.Add("trigger_data", triggerDataAsString);
            }

            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/activities",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Activity>>(request);
        }
    }
}