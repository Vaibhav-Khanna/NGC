using System;
using Newtonsoft.Json;

namespace NGC.Models
{
    public class CountryCode
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dial_code")]
        public string DialCode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonIgnore]
        public string PickerDisplay { get { return Name + " - " + DialCode; } }
    }
}
