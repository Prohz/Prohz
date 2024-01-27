using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.ViewModels.DashboardViewModels.BusinessProfileModels
{
#nullable disable
    public class EditBusinessProfileOfContractor : BaseViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }



        [Required(ErrorMessage = "Business Profile Description is required")]
        public string BusinessDescription { get; set; }

        [Required(ErrorMessage = "Business Years is required")]
        public string YearsInBusiness { get; set; }




        public bool IsCompanyWebsite { get; set; }

        [Required(ErrorMessage = "Company Website URL is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid URL")]
        public string CompanyWebsiteURL { get; set; }

        [Required(ErrorMessage = "Facebook Page is required")]
        public bool IsFacebookPage { get; set; }

        [Required(ErrorMessage = "Facebook Page URL is required")]
        public string FacebookPageURL { get; set; }
        public bool CommercialLocation { get; set; }
        [Required(ErrorMessage = "Number Of Employees is required")]
        public string NumberOfEmployees { get; set; }
        [Required(ErrorMessage = "JobSiteCrews is required")]
        public string JobSiteCrews { get; set; }
        [Required(ErrorMessage = "Phone Call Support is required")]
        public bool IsPhoneCallSupport { get; set; }
        public string NormalBusinessHours { get; set; }
        [Required(ErrorMessage = "24 Hours Phone Answering is required")]
        public bool Is24HoursPhoneAnswering { get; set; }
        [Required(ErrorMessage = "Offer Emergency Services is required")]
        public bool IsOfferEmergencyServices { get; set; }
        [Required(ErrorMessage = "Business Or Trade License is required")]
        public bool IsBusinessOrTradeLicense { get; set; }



        public string ExistingBusinessOrTradeLicenseFiles { get; set; }

        public List<IFormFile> BusinessOrTradeLicenseFiles { get; set; }

        [Required(ErrorMessage = "Liability Insurance is required")]
        public bool IsLiabilityInsurance { get; set; }

        public string ExistingLiabilityInsuranceFile { get; set; }

        public IFormFile LiabilityInsuranceFile { get; set; }
        [Required(ErrorMessage = "Workman Compensation Insurance is required")]
        public bool IsWorkmanCompensationInsurance { get; set; }

        public string ExistingWorkmanCompensationInsuranceFile { get; set; }
        public IFormFile WorkmanCompensationInsuranceFile { get; set; }
        [Required(ErrorMessage = "Cash is required")]
        public bool IsCash { get; set; }

        public int MC { get; set; }

        public int Visa { get; set; }

        public int AmEx { get; set; }

        public int OtherCreditCard { get; set; }
        public string ExistingProfilePicture { get; set; }

        public IFormFile ProfilePicture { get; set; }
        public string MyPrIsFreeEstimatesoperty { get; set; }
        [Required(ErrorMessage = "Estimate Charge is required")]
        public bool IsEstimateCharge { get; set; }
        [Required(ErrorMessage = "Estimate Charge is required")]
        public string EstimateCharge { get; set; }
        [Required(ErrorMessage = "Design Services is required")]
        public bool IsDesignServices { get; set; }
        [Required(ErrorMessage = "Design Services charge is required")]
        public string DesignServices { get; set; }
        [Required(ErrorMessage = "Contacted By Homeowners is required")]
        public bool IsContactedByHomeowners { get; set; }
        [Required(ErrorMessage = "Contacted By Other contractor is required")]
        public bool IsContactedBySubcontractors { get; set; }
        //[Required(ErrorMessage = "Service Call Charge is required")]
        public bool IsContactedByContractors { get; set; }
        [Required(ErrorMessage = "Service Call Charge is required")]
        public string ServiceCallCharge { get; set; }
        [Required(ErrorMessage = "Which Payment App is required")]
        public string WhichPaymentApps { get; set; }
        public bool IsPaymentApps { get; set; }
        public bool PersonalChecks { get; set; }
        public bool CommercialLocationContractor { get; set; }



    }
}
