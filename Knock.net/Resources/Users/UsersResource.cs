using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    // We don't define a user type, but instead just use it as a dictionary
    // we do this right now because the fields on the user are dynamic.
    using User = Dictionary<string, object>;

    /// <summary>
    /// Methods for working with users
    /// </summary>
    public class UsersResource : BaseResource
    {
        /// <summary>
        /// Ctor for user methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public UsersResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns a user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock User record.</returns>
        public async Task<User> Get(string userId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest {
                Path = $"/users/{userId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<User>(request, cancellationToken);
        }

        /// <summary>
        /// Identifies a user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">Dictionary of params</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock User record.</returns>
        public async Task<User> Identify(string userId, Dictionary<string, object> options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<User>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock User record.</returns>
        public async Task<User> Delete(string userId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}",
                Method = HttpMethod.Delete,
            };

            return await Client.MakeAPIRequest<User>(request, cancellationToken);
        }

        /// <summary>
        /// Returns the channel data for the user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock ChannelData entry.</returns>
        public async Task<ChannelData> GetChannelData(string userId, string channelId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/channel_data/{channelId}",
                Method = HttpMethod.Get,
            };

            Console.Write(Client);

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel data for the user on the channel provided.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="data">The data to set</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock ChannelData entry.</returns>
        public async Task<ChannelData> SetChannelData(string userId, string channelId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/channel_data/{channelId}",
                Method = HttpMethod.Put,
                Options = data,
            };

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }
    }
}
