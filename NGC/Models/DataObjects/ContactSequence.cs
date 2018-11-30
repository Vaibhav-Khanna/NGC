﻿using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class ContactSequence : BaseDataObject
    {
        [JsonProperty("sequenceId")]
        public string SequenceId { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("endReason")]
        public string EndReason { get; set; }

        [JsonProperty("manualEndingUserId")]
        public string ManualEndingUserId { get; set; }

        [JsonProperty("endAt")]
        public DateTimeOffset? EndAt { get; set; }
    }
}
