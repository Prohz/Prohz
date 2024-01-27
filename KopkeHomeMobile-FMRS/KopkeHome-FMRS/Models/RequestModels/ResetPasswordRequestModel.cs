using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
    public class ResetPasswordRequestModel
    {
         
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }


    }

}

