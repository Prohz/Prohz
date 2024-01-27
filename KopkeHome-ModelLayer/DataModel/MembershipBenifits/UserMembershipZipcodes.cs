using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel.MembershipBenifits
{
#nullable disable
    [Table("UserMembershipZipcodes")]
    public class UserMembershipZipcodes
    {
        public int Id { get; set; }
        public int UserId { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        public string ZipCodeId { get; set; }
       // public int ZipCodeId { get; set; }


        //[ForeignKey("ZipCodeId")]
        //public ZipCode ZipCode { get; set; }
        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public MembershipBenifitsPlan MembershipBenifitsPlan { get; set; }

    }
}
