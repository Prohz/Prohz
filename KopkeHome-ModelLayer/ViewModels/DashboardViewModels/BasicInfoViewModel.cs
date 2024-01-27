using KopkeHome_ModelLayer.DataModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels
{
#nullable disable
    public class BasicInfoViewModel : BaseViewModel
    {
        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public List<State> States { get; set; }






        public int Id { get; set; }


        public int RoleId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Business Name is required")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Business Address is required")]
        public string BusinessAddress { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^([0-9]{5})$", ErrorMessage = "Please enter a valid Zip Code")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        public IFormFile ProfilePicture { get; set; }
        [StringLength(12, ErrorMessage = "Enter valid Phone Number", MinimumLength = 12)]

        //[RegularExpression(@"^([0-9]{12})$", ErrorMessage = "Invalid Phone No")]
        public string PhoneNumberOffice { get; set; }
        //[Required(ErrorMessage = "Mobile Phone Number is required")]
        //[RegularExpression(@"^([0-9]{12})$", ErrorMessage = "Invalid Phone No")]
        [StringLength(12, ErrorMessage = "Enter valid Phone Number", MinimumLength = 12)]
        public string PhoneNumber { get; set; }

    }

    public class UpdateBasicInfo
    {
        public int Id { get; set; }


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

        public string ProfilePicture { get; set; }
        public IFormFile ProfilePictureApp { get; set; }
    }

    public class UpdateBasicInfoHomeOwner
    {
        public int Id { get; set; }


        public int RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumberOffice { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }


        public string ZipCode { get; set; }

        public string State { get; set; }

        public string ProfilePicture { get; set; }
        public IFormFile ProfilePictureApp { get; set; }
    }

    public class ImageModel
    {
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Encryptedfilename { get; set; }
    }
}
