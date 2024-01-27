using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
    public class UpdateBasicInfoRequestModel
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("roleId")]
        public int RoleId;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("phoneNumberOffice")]
        public string PhoneNumberOffice;

        [JsonProperty("phoneNumber")]
        public string PhoneNumber;

        [JsonProperty("businessName")]
        public string BusinessName;

        [JsonProperty("city")]
        public string City;

        [JsonProperty("businessAddress")]
        public string BusinessAddress;

        [JsonProperty("zipCode")]
        public string ZipCode;

        [JsonProperty("state")]
        public string State;

        [JsonProperty("profilePicture")]
        public string ProfilePicture;
    }
}
