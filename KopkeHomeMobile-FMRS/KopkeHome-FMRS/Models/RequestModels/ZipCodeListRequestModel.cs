using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
  public  class ZipCodeListRequestModel
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
