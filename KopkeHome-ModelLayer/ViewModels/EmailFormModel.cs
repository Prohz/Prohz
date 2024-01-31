using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.ViewModels
{
#pragma warning disable
    public class EmailFormModel : BaseViewModel
    {
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
