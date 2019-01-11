using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class Contact : BaseDataObject
    {

        [JsonProperty("contactCreatedAt")]
        public string ContactCreatedAt { get; set; }

        [JsonProperty("contactLastUpdateAt")]
        public string ContactLastUpdateAt { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("companyId")]
        public string CompanyId { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyStreet1")]
        public string CompanyStreet1 { get; set; }

        [JsonProperty("companyStreet2")]
        public string CompanyStreet2 { get; set; }

        [JsonProperty("companyZipCode")]
        public string CompanyZipCode { get; set; }

        [JsonProperty("companyCity")]
        public string CompanyCity { get; set; }

        [JsonProperty("companyState")]
        public string CompanyState { get; set; }

        [JsonProperty("companyCountry")]
        public string CompanyCountry { get; set; }

        [JsonProperty("companyLongitude")]
        public double CompanyLongitude { get; set; }

        [JsonProperty("companyLatitude")]
        public double CompanyLatitude { get; set; }

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
        public long Longitude { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("placeId")]
        public string PlaceId { get; set; }

        [JsonProperty("placeLocation")]
        public string PlaceLocation { get; set; }

        [JsonProperty("qualification")]
        public string Qualification { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("web")]
        public string Web { get; set; }

        [JsonProperty("allowCheckin")]
        public bool AllowCheckin { get; set; }

        [JsonProperty("lastCheckinAt")]
        public DateTimeOffset LastCheckinAt { get; set; }

        [JsonProperty("collectSourceId")]
        public string CollectSourceId { get; set; }

        [JsonProperty("collectSourceName")]
        public string CollectSourceName { get; set; }

        [JsonProperty("collectSourceChangedAt")]
        public string CollectSourceChangedAt { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("isBtoBDisplay")]
        public bool IsBtoBDisplay { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userFirstname")]
        public string UserFirstname { get; set; }

        [JsonProperty("userLastname")]
        public string UserLastname { get; set; }

        [JsonProperty("creatorUserId")]
        public string CreatorUserId { get; set; }

        [JsonProperty("creatorUserFirstname")]
        public string CreatorUserFirstname { get; set; }

        [JsonProperty("creatorUserLastname")]
        public string CreatorUserLastname { get; set; }

        [JsonProperty("salesTeamId")]
        public string SalesTeamId { get; set; }

        [JsonProperty("salesTeamName")]
        public string SalesTeamName { get; set; }

        [JsonProperty("displayPreference")]
        public string DisplayPreference { get; set; }

        [JsonProperty("firstCheckinDuration")]
        public long FirstCheckinDuration { get; set; }

        [JsonProperty("secondCheckinDuration")]
        public long SecondCheckinDuration { get; set; }

        [JsonProperty("importVersion")]
        public string ImportVersion { get; set; }

        [JsonProperty("linkedinLastConnectionDate")]
        public string LinkedinLastConnectionDate { get; set; }

        [JsonProperty("customFields")]
        public string CustomFields { get; set; }

        [JsonProperty("isSequenceInProgress")]
        public bool IsSequenceInProgress { get; set; }

        [JsonProperty("lastSequenceDate")]
        public string LastSequenceDate { get; set; }

        [JsonProperty("civility")]
        public string Civility { get; set; }

        [JsonProperty("photoUrl")]
        public string PhotoUrl { get; set; }

        [JsonProperty("linkedinProfileUrl")]
        public string LinkedinProfileUrl { get; set; }

        [JsonIgnore]
        public string Name => $"{Firstname} {Lastname}";

        [JsonIgnore]
        public string Address
        {
            get
            {
                string s = $"{Street1} {Street2} {City} {State} {Country} {ZipCode}";
                return string.IsNullOrWhiteSpace(s) ? "N.A" : s;
            }   
        }

    }
}
