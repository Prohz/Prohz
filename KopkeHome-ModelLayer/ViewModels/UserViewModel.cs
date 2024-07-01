using KopkeHome_ModelLayer.DataModel;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace KopkeHome_ModelLayer.ViewModels
{
    public class HomeOwnerViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstNameHo { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastNameHo { get; set; }
        [StringLength(12, ErrorMessage = "Enter a valid Phone Number", MinimumLength = 12)]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone No")]
        public string PhoneNumberOfficeHo { get; set; }

        [Required(ErrorMessage = "Mobile Phone Number is required")]
        [StringLength(12, ErrorMessage = "Enter a valid Phone Number", MinimumLength = 12)]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone No")]
        public string PhoneNumberHo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email not valid")]                
        public string EmailHo { get; set; }

        //[Required(ErrorMessage = "Business Name is required")]
        //public string BusinessName { get; set; }
        [StringLength(50, ErrorMessage = "Maximum {2} characters exceeded")]
        public string SalesAssociateHo { get; set; }
        [StringLength(10, ErrorMessage = "Enter a valid member id", MinimumLength = 10)]
        public string MemberReferralIdHo { get; set; }
        [StringLength(10, ErrorMessage = "Enter a valid member id", MinimumLength = 10)]
        public string CityHo { get; set; }


        public string BusinessAddressHo { get; set; }


        public string ZipCodeHo { get; set; }


        public string StateHo { get; set; }

        //public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,20}$", ErrorMessage = "Password should contain at least one capital letter, upper case, lower case letter, numeric value and one special character.")]
        [StringLength(16, ErrorMessage = "Password must be between 8 to 16 characters", MinimumLength = 8)]

        [DataType(DataType.Password)]
        public string PasswordHo { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("PasswordHo", ErrorMessage = "Password and Confirm password do not match.")]
        public string ConfirmPasswordHo { get; set; }

        public string MessageCodeHo { get; set; }
        public string MessageHo { get; set; }
        public bool IsRegistrationSuccessHo { get; set; }
        //  [Required(ErrorMessage = "Please accept Terms And Conditions")]
        public int TermAndConditions { get; set; }

        public int HeardAboutProhzFromHo { get; set; }

    }

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
        public string PhoneNumberOffice { get; set; }

        [StringLength(12, ErrorMessage = "Enter a valid Phone Number", MinimumLength = 12)]

        public string PhoneNumber { get; set; }
        //[RegularExpression("True|true", ErrorMessage = "At least one field must be given a value")]

        //public bool Any => PhoneNumber != null || PhoneNumberOffice != null;

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email ")]
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

        public string HomeValue { get; set; }

        [Required(ErrorMessage = "Please mention if you are using a secret code or not")]
        public bool SecretCodeNeeded { get; set; }
    }
    public class UserResponse : Response
    {
        public User user { get; set; }
    }

}
