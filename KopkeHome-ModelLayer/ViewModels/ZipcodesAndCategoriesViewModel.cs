using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;

namespace KopkeHome_ModelLayer.ViewModels
{
    public class ZipcodesAndCategoriesViewModel : BaseViewModel
    {
        public List<ZipCode> ZipCodes { get; set; }
        public List<Categories> Categories { get; set; }
        public List<City> City { get; set; }
        public List<State> States { get; set; }
        public List<UserMembershipCategories> UserMembershipCategories { get; set; }
        public List<UserMembershipZipcodes> UserMembershipZipcodes { get; set; }
        public int LimitZipCodes { get; set; }
        public int LimitCategories { get; set; }
        public int PlanId { get; set; }
    }
}
