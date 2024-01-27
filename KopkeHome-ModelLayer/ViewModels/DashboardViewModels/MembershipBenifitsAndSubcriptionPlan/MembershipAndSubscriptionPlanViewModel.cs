using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels.MembershipBenifitsAndSubcriptionPlan
{
    public class MembershipAndSubscriptionPlanViewModel : BaseViewModel
    {
        //public List<MembershipPlanViewmodel> MembershipBenifitsList { get; set; }
        //public MembershipPlanFromStrip MembershipBenifitsDetails { get; set; }
        public UserMembershipSubscriptions SubscriptionsStripeData { get; set; }
        public List<MembershipPlanViewmodel> Plans { get; set; }
        public string PriceMonthlySilverID { get; set; }
        public decimal PriceMonthlySilver { get; set; }
        public string PriceYearlySilverID { get; set; }

        public string PriceMonthlyGoldID { get; set; }
        public string PriceYearlyGoldID { get; set; }

        public string PriceMonthlyBronzeID { get; set; }
        public string PriceYearlyBronzeID { get; set; }
        public CustomZipcodesRequest CustomPlan { get; set; }
    }
}
