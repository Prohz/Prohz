using KopkeHome_ModelLayer.DataModel;


namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels
{
#nullable disable
    public class ContractorProfileDetailsViewModel : BaseViewModel
    {

        public User User { get; set; }
        public BusinessProfileDataModel BusinessProfileContractor { get; set; }
        public BusinessProfileSubContractor SubContractorBusinessProfile { get; set; }
        public List<Categories> Categories { get; set; }

        public List<ZipCode> Zipcodes { get; set; }
        public string WorkGallery { get; set; }

        public MembershipPlanViewmodelApp SubscriptinDetails { get; set; }
    }
}
