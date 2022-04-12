using System;
namespace Knock
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class PaginatedResponse<T>
    {

        [JsonProperty("page_info")]
        public PageInfo pageInfo { get; set; }

        [JsonProperty("items")]
        public ICollection<T> items { get; set; }

    }

    public class PageInfo
    {
        [JsonProperty("page_size")]
        public int pageSize { get; set; }

        [JsonProperty("before")]
        public string before { get; set; }

        [JsonProperty("after")]
        public string after { get; set; }
    }
}