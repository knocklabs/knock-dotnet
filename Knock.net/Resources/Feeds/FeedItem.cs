namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about an item in a user's in-app feed.
    /// </summary>
    public class FeedItem
    {
        /// <summary>
        /// The id of the feed item
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The activities that make up the feed item (omitted in `compact` mode)
        /// </summary>
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        /// <summary>
        /// The actors associated with the feed item (omitted in `compact` mode)
        /// </summary>
        [JsonProperty("actors")]
        public List<object> Actors { get; set; }

        /// <summary>
        /// The rendered content blocks of the feed item
        /// </summary>
        [JsonProperty("blocks")]
        public List<Dictionary<string, object>> Blocks { get; set; }

        /// <summary>
        /// Data associated with the feed item
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// The workflow source of the feed item
        /// </summary>
        [JsonProperty("source")]
        public Dictionary<string, object> Source { get; set; }

        /// <summary>
        /// The id of the tenant the feed item belongs to
        /// </summary>
        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        /// <summary>
        /// The total number of activities in the feed item
        /// </summary>
        [JsonProperty("total_activities")]
        public int TotalActivities { get; set; }

        /// <summary>
        /// The total number of actors in the feed item
        /// </summary>
        [JsonProperty("total_actors")]
        public int TotalActors { get; set; }

        /// <summary>
        /// The date the feed item was seen (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("seen_at")]
        public string SeenAt { get; set; }

        /// <summary>
        /// The date the feed item was read (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("read_at")]
        public string ReadAt { get; set; }

        /// <summary>
        /// The date the feed item was interacted with (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("interacted_at")]
        public string InteractedAt { get; set; }

        /// <summary>
        /// The date the feed item was clicked (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("clicked_at")]
        public string ClickedAt { get; set; }

        /// <summary>
        /// The date a link in the feed item was clicked (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("link_clicked_at")]
        public string LinkClickedAt { get; set; }

        /// <summary>
        /// The date the feed item was archived (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("archived_at")]
        public string ArchivedAt { get; set; }

        /// <summary>
        /// The date the feed item was inserted at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("inserted_at")]
        public string InsertedAt { get; set; }

        /// <summary>
        /// The date the feed item was updated at (as an ISO8601 datetime string)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
