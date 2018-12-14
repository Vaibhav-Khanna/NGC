using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class Notification : BaseDataObject
    {

        [JsonProperty("databaseInsertAt")]
        public DateTime DatabaseInsertAt { get; set; }

        [JsonProperty("hasBeenReadAt")]
        public DateTime? HasBeenReadAt { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("referenceKind")]
        public string ReferenceKind { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("contactFirstname")]
        public string ContactFirstname { get; set; }

        [JsonProperty("contactLastname")]
        public string ContactLastname { get; set; }

        [JsonProperty("contactCompanyName")]
        public string ContactCompanyName { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("extra1")]
        public string Extra1 { get; set; }

        [JsonIgnore]
        public string DatabaseInsertAtMonth => DatabaseInsertAt.ToString("MMMM");

    }
}
