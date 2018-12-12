using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{

    public class Authentification
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("tokenExpiration")]
        public long TokenExpiration { get; set; }
    }

}
