using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel.MembershipBenifits
{
#nullable disable
    [Table("CustomMembershipPlanRequest")]
    public class CustomZipcodesRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NumberOfZipcodes { get; set; }
        [StringLength(1000)]
        public string Descrption { get; set; }
        public int NumberOfCategories { get; set; }
        public bool MobileApp { get; set; }
        public bool WebApp { get; set; }
        public bool IsYearly { get; set; }
        public double PriceMonthly { get; set; }
        public double PriceYearly { get; set; }

        [StringLength(300)]
        public string StripePriceMonthly { get; set; }
        [StringLength(300)]
        public string StripePriceYearly { get; set; }
        public bool IsPlanCreated { get; set; }
    }
}
