namespace Knock
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Methods for working with Preferences
    /// </summary>
    public class PreferencesResource : BaseResource
    {
        /// <summary>
        /// The default preference set id
        /// </summary>
        public const string DefaultPreferenceSetId = "default";

        /// <summary>
        /// Constructor for creating a new preferences resource
        /// </summary>
        /// <param name="client">The Knock client</param>
        public PreferencesResource(KnockClient client) : base(client) { }

        /// <summary>
        /// Returns all preference sets for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>List of preference sets</returns>
        public async Task<List<PreferenceSet>> GetAll(string userId, CancellationToken cancellationToken = default)
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
        public async Task<PreferenceSet> Get(string userId, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Get,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }

        /// <summary>
        /// Sets the data onto the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="setPreferenceSet">The preference set data to set</param>
        /// <param name="preferenceSetId">An optional preference set id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>PreferenceSet</returns>
        public async Task<PreferenceSet> Set(string userId, SetPreferenceSet setPreferenceSet, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}",
                Method = HttpMethod.Put,
                Options = setPreferenceSet,
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
        public async Task<PreferenceSet> SetChannelType(string userId, string channelType, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
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
        public async Task<PreferenceSet> SetWorkflow(string userId, string workflowKey, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
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
        public async Task<PreferenceSet> SetWorkflow(string userId, string workflowKey, Dictionary<string, bool> channelTypes, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var request = new KnockRequest
            {
                Path = $"/users/{userId}/preferences/{preferenceSetId}/workflows/{workflowKey}",
                Method = HttpMethod.Put,
                Options = channelTypes,
            };

            return await Client.MakeAPIRequest<PreferenceSet>(request, cancellationToken);
        }
    }
}
