using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knock
{
    /// <summary>
    /// Methods for working with Objects
    /// </summary>
    public class ObjectsResource : BaseResource
    {
        /// <summary>
        /// Ctor for object methods
        /// </summary>
        /// <param name="client">The knock client</param>
        public ObjectsResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns an object in a collection.
        /// </summary>
        /// <param name="collection">Object unique identifier.</param>
        /// <param name="objectId">Object unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Object record.</returns>
        public async Task<Object> Get(string collection, string objectId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Object>(request, cancellationToken);
        }

        /// <summary>
        /// Sets an object in a collection.
        /// </summary>
        /// <param name="collection">The collection this object belongs to</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="options">Dictionary of params</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Object record.</returns>
        public async Task<Object> Set(string collection, string objectId, Dictionary<string, object> options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<Object>(request, cancellationToken);
        }

        /// <summary>
        /// Bulk sets an objects in a collection.
        /// </summary>
        /// <param name="collection">The collection that the objects should be set in</param>
        /// <param name="options">Options for the bulk set</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkSet(string collection, BulkSetObjectsOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/bulk/set",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes an object in a collection.
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock Object record.</returns>
        public async Task<Object> Delete(string collection, string objectId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}",
                Method = HttpMethod.Delete,
            };

            return await Client.MakeAPIRequest<Object>(request, cancellationToken);
        }

        /// <summary>
        /// Bulk deletes objects in a collection.
        /// </summary>
        /// <param name="collection">The collection that the objects should be set in</param>
        /// <param name="options">Options for the bulk delete</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkDelete(string collection, BulkDeleteObjectsOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/bulk/set",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }

        /// <summary>
        /// Returns the channel data for the object
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock ChannelData record.</returns>
        public async Task<ChannelData> GetChannelData(string collection, string objectId, string channelId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/channel_data/{channelId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel data for the object on the channel provided.
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="data">The data to set</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock ChannelData entry.</returns>
        public async Task<ChannelData> SetChannelData(string collection, string objectId, string channelId, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/channel_data/{channelId}",
                Method = HttpMethod.Put,
                Options = data,
            };

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }
    }
}