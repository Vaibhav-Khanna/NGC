using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class Note : BaseDataObject
    {
        [JsonProperty("doneAt")]
        public DateTime? DoneAt { get; set; }

        [JsonProperty("databaseInsertAt")]
        public DateTime DatabaseInsertAt { get; set; }

        [JsonProperty("reminderAt")]
        public DateTime? ReminderAt { get; set; }

        [JsonProperty("activityStreamDate")]
        public DateTime ActivityStreamDate { get; set; }

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

        [JsonProperty("contactQualification")]
        public string ContactQualification { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("extra1")]
        public string Extra1 { get; set; }

        [JsonProperty("extra2")]
        public string Extra2 { get; set; }

        [JsonProperty("contentHasRGPDData")]
        public bool ContentHasRgpdData { get; set; }

        [JsonIgnore]
        public string UserName => $"{UserFirstname} {UserLastname}";
    }
}
