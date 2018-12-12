using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class Opportunity : BaseDataObject
    {
        [JsonProperty("doneAt")]
        public DateTime? DoneAt { get; set; }

        [JsonProperty("databaseInsertAt")]
        public DateTime? DatabaseInsertAt { get; set; }

        [JsonProperty("reminderAt")]
        public DateTime? ReminderAt { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("opportunityStepId")]
        public string OpportunityStepId { get; set; }

        [JsonProperty("opportunityStepName")]
        public string OpportunityStepName { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userLastname")]
        public string UserLastname { get; set; }

        [JsonProperty("userFirstname")]
        public string UserFirstname { get; set; }

        [JsonProperty("contactFirstname")]
        public string ContactFirstname { get; set; }

        [JsonProperty("contactLastname")]
        public string ContactLastname { get; set; }

        [JsonProperty("contactCompanyName")]
        public string ContactCompanyName { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("contentHasRGPDData")]
        public bool ContentHasRgpdData { get; set; }

        [JsonIgnore]
        public string UserName => $"{UserFirstname} {UserLastname}";

    }
}
