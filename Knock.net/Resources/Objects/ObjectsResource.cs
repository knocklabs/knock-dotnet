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
        /// The default preference set id
        /// </summary>
        public const string DefaultPreferenceSetId = "default";

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

        /// <summary>
        /// Sets the channel data for the object on the channel provided.
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>No response.</returns>
        public async Task<ChannelData> UnsetChannelData(string collection, string objectId, string channelId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/channel_data/{channelId}",
                Method = HttpMethod.Delete,
            };

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a paginated response of the object's messages
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="options">Options filtering and pagination</param>
        /// <returns>A paginated Message response.</returns>
        public async Task<PaginatedResponse<Message>> GetMessages(string collection, string objectId, Dictionary<string, object> options = null)
        {
            if (options != null && options.ContainsKey("trigger_data"))
            {
                var triggerData = (Dictionary<string, object>) options["trigger_data"];
                var triggerDataAsString = Newtonsoft.Json.JsonConvert.SerializeObject(triggerData);
                options.Remove("trigger_data");
                options.Add("trigger_data", triggerDataAsString);
            }

            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/messages",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Message>>(request);
        }

        /// <summary>
        /// Returns a paginated response of the object's schedules
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="options">Options filtering and pagination</param>
        /// <returns>A paginated Schedule response.</returns>
        public async Task<PaginatedResponse<Schedule>> GetSchedules(string collection, string objectId, Dictionary<string, object> options = null)
        {

            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/schedules",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Schedule>>(request);
        }

        #region Preferences

        /// <summary>
        /// Returns all preference sets for the object
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of preference sets</returns>
        public async Task<List<PreferenceSet>> GetAllPreferences(string collection, string objectId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<List<PreferenceSet>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a single preference set for the object
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="preferenceSetId">The identifier of the preference set, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>Preference set</returns>
        public async Task<PreferenceSet> GetPreferences(string collection, string objectId, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the data onto the preference set
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="options">The preference set data to set</param>
        /// <param name="preferenceSetId">An optional preference set id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>PreferenceSet</returns>
        public async Task<PreferenceSet> SetPreferences(string collection, string objectId, SetPreferenceSet options, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel type for the preference set
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="channelType">Type of channel to set</param>
        /// <param name="subscribed">Boolean to set for the channel type</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        public async Task<PreferenceSet> SetChannelTypePreferences(string collection, string objectId, string channelType, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var options = new Dictionary<string, bool>{
                { "subscribed", subscribed }
            };

            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences/{preferenceSetId}/channel_types/{channelType}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the workflow setting for the preference set
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="workflowKey">The workflow to set preferences for</param>
        /// <param name="subscribed">Boolean preference setting</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        public async Task<PreferenceSet> SetWorkflowPreferences(string collection, string objectId, string workflowKey, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var options = new Dictionary<string, bool>{
                { "subscribed", subscribed }
            };

            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences/{preferenceSetId}/workflows/{workflowKey}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel types for the provided workflow in the preference set
        /// </summary>
        /// <param name="collection">Collection the object belongs to.</param>
        /// <param name="objectId">Unique identifier.</param>
        /// <param name="workflowKey">The workflow to set preferences for</param>
        /// <param name="channelTypes">Dictionary of channel type settings</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        public async Task<PreferenceSet> SetWorkflowPreferences(string collection, string objectId, string workflowKey, Dictionary<string, object> channelTypes, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/objects/{collection}/{objectId}/preferences/{preferenceSetId}/workflows/{workflowKey}",
                Method = HttpMethod.Put,
                Options = channelTypes,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        #endregion
    }
}