using System;
using Newtonsoft.Json;
using NGC.Converter;
using NGC.Resources;

namespace NGC.Models.DataObjects
{
    public class Checkin : BaseDataObject
    {
        [JsonProperty("doneAt")]
        public DateTime? DoneAt { get; set; }

        [JsonProperty("checkinTypeId")]
        public string CheckinTypeId { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonIgnore]
        public string Subject { get; set; }

        [JsonIgnore]
        public string UserName { get; set; }

        public string DoneAtUserName => $"{new DateToStringConverter().Convert(DoneAt,typeof(string),null,AppResources.Culture)} | {UserName}";

    }
}
