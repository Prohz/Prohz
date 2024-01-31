
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_WebApp.Models
{
#pragma warning disable
    public class EmailFormModel : BaseViewModel
    {
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
