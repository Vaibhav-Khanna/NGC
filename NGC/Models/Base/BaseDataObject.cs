using System;
using Newtonsoft.Json;

namespace NGC.Models
{
    public class BaseDataObject : IBaseDataObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
             
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
}