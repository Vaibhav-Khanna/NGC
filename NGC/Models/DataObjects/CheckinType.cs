using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class CheckinType : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
