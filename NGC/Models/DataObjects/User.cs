using System;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class User : BaseDataObject
    {
        [JsonProperty("invitationAcceptedAt")]
        public DateTime? InvitationAcceptedAt { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("planId")]
        public string PlanId { get; set; }

        [JsonProperty("planKey")]
        public string PlanKey { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("tempPassword")]
        public string TempPassword { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("phoneMobile")]
        public string PhoneMobile { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("mailKind")]
        public string MailKind { get; set; }

        [JsonProperty("mailConfigurationLogin")]
        public string MailConfigurationLogin { get; set; }

        [JsonProperty("mailConfigurationPassword")]
        public string MailConfigurationPassword { get; set; }

        [JsonProperty("imapHost")]
        public string ImapHost { get; set; }

        [JsonProperty("imapPort")]
        public string ImapPort { get; set; }

        [JsonProperty("smtpHost")]
        public string SmtpHost { get; set; }

        [JsonProperty("smtpPort")]
        public string SmtpPort { get; set; }

        [JsonProperty("tls")]
        public bool Tls { get; set; }

        [JsonProperty("nextCheckImap")]
        public string NextCheckImap { get; set; }

        [JsonProperty("androidOneSignalID")]
        public string AndroidOneSignalId { get; set; }

        [JsonProperty("iosOneSignalID")]
        public string IosOneSignalId { get; set; }

        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [JsonIgnore]
        public string Name => $"{Firstname} {Lastname}";
    }
}
