using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
   public class GetUserByIdRequestModel
    {
       
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
