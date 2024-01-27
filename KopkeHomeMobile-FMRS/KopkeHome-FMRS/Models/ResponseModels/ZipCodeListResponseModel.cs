using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
  
    public class ZipCodeListResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cityId")]
        public int CityId { get; set; }

        [JsonProperty("city")]
        public object City { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
    }
}
