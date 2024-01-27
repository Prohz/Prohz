

namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels
{
#nullable disable
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
