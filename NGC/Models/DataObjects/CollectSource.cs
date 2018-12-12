﻿using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class CollectSource : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayOrder")]
        public long DisplayOrder { get; set; }
    }
}
