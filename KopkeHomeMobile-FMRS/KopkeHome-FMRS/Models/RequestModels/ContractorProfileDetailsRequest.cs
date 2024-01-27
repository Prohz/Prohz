using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
    public class ContractorProfileDetailsRequest
    {

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
