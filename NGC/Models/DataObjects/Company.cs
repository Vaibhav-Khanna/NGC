using System;
using Newtonsoft.Json;
using System.Linq;

namespace NGC.Models.DataObjects
{
    public class Company : BaseDataObject
    {
        [JsonProperty("companyCreatedAt")]
        public string CompanyCreatedAt { get; set; }

        [JsonProperty("companyLastUpdateAt")]
        public string CompanyLastUpdateAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("street1")]
        public string Street1 { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("web")]
        public string Web { get; set; }

        [JsonProperty("collectSourceId")]
        public string CollectSourceId { get; set; }

        [JsonProperty("collectSourceName")]
        public string CollectSourceName { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userFirstname")]
        public string UserFirstname { get; set; }

        [JsonProperty("userLastname")]
        public string UserLastname { get; set; }

        [JsonProperty("siret")]
        public string Siret { get; set; }

        [JsonProperty("statutJuridique")]
        public string StatutJuridique { get; set; }

        [JsonProperty("ape")]
        public string Ape { get; set; }

        [JsonProperty("apeLibelle")]
        public string ApeLibelle { get; set; }

        [JsonProperty("apeSousClasse")]
        public string ApeSousClasse { get; set; }

        [JsonProperty("effectif")]
        public string Effectif { get; set; }

        [JsonIgnore]
        public string Address
        {
            get
            {
                string s = $"{Street1} {Street2} {City} {State} {Country} {ZipCode}";
                return string.IsNullOrWhiteSpace(s) ? "N.A" : s;
            }
            set
            {
                var char_sets = value?.Trim().Split(',');

                if (char_sets!=null && char_sets.Any())
                {

                }
            }
        }

    }
}
