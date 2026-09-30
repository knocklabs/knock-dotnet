using System;
using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Message response.</returns>
        public async Task<PaginatedResponse<Message>> List(Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages",
                Method = HttpMethod.Get,
                Options = RequestUtilities.SerializeTriggerData(options),
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Message>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a message
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A Knock Message record.</returns>
        public async Task<Message> Get(string messageId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Message>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a message's content
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A Knock MessageContent record.</returns>
        public async Task<MessageContent> GetContent(string messageId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/content",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<MessageContent>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a message's events
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="options">Dictionary of params for filtering and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Knock MessageEvent response.</returns>
        public async Task<PaginatedResponse<MessageEvent>> GetEvents(string messageId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/events",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<MessageEvent>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a message's activities
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="options">Dictionary of params for filtering and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Knock Activity response.</returns>
        public async Task<PaginatedResponse<Activity>> GetActivities(string messageId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/activities",
                Method = HttpMethod.Get,
                Options = RequestUtilities.SerializeTriggerData(options),
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Activity>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns the provider delivery logs for a message
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="options">Dictionary of params for pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Knock MessageDeliveryLog response.</returns>
        public async Task<PaginatedResponse<MessageDeliveryLog>> GetDeliveryLogs(string messageId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/delivery_logs",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<MessageDeliveryLog>>(request, cancellationToken);
        }

        #region Status

        /// <summary>
        /// Marks a message as seen
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> MarkAsSeen(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "seen", HttpMethod.Put, null, cancellationToken);
        }

        /// <summary>
        /// Marks a message as unseen
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> MarkAsUnseen(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "seen", HttpMethod.Delete, null, cancellationToken);
        }

        /// <summary>
        /// Marks a message as read
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> MarkAsRead(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "read", HttpMethod.Put, null, cancellationToken);
        }

        /// <summary>
        /// Marks a message as unread
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> MarkAsUnread(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "read", HttpMethod.Delete, null, cancellationToken);
        }

        /// <summary>
        /// Marks a message as interacted with
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="metadata">Optional metadata about the interaction</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> MarkAsInteracted(string messageId, Dictionary<string, object> metadata = null, CancellationToken cancellationToken = default)
        {
            var options = new Dictionary<string, object>();
            if (metadata != null)
            {
                options.Add("metadata", metadata);
            }

            return UpdateStatus(messageId, "interacted", HttpMethod.Put, options, cancellationToken);
        }

        /// <summary>
        /// Archives a message
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> Archive(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "archived", HttpMethod.Put, null, cancellationToken);
        }

        /// <summary>
        /// Unarchives a message
        /// </summary>
        /// <param name="messageId">Message unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Message.</returns>
        public Task<Message> Unarchive(string messageId, CancellationToken cancellationToken = default)
        {
            return UpdateStatus(messageId, "archived", HttpMethod.Delete, null, cancellationToken);
        }

        #endregion

        #region Batch

        /// <summary>
        /// Marks the given messages as seen
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchMarkAsSeen(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("seen", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Marks the given messages as unseen
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchMarkAsUnseen(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("unseen", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Marks the given messages as read
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchMarkAsRead(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("read", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Marks the given messages as unread
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchMarkAsUnread(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("unread", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Marks the given messages as interacted with
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="metadata">Optional metadata about the interaction</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchMarkAsInteracted(List<string> messageIds, Dictionary<string, object> metadata = null, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("interacted", messageIds, metadata, cancellationToken);
        }

        /// <summary>
        /// Archives the given messages
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchArchive(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("archived", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Unarchives the given messages
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>The updated Messages.</returns>
        public Task<List<Message>> BatchUnarchive(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            return BatchUpdateStatus("unarchived", messageIds, null, cancellationToken);
        }

        /// <summary>
        /// Returns the contents of the given messages
        /// </summary>
        /// <param name="messageIds">Message unique identifiers.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A list of Knock MessageContent records.</returns>
        public async Task<List<MessageContent>> BatchGetContent(List<string> messageIds, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/batch/content",
                Method = HttpMethod.Get,
                Options = new Dictionary<string, object> { { "message_ids", messageIds } },
            };

            return await Client.MakeAPIRequest<List<MessageContent>>(request, cancellationToken);
        }

        #endregion

        private async Task<Message> UpdateStatus(string messageId, string status, HttpMethod method, Dictionary<string, object> options, CancellationToken cancellationToken)
        {
            var request = new KnockRequest
            {
                Path = $"/messages/{messageId}/{status}",
                Method = method,
                Options = options,
            };

            return await Client.MakeAPIRequest<Message>(request, cancellationToken);
        }

        private async Task<List<Message>> BatchUpdateStatus(string status, List<string> messageIds, Dictionary<string, object> metadata, CancellationToken cancellationToken)
        {
            var options = new Dictionary<string, object> { { "message_ids", messageIds } };
            if (metadata != null)
            {
                options.Add("metadata", metadata);
            }

            var request = new KnockRequest
            {
                Path = $"/messages/batch/{status}",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<List<Message>>(request, cancellationToken);
        }
    }
}
