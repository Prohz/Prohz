using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class UpdateContractorImageResponseModel
    {
        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("contractorId")]
        public int contractorId { get; set; }

        [JsonProperty("isLiked")]
        public bool isLiked { get; set; }

        [JsonProperty("isDisLiked")]
        public bool isDisLiked { get; set; }
    }
}
