﻿using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// The pagination information of the response
        /// </summary>
        [JsonProperty("page_info")]
        public PageInfo pageInfo { get; set; }

        /// <summary>
        /// The records of the response (for messages, message events and activities)
        /// </summary>
        [JsonProperty("items")]
        public ICollection<T> items { get; set; }

        /// <summary>
        /// The records of the response (for the rest of the paginated entities)
        /// </summary>
        [JsonProperty("entries")]
        public ICollection<T> entries { get; set; }

    }

    public class PageInfo
    {
        /// <summary>
        /// The size of the response's page
        /// </summary>
        [JsonProperty("page_size")]
        public int pageSize { get; set; }

        /// <summary>
        /// The before cursor for response's page
        /// </summary>
        [JsonProperty("before")]
        public string before { get; set; }

        /// <summary>
        /// The after cursor for response's page
        /// </summary>
        [JsonProperty("after")]
        public string after { get; set; }
    }
}
