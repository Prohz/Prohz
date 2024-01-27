using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class User : IdentityUser<int>
    {
        public bool isSccess;

        [Key]
        public override int Id { get; set; }


        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumberOffice { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public string City { get; set; }
        public string BusinessAddress { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public bool IsEmailVerified { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long UniqueMemberId { get; set; }
        public int? WorkStatus { get; set; } = 0;
        public DateTime WorkStatusModifiedOn { get; set; }
        public bool IsDocumentsVerified { get; set; }
        //[ForeignKey("HeardAboutProhzFrom")]
        //public HeardAboutProhz HeardAboutProhz { get; set; }
        public int HeardAboutProhzFrom { get; set; } = 1;
        public bool IsDeleted { get; set; }= false;


    }
}
