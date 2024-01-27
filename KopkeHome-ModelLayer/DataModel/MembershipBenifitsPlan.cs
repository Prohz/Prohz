using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class MembershipBenifitsPlan
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int RoleId { get; set; }
        [JsonIgnore]
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [StringLength(50)]
        public string Categories { get; set; }

        [StringLength(50)]
        public string ZipCodes { get; set; }

        [Column(TypeName = "money")]
        public double PricePerMonth { get; set; }
        [Column(TypeName = "money")]
        public double PricePerYear { get; set; }
        public bool Website { get; set; }
        public bool PhoneApp { get; set; }

        public string MonthlyStripePriceId { get; set; }
        public string AnnuallyStripePriceId { get; set; }
    }
}
