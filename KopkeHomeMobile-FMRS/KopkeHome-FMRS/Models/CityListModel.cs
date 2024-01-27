using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models
{
    public class CityListModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("stateId")]
        public int StateId { get; set; }

        [JsonProperty("state")]
        public object State { get; set; }
        public Command CitySelectedCommand { get; set; }
    }
}
