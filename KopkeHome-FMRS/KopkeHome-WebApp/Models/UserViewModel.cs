using KopkeHome_ModelLayer.DataModel;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_WebApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }



        [Required(ErrorMessage = "First Name is required")]

        [StringLength(51, ErrorMessage = "Maximum 50 characters limit exceeded")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(51, ErrorMessage = "Maximum 50 characters limit exceeded")]
        public string LastName { get; set; }

        [StringLength(12, ErrorMessage = "Enter a valid Phone Number", MinimumLength = 12)]
        [Required(ErrorMessage = "Phone number office is required.")]
        public string PhoneNumberOffice { get; set; }

        [StringLength(12, ErrorMessage = "Enter a valid Phone Number", MinimumLength = 12)]
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
        //[RegularExpression("True|true", ErrorMessage = "At least one field must be given a value")]

        //public bool Any => PhoneNumber != null || PhoneNumberOffice != null;

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Business Name is required")]
        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")]
        public string BusinessName { get; set; }

        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")]
        public string SalesAssociate { get; set; }
        [StringLength(10, ErrorMessage = "Enter a valid member id", MinimumLength = 10)]
        public string MemberReferralId { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40, ErrorMessage = "City is required")]

        public string City { get; set; }

        [Required(ErrorMessage = "Business Address is required")]
        public string BusinessAddress { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [Display(Name = "Zip Code")]
        [StringLength(5, ErrorMessage = "Enter a valid Zip Code", MinimumLength = 5)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")]
        public string State { get; set; }

        //public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,20}$", ErrorMessage = "Password must contain at least one upper case letter, one lower case letter, one digit value and one special character.")]
        [StringLength(16, ErrorMessage = "Password must be between 8 to 16 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        public string MessageCode { get; set; }
        public string Message { get; set; }
        public bool IsRegistrationSuccess { get; set; }
        public List<State> States { get; set; }
        //  [Required(ErrorMessage = "Please accept Terms And Conditions")]
        public int TermAndConditions { get; set; }
        [Required(ErrorMessage = "Please mention how did you hear about prohz")]
        public int HeardAboutProhzFrom { get; set; }
    }
}
