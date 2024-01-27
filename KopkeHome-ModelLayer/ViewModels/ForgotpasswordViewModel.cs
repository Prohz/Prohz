using System.ComponentModel.DataAnnotations;
namespace KopkeHome_ModelLayer.ViewModels
{
#nullable disable
    public class ForgotpasswordViewModel
    {

        public string Email { get; set; }
        public string Token { get; set; }

        [StringLength(16, ErrorMessage = "Password must be between 8 to 16 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
