namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A paginated page of a user's in-app feed.
    /// </summary>
    public class UserFeed : PaginatedResponse<FeedItem>
    {
        /// <summary>
        /// Metadata about the feed, such as unread and unseen counts
        /// </summary>
        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }

        /// <summary>
        /// Environment variables available to the feed
        /// </summary>
        [JsonProperty("vars")]
        public Dictionary<string, object> Vars { get; set; }
    }
}
