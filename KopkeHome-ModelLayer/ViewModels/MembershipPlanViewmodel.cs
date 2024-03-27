using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.ViewModels
{
    public class MembershipPlanViewmodel
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public int RoleId { get; set; }



        public string Categories { get; set; }


        public string ZipCodes { get; set; }


        public double PricePerMonth { get; set; }

        public double PricePerYear { get; set; }

        public bool PhoneApp { get; set; }
        public bool Website { get; set; }

        public string MonthlyStripePriceId { get; set; }
        public string AnnuallyStripePriceId { get; set; }

    }

    public class MembershipPlanFromStrip : BaseViewModel
    {

        public List<MembershipPlanViewmodel> Plans { get; set; }
        public string PriceMonthlySilverID { get; set; }
        public decimal PriceMonthlySilver { get; set; }
        public string PriceYearlySilverID { get; set; }

        public string PriceMonthlyGoldID { get; set; }
        public string PriceYearlyGoldID { get; set; }

        public string PriceMonthlyBronzeID { get; set; }
        public string PriceYearlyBronzeID { get; set; }

        public CustomZipcodesRequest CustomPlan { get; set; }
        public int priceYearlySilver { get; set; }
    }
    public class MembershipPlanViewmodelApp
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Categories { get; set; }

        public string ZipCodes { get; set; }

        public int Days { get; set; }

        public double Price { get; set; }
        public bool PhoneApp { get; set; }
        public bool Website { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public string StripePriceId { get; set; }
        public string StripePriceIdAnu { get; set; }
        public string StripePriceIdmon { get; set; }
        public string PaymentStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceUrl { get; set; }
        public DateTime CancelledOn { get; set; }
        public DateTime UpgradedOn { get; set; }
        public DateTime DowngradedOn { get; set; }
        public bool IsActive { get; set; }
        //public CustomZipcodesRequest CustomPlanDetails { get; set; }
        
    }
}
