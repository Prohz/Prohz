using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class Dashboard : BaseViewModel
    {
        public List<User> Users { get; set; }
        public User CurrentUser { get; set; }
        public List<ContractorListViewModel> ContractorList { get; set; }
    }
}
