using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models
{
    public class ZipListModel
    {
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
        public Command ZipcodeSelectedCommand { get; set; } 
    }
}
