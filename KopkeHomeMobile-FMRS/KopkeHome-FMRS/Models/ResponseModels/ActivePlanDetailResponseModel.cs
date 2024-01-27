using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class ActivePlanDetailResponseModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("numberOfZipcodes")]
        public int? NumberOfZipcodes { get; set; }

        [JsonProperty("descrption")]
        public string Descrption { get; set; }

        [JsonProperty("numberOfCategories")]
        public int? NumberOfCategories { get; set; }

        [JsonProperty("mobileApp")]
        public bool? MobileApp { get; set; }

        [JsonProperty("webApp")]
        public bool? WebApp { get; set; }

        [JsonProperty("isYearly")]
        public bool IsYearly { get; set; }

        [JsonProperty("priceMonthly")]
        public int? PriceMonthly { get; set; }

        [JsonProperty("priceYearly")]
        public int? PriceYearly { get; set; }

        [JsonProperty("stripePriceMonthly")]
        public string StripePriceMonthly { get; set; }

        [JsonProperty("stripePriceYearly")]
        public string StripePriceYearly { get; set; }

        [JsonProperty("isPlanCreated")]
        public bool? IsPlanCreated { get; set; }
    }
}
