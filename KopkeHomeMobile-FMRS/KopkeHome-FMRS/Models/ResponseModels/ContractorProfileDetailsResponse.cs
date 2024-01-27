using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models.ResponseModels
{
    public class ContractorProfileDetailsResponse
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("businessProfileContractor")]
        public BusinessProfileContractor BusinessProfileContractor { get; set; }

        [JsonProperty("subContractorBusinessProfile")]
        public SubContractorBusinessProfile SubContractorBusinessProfile { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("zipcodes")]
        public List<Zipcode> Zipcodes { get; set; }

        [JsonProperty("workGallery")]
        public object WorkGallery { get; set; }

        [JsonProperty("loggedInUser")]
        public object LoggedInUser { get; set; }
    }
    public class BusinessProfileContractor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("businessDescription")]
        public string BusinessDescription { get; set; }

        [JsonProperty("yearsInBusiness")]
        public string YearsInBusiness { get; set; }

        [JsonProperty("isCompanyWebsite")]
        public bool IsCompanyWebsite { get; set; }

        [JsonProperty("companyWebsiteURL")]
        public string CompanyWebsiteURL { get; set; }

        [JsonProperty("isFacebookPage")]
        public bool IsFacebookPage { get; set; }

        [JsonProperty("facebookPageURL")]
        public string FacebookPageURL { get; set; }

        [JsonProperty("commercialLocation")]
        public bool CommercialLocation { get; set; }

        [JsonProperty("numberOfEmployees")]
        public string NumberOfEmployees { get; set; }

        [JsonProperty("jobSiteCrews")]
        public string JobSiteCrews { get; set; }

        [JsonProperty("isPhoneCallSupport")]
        public bool IsPhoneCallSupport { get; set; }

        [JsonProperty("normalBusinessHours")]
        public string NormalBusinessHours { get; set; }

        [JsonProperty("is24HoursPhoneAnswering")]
        public bool Is24HoursPhoneAnswering { get; set; }

        [JsonProperty("isOfferEmergencyServices")]
        public bool IsOfferEmergencyServices { get; set; }

        [JsonProperty("isBusinessOrTradeLicense")]
        public bool IsBusinessOrTradeLicense { get; set; }

        [JsonProperty("isLiabilityInsurance")]
        public bool IsLiabilityInsurance { get; set; }

        [JsonProperty("isWorkmanCompensationInsurance")]
        public bool IsWorkmanCompensationInsurance { get; set; }

        [JsonProperty("isCash")]
        public bool IsCash { get; set; }

        [JsonProperty("isEstimateCharge")]
        public bool IsEstimateCharge { get; set; }

        [JsonProperty("estimateCharge")]
        public string EstimateCharge { get; set; }

        [JsonProperty("isDesignServices")]
        public bool IsDesignServices { get; set; }

        [JsonProperty("designServices")]
        public String DesignServices { get; set; }

        [JsonProperty("isContactedByHomeowners")]
        public bool IsContactedByHomeowners { get; set; }

        [JsonProperty("isContactedBySubcontractors")]
        public bool IsContactedBySubcontractors { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("workmanCompensationInsuranceFile")]
        public string WorkmanCompensationInsuranceFile { get; set; }

        [JsonProperty("liabilityInsuranceFile")]
        public string LiabilityInsuranceFile { get; set; }

        [JsonProperty("businessOrTradeLicenseFiles")]
        public string BusinessOrTradeLicenseFiles { get; set; }

        [JsonProperty("mc")]
        public int Mc { get; set; }

        [JsonProperty("visa")]
        public int Visa { get; set; }

        [JsonProperty("amEx")]
        public int AmEx { get; set; }

        [JsonProperty("otherCreditCard")]
        public int OtherCreditCard { get; set; }

        [JsonProperty("isPaymentApps")]
        public bool IsPaymentApps { get; set; }

        [JsonProperty("personalChecks")]
        public bool PersonalChecks { get; set; }

        [JsonProperty("whichPaymentApps")]
        public string WhichPaymentApps { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty("verificationStatus")]
        public int VerificationStatus { get; set; }
    }

    public class SubContractorBusinessProfile
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("businessDescription")]
        public string BusinessDescription { get; set; }

        [JsonProperty("yearsInBusiness")]
        public string YearsInBusiness { get; set; }

        [JsonProperty("isCompanyWebsite")]
        public bool IsCompanyWebsite { get; set; }

        [JsonProperty("companyWebsiteURL")]
        public string CompanyWebsiteURL { get; set; }

        [JsonProperty("isFacebookPage")]
        public bool IsFacebookPage { get; set; }

        [JsonProperty("facebookPageURL")]
        public string FacebookPageURL { get; set; }

        [JsonProperty("commercialLocationContractor")]
        public bool CommercialLocationContractor { get; set; }

        [JsonProperty("numberOfEmployees")]
        public string NumberOfEmployees { get; set; }

        [JsonProperty("jobSiteCrews")]
        public string JobSiteCrews { get; set; }

        [JsonProperty("isPhoneCallSupport")]
        public bool IsPhoneCallSupport { get; set; }

        [JsonProperty("normalBusinessHours")]
        public string NormalBusinessHours { get; set; }

        [JsonProperty("is24HoursPhoneAnswering")]
        public bool Is24HoursPhoneAnswering { get; set; }

        [JsonProperty("isOfferEmergencyServices")]
        public bool IsOfferEmergencyServices { get; set; }

        [JsonProperty("isBusinessOrTradeLicense")]
        public bool IsBusinessOrTradeLicense { get; set; }

        [JsonProperty("businessOrTradeLicenseFiles")]
        public string BusinessOrTradeLicenseFiles { get; set; }

        [JsonProperty("isLiabilityInsurance")]
        public bool IsLiabilityInsurance { get; set; }

        [JsonProperty("liabilityInsuranceFile")]
        public string LiabilityInsuranceFile { get; set; }

        [JsonProperty("isWorkmanCompensationInsurance")]
        public bool IsWorkmanCompensationInsurance { get; set; }

        [JsonProperty("workmanCompensationInsuranceFile")]
        public string WorkmanCompensationInsuranceFile { get; set; }

        [JsonProperty("isCash")]
        public bool IsCash { get; set; }

        [JsonProperty("mc")]
        public int Mc { get; set; }

        [JsonProperty("visa")]
        public int Visa { get; set; }

        [JsonProperty("amEx")]
        public int AmEx { get; set; }

        [JsonProperty("otherCreditCard")]
        public int OtherCreditCard { get; set; }

        [JsonProperty("personalChecks")]
        public bool PersonalChecks { get; set; }

        [JsonProperty("isPaymentApps")]
        public bool IsPaymentApps { get; set; }

        [JsonProperty("whichPaymentApps")]
        public string WhichPaymentApps { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("isEstimateCharge")]
        public bool IsEstimateCharge { get; set; }

        [JsonProperty("estimateCharge")]
        public Object EstimateCharge { get; set; }

        [JsonProperty("isDesignServices")]
        public bool IsDesignServices { get; set; }

        [JsonProperty("designServices")]
        public string DesignServices { get; set; }

        [JsonProperty("isContactedByHomeowners")]
        public bool IsContactedByHomeowners { get; set; }

        [JsonProperty("isContactedByContractors")]
        public bool IsContactedByContractors { get; set; }

        [JsonProperty("serviceCallCharge")]
        public string ServiceCallCharge { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty("verificationStatus")]
        public int VerificationStatus { get; set; }
    }


    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }



    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phoneNumberOffice")]
        public string PhoneNumberOffice { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("businessName")]
        public string BusinessName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("businessAddress")]
        public string BusinessAddress { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("isEmailVerified")]
        public bool IsEmailVerified { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty("uniqueMemberId")]
        public long UniqueMemberId { get; set; }

        [JsonProperty("workStatus")]
        public int WorkStatus { get; set; }

        [JsonProperty("workStatusModifiedOn")]
        public DateTime WorkStatusModifiedOn { get; set; }

        [JsonProperty("isDocumentsVerified")]
        public bool IsDocumentsVerified { get; set; }

        [JsonProperty("heardAboutProhzFrom")]
        public int HeardAboutProhzFrom { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("normalizedUserName")]
        public string NormalizedUserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("normalizedEmail")]
        public string NormalizedEmail { get; set; }

        [JsonProperty("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty("securityStamp")]
        public string SecurityStamp { get; set; }

        [JsonProperty("concurrencyStamp")]
        public string ConcurrencyStamp { get; set; }

        [JsonProperty("phoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonProperty("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("lockoutEnd")]
        public object LockoutEnd { get; set; }

        [JsonProperty("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [JsonProperty("accessFailedCount")]
        public int AccessFailedCount { get; set; }
    }

    public class Zipcode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cityId")]
        public int CityId { get; set; }

        [JsonProperty("city")]
        public object City { get; set; }

        [JsonProperty("zipcode")]
        public string zipcode { get; set; }
    }

}