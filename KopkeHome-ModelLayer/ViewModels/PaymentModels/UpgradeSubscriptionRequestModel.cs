namespace KopkeHome_ModelLayer.ViewModels.PaymentModels
{
    public class UpgradeSubscriptionRequestModel
    {
        public string StripesubId { get; set; }
        public string StripeCusId { get; set; }
        public string StripePriceId { get; set; }
        public string PlanId { get; set; }
    }
}
