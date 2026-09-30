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
    using Response = Dictionary<string, object>;

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
        /// The default preference set id
        /// </summary>
        public const string DefaultPreferenceSetId = "default";

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
            var request = new KnockRequest
            {
                Path = $"/users/{userId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<User>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a paginated list of users
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>Paginate list of users.</returns>
        public async Task<PaginatedResponse<User>> List(Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest {
                Path = $"/users",
                Method = HttpMethod.Get,
                Options = options
            };

            return await Client.MakeAPIRequest<PaginatedResponse<User>>(request, cancellationToken);
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
        /// Bulk identifies multiple users
        /// </summary>
        /// <param name="options">Options to pass to the request</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkIdentify(BulkIdentifyUsersOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/bulk/identify",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
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
        /// Bulk deletes multiple users
        /// </summary>
        /// <param name="options">Options to pass to the request</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation record.</returns>
        public async Task<BulkOperation> BulkDelete(BulkDeleteUsersOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/bulk/delete",
                Method = HttpMethod.Post,
                Options = options,
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }

        /// <summary>
        /// Merges the user specified with `fromUserId` into the user specified with `userId`.
        /// </summary>
        /// <param name="userId">User identifier to merge into.</param>
        /// <param name="fromUserId">User identifier to merge from.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock User record.</returns>
        public async Task<User> Merge(string userId, string fromUserId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/merge",
                Method = HttpMethod.Post,
                Options = new Dictionary<string, string>
                {
                    {"from_user_id", fromUserId}
                },
            };

            return await Client.MakeAPIRequest<User>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a paginated list of the objects the user is subscribed to
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A paginated list of ObjectSubscription records.</returns>
        public async Task<PaginatedResponse<ObjectSubscription>> ListSubscriptions(string userId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/subscriptions",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<ObjectSubscription>>(request, cancellationToken);
        }

        #region Feeds

        /// <summary>
        /// Returns a paginated list of items in the user's in-app feed
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">The id of the in-app feed channel.</param>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A page of the user's feed.</returns>
        public async Task<UserFeed> GetFeedItems(string userId, string channelId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/feeds/{channelId}",
                Method = HttpMethod.Get,
                Options = RequestUtilities.SerializeTriggerData(options),
            };

            return await Client.MakeAPIRequest<UserFeed>(request, cancellationToken);
        }

        /// <summary>
        /// Returns the settings for the user's in-app feed
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">The id of the in-app feed channel.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A response dictionary containing the feed's `features`.</returns>
        public async Task<Response> GetFeedSettings(string userId, string channelId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/feeds/{channelId}/settings",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        #endregion

        #region Guides

        /// <summary>
        /// Returns the in-app guides the user is eligible for on a channel
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">The id of the guide channel.</param>
        /// <param name="options">Options for filtering by tenant, data or type.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The user's guides.</returns>
        public async Task<GuidesResponse> GetGuides(string userId, string channelId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/{channelId}",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuidesResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Records that a guide has been seen by the user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">The guide that was seen.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The result of the action.</returns>
        public async Task<GuideActionResponse> MarkGuideMessageAsSeen(string userId, GuideSeenOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/messages/seen",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuideActionResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Records that the user has interacted with a guide
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">The guide that was interacted with.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The result of the action.</returns>
        public async Task<GuideActionResponse> MarkGuideMessageAsInteracted(string userId, GuideInteractedOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/messages/interacted",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuideActionResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Records that the user has archived a guide
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">The guide that was archived.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The result of the action.</returns>
        public async Task<GuideActionResponse> MarkGuideMessageAsArchived(string userId, GuideArchivedOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/messages/archived",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuideActionResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Unarchives a guide for the user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">The guide to unarchive.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The result of the action.</returns>
        public async Task<GuideActionResponse> UnarchiveGuideMessage(string userId, GuideUnarchivedOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/messages/archived",
                Method = HttpMethod.Delete,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuideActionResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Resets the user's engagement with a guide so it can be shown again
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="options">The guide to reset.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The result of the action.</returns>
        public async Task<GuideActionResponse> ResetGuideEngagements(string userId, GuideResetOptions options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/guides/engagements/reset",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<GuideActionResponse>(request, cancellationToken);
        }

        #endregion

        #region PreferenceCenter

        /// <summary>
        /// Returns the hosted preference center configuration for the user
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A response dictionary with the preference center configuration.</returns>
        public async Task<Response> GetPreferenceCenterConfig(string userId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preference_center/config",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<Response>(request, cancellationToken);
        }

        /// <summary>
        /// Generates a signed URL to the user's hosted preference center
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>The signed URL and token.</returns>
        public async Task<PreferenceCenterSignedUrl> GeneratePreferenceCenterSignedUrl(string userId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preference_center/signed_url",
                Method = HttpMethod.Post,
            };

            return await Client.MakeAPIRequest<PreferenceCenterSignedUrl>(request, cancellationToken);
        }

        #endregion

        #region ChannelData

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

        /// <summary>
        /// Unsets the channel data for the user on the channel provided.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>No response.</returns>
        public async Task<ChannelData> UnsetChannelData(string userId, string channelId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/channel_data/{channelId}",
                Method = HttpMethod.Delete,
            };

            return await Client.MakeAPIRequest<ChannelData>(request, cancellationToken);
        }

        #endregion

        #region Preferences

        /// <summary>
        /// Returns all preference sets for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of preference sets</returns>
        public async Task<List<PreferenceSet>> GetAllPreferences(string userId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<List<PreferenceSet>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a single preference set for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="preferenceSetId">The identifier of the preference set, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>Preference set</returns>
        public async Task<PreferenceSet> GetPreferences(string userId, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a single preference set for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="preferenceSetId">The identifier of the preference set</param>
        /// <param name="options">Query options, e.g. `tenant` to merge tenant-level defaults</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>Preference set</returns>
        public async Task<PreferenceSet> GetPreferences(string userId, string preferenceSetId, Dictionary<string, object> options, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Unsets (deletes) a preference set for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="preferenceSetId">The identifier of the preference set, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>No response.</returns>
        public async Task UnsetPreferences(string userId, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Delete,
            };

            await Client.MakeAPIRequest(request, cancellationToken);
        }

        /// <summary>
        /// Sets the data onto the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="options">The preference set data to set</param>
        /// <param name="preferenceSetId">An optional preference set id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>PreferenceSet</returns>
        public async Task<PreferenceSet> SetPreferences(string userId, SetPreferenceSet options, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel type for the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="channelType">Type of channel to set</param>
        /// <param name="subscribed">Boolean to set for the channel type</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        [Obsolete("The granular preference endpoints are deprecated. Use SetPreferences with PersistenceStrategy = \"merge\".")]
        public async Task<PreferenceSet> SetChannelTypePreferences(string userId, string channelType, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var options = new Dictionary<string, bool>{
                { "subscribed", subscribed }
            };

            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}/channel_types/{channelType}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the workflow setting for the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="workflowKey">The workflow to set preferences for</param>
        /// <param name="subscribed">Boolean preference setting</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        [Obsolete("The granular preference endpoints are deprecated. Use SetPreferences with PersistenceStrategy = \"merge\".")]
        public async Task<PreferenceSet> SetWorkflowPreferences(string userId, string workflowKey, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var options = new Dictionary<string, bool>{
                { "subscribed", subscribed }
            };

            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}/workflows/{workflowKey}",
                Method = HttpMethod.Put,
                Options = options,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the channel types for the provided workflow in the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="workflowKey">The workflow to set preferences for</param>
        /// <param name="channelTypes">Dictionary of channel type settings</param>
        /// <param name="preferenceSetId">Optional preference set id, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A preference set object</returns>
        [Obsolete("The granular preference endpoints are deprecated. Use SetPreferences with PersistenceStrategy = \"merge\".")]
        public async Task<PreferenceSet> SetWorkflowPreferences(string userId, string workflowKey, Dictionary<string, bool> channelTypes, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}/workflows/{workflowKey}",
                Method = HttpMethod.Put,
                Options = channelTypes,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Bulk sets the preferences for the users provided
        /// </summary>
        /// <param name="options">Options to send.</param>
        /// <param name="preferenceSetId">The preference set id (defaults to "default").</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A Knock BulkOperation entry.</returns>
        public async Task<BulkOperation> BulkSetPreferences(BulkSetUserPreferencesOptions options, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            // Set the preference set id if not given
            if (options.Preferences.Id == null)
            {
                options.Preferences.Id = preferenceSetId;
            }

            var request = new KnockRequest
            {
                Path = $"/users/bulk/preferences",
                Method = HttpMethod.Post,
                Options = options
            };

            return await Client.MakeAPIRequest<BulkOperation>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a paginated response of the user's messages
        /// </summary>
        /// <param name="userId">Unique identifier.</param>
        /// <param name="options">Options filtering and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Message response.</returns>
        public async Task<PaginatedResponse<Message>> GetMessages(string userId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/messages",
                Method = HttpMethod.Get,
                Options = RequestUtilities.SerializeTriggerData(options),
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Message>>(request, cancellationToken);
        }

        /// <summary>
        /// Returns a paginated response of the user's schedules
        /// </summary>
        /// <param name="userId">Unique identifier.</param>
        /// <param name="options">Options filtering and pagination</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>A paginated Schedule response.</returns>
        public async Task<PaginatedResponse<Schedule>> GetSchedules(string userId, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/schedules",
                Method = HttpMethod.Get,
                Options = options,
            };

            return await Client.MakeAPIRequest<PaginatedResponse<Schedule>>(request, cancellationToken);
        }

        #endregion
    }
}
