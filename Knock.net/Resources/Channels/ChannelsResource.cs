using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Channels
    /// </summary>
    public class ChannelsResource : BaseResource
    {
        /// <summary>
        /// Ctor for channel methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public ChannelsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Bulk updates the status of messages sent through a channel
        /// </summary>
        /// <param name="channelId">The id of the channel</param>
        /// <param name="action">
        /// The status to apply, one of: `seen`, `unseen`, `read`, `unread`, `archived`,
        /// `unarchived`, `interacted`, `archive`, `unarchive`, `delete`
        /// </param>
        /// <param name="options">Filters selecting which messages to update</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkUpdateMessageStatus(string channelId, string action, BulkUpdateChannelMessagesOptions options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/channels/{channelId}/messages/bulk/{action}",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }
    }
}
