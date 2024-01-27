

using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels
{
#nullable disable
    public class ChangePasswordViewModel : BaseViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [StringLength(16, ErrorMessage = "Password must be between 8 to 16 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password do not match with new password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
