using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    [Table("UsersZipcodesAndCategories")]
    public class MembershipBenefits
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ZipCodeId { get; set; }


        [ForeignKey("ZipCodeId")]
        public ZipCode ZipCode { get; set; }


        public int CategoriesId { get; set; }
        [ForeignKey("CategoriesId")]
        public Categories Categories { get; set; }

        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public MembershipBenifitsPlan MembershipBenifitsPlan { get; set; }

    }
}
