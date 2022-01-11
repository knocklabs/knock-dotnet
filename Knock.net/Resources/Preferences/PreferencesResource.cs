namespace Knock
{
    using System;
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
        public const string DefaultPreferenceSetId = UsersResource.DefaultPreferenceSetId;

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
        [Obsolete("This method is deprecated. Use Users.GetAllPreferences")]
        public async Task<List<PreferenceSet>> GetAll(string userId, CancellationToken cancellationToken = default)
        {
            var userResource = new UsersResource(Client);
            return await userResource.GetAllPreferences(userId, cancellationToken);
        }

        /// <summary>
        /// Returns a single preference set for the user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="preferenceSetId">The identifier of the preference set, defaults to "default"</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>Preference set</returns>
        [Obsolete("This method is deprecated. Use Users.GetPreferences")]
        public async Task<PreferenceSet> Get(string userId, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var userResource = new UsersResource(Client);
            return await userResource.GetPreferences(userId, preferenceSetId, cancellationToken);
        }

        /// <summary>
        /// Sets the data onto the preference set
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <param name="setPreferenceSet">The preference set data to set</param>
        /// <param name="preferenceSetId">An optional preference set id</param>
        /// <param name="cancellationToken">An optional token to cancel the request</param>
        /// <returns>PreferenceSet</returns>
        [Obsolete("This method is deprecated. Use Users.SetPreferences")]
        public async Task<PreferenceSet> Set(string userId, SetPreferenceSet setPreferenceSet, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var userResource = new UsersResource(Client);
            return await userResource.SetPreferences(userId, setPreferenceSet, preferenceSetId, cancellationToken);
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
        [Obsolete("This method is deprecated. Use Users.SetChannelTypePreferences")]
        public async Task<PreferenceSet> SetChannelType(string userId, string channelType, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var userResource = new UsersResource(Client);
            return await userResource.SetChannelTypePreferences(userId, channelType, subscribed, preferenceSetId, cancellationToken);
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
        [Obsolete("This method is deprecated. Use Users.SetWorkflowPreferences")]
        public async Task<PreferenceSet> SetWorkflow(string userId, string workflowKey, bool subscribed, string preferenceSetId = DefaultPreferenceSetId, CancellationToken cancellationToken = default)
        {
            var userResource = new UsersResource(Client);
            return await userResource.SetWorkflowPreferences(userId, workflowKey, subscribed, preferenceSetId, cancellationToken);
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
            var userResource = new UsersResource(Client);
            return await userResource.SetWorkflowPreferences(userId, workflowKey, channelTypes, preferenceSetId, cancellationToken);
        }
    }
}
