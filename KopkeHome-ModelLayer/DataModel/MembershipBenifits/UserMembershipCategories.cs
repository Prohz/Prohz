using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel.MembershipBenifits
{
#nullable disable
    [Table("UserMembershipCategories")]
    public class UserMembershipCategories
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int CategoriesId { get; set; }
        [ForeignKey("CategoriesId")]
        public Categories Categories { get; set; }
        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public MembershipBenifitsPlan MembershipBenifitsPlan { get; set; }
    }
}
