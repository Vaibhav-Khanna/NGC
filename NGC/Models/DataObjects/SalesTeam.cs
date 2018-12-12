using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class SalesTeam : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
