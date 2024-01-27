using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.RequestModels
{
    public class SearchContractorsListRequest
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("categories")]
        public string Categories { get; set; }
    }
}
