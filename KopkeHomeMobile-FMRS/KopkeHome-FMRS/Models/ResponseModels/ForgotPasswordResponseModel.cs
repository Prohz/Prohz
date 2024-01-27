using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class  ForgotPasswordResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statuscode")]
        public int Statuscode { get; set; }

        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorDetails")]
        public object ErrorDetails { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

    }
}
