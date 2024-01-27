using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class ResponseModel
    {
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("error")]
        public bool error { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("errorDetails")]
        public object errorDetails { get; set; }
        [JsonProperty("data")]
        public string data { get; set; }
        [JsonProperty("code")]
        public int? code { get; set; }
    }

    public class CommonResponseModel<T>
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statuscode")]
        public int Statuscode { get; set; }

        [JsonProperty("error")]
        public bool error { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("errorDetails")]
        public object errorDetails { get; set; }
        public T Data { get; set; }
    }
}
