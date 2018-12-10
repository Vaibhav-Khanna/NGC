using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class Checkin : BaseDataObject
    {
        [JsonProperty("doneAt")]
        public DateTimeOffset DoneAt { get; set; }

        [JsonProperty("checkinTypeId")]
        public string CheckinTypeId { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
