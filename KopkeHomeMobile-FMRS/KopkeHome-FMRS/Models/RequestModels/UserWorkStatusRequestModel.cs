using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
  public  class UserWorkStatusRequestModel
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("radioWorkStatus")]
        public int RadioWorkStatus { get; set; }
    }
}
