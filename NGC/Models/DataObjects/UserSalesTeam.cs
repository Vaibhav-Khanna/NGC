using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class UserSalesTeam : BaseDataObject
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("salesTeamId")]
        public string SalesTeamId { get; set; }
    }
}
