using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class UpdateBasicInfoResoponseModel
    {

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statuscode")]
        public int? Statuscode { get; set; }

        [JsonProperty("error")]
        public bool? Error { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorDetails")]
        public object ErrorDetails { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

    }
        public class Data
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("roleId")]
            public int? RoleId { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("phoneNumberOffice")]
            public string PhoneNumberOffice { get; set; }

            [JsonProperty("phoneNumber")]
            public string PhoneNumber { get; set; }

            [JsonProperty("businessName")]
            public string BusinessName { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("businessAddress")]
            public string BusinessAddress { get; set; }

            [JsonProperty("zipCode")]
            public string ZipCode { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("isEmailVerified")]
            public bool? IsEmailVerified { get; set; }

            [JsonProperty("profilePicture")]
            public string ProfilePicture { get; set; }

            [JsonProperty("createdOn")]
            public DateTime? CreatedOn { get; set; }

            [JsonProperty("modifiedOn")]
            public DateTime? ModifiedOn { get; set; }

            [JsonProperty("uniqueMemberId")]
            public long? UniqueMemberId { get; set; }

            [JsonProperty("workStatus")]
            public int? WorkStatus { get; set; }

            [JsonProperty("workStatusModifiedOn")]
            public DateTime? WorkStatusModifiedOn { get; set; }

            [JsonProperty("isDocumentsVerified")]
            public bool? IsDocumentsVerified { get; set; }

            [JsonProperty("heardAboutProhzFrom")]
            public int? HeardAboutProhzFrom { get; set; }

            [JsonProperty("userName")]
            public string UserName { get; set; }

            [JsonProperty("normalizedUserName")]
            public string NormalizedUserName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("normalizedEmail")]
            public string NormalizedEmail { get; set; }

            [JsonProperty("emailConfirmed")]
            public bool? EmailConfirmed { get; set; }

            [JsonProperty("passwordHash")]
            public string PasswordHash { get; set; }

            [JsonProperty("securityStamp")]
            public string SecurityStamp { get; set; }

            [JsonProperty("concurrencyStamp")]
            public string ConcurrencyStamp { get; set; }

            [JsonProperty("phoneNumberConfirmed")]
            public bool? PhoneNumberConfirmed { get; set; }

            [JsonProperty("twoFactorEnabled")]
            public bool? TwoFactorEnabled { get; set; }

            [JsonProperty("lockoutEnd")]
            public object LockoutEnd { get; set; }

            [JsonProperty("lockoutEnabled")]
            public bool? LockoutEnabled { get; set; }

            [JsonProperty("accessFailedCount")]
            public int? AccessFailedCount { get; set; }
        }

       
    
}
