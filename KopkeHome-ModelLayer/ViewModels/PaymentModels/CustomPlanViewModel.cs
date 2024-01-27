using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;

namespace KopkeHome_ModelLayer.ViewModels.PaymentModels
{
    public class CustomPlanViewModel
    {
        public int UserId { get; set; }
        public int NumberOfZipcodes { get; set; }

        public double Price { get; set; }

        public string Descrption { get; set; }
        public int NumberOfCategories { get; set; }
        public bool MobileApp { get; set; }
        public bool WebApp { get; set; }
        public bool IsYearly { get; set; }
        public int PriceMonthly { get; set; }

        public int PriceYearly { get; set; }
    }
    public class CustomPlanWebViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public int NumberOfZipcodes { get; set; }

        public string Descrption { get; set; }
        public int NumberOfCategories { get; set; }
        public bool MobileApp { get; set; }
        public bool WebApp { get; set; }
        public bool IsYearly { get; set; }
        public int Price { get; set; }

        public string StripePriceMonthly { get; set; }

        public string StripePriceYearly { get; set; }
    }
}
