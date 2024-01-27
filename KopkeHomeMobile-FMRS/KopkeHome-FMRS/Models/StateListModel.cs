using KopkeHome_FMRS.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models
{
    public partial class StateListModel:BaseViewModel
    {
        
        [JsonProperty("stateId")]
        public int? StateId { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }

        [JsonProperty("countryId")]
        public int? CountryId { get; set; }

        [JsonProperty("usaStateCode")]
        public string UsaStateCode { get; set; }
        public Command StateSelectedCommand { get;set; }
    }

}

