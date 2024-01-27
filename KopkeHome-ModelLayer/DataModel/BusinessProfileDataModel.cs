using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable
namespace KopkeHome_ModelLayer.DataModel
{
    public class BusinessProfileDataModel
    {
        public int Id { get; set; }


        public int UserId { get; set; }


        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [StringLength(1000)]
        public string BusinessDescription { get; set; }

        [StringLength(50)]
        public string YearsInBusiness { get; set; }
        public bool IsCompanyWebsite { get; set; }

        [StringLength(50)]
        public string CompanyWebsiteURL { get; set; }
        public bool IsFacebookPage { get; set; }

        [StringLength(50)]
        public string FacebookPageURL { get; set; }
        public bool CommercialLocation { get; set; }

        [StringLength(50)]
        public string NumberOfEmployees { get; set; }

        [StringLength(50)]
        public string JobSiteCrews { get; set; }
        public bool IsPhoneCallSupport { get; set; }

        [StringLength(100)]
        public string NormalBusinessHours { get; set; }
        public bool Is24HoursPhoneAnswering { get; set; }
        public bool IsOfferEmergencyServices { get; set; }
        public bool IsBusinessOrTradeLicense { get; set; }
        public bool IsLiabilityInsurance { get; set; }
        public bool IsWorkmanCompensationInsurance { get; set; }
        public bool IsCash { get; set; }





        public bool IsEstimateCharge { get; set; }
        [StringLength(50)]
        public string EstimateCharge { get; set; }
        public bool IsDesignServices { get; set; }

        [StringLength(50)]
        public string DesignServices { get; set; }
        public bool IsContactedByHomeowners { get; set; }
        public bool IsContactedBySubcontractors { get; set; }
        //public bool IsContactedByContractors { get; set; }


        //public string ServiceCallCharge { get; set; }

        [StringLength(250)]
        public string ProfilePicture { get; set; }
        [StringLength(200)]
        public string WorkmanCompensationInsuranceFile { get; set; }
        [StringLength(200)]
        public string LiabilityInsuranceFile { get; set; }
        [StringLength(2000)]
        public string BusinessOrTradeLicenseFiles { get; set; }


        public int MC { get; set; }
        public int Visa { get; set; }
        public int AmEx { get; set; }
        public int OtherCreditCard { get; set; }
        public bool IsPaymentApps { get; set; }
        public bool PersonalChecks { get; set; }
        [StringLength(50)]
        public string WhichPaymentApps { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        //public bool IsDocumentsVerified { get; set; }
        public int VerificationStatus { get; set; }

    }
}
