namespace KopkeHome_ModelLayer.ViewModels.AdminViewModels
{
#nullable disable

    public class DocumentsVerificationViewModels
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public string WorkmanCompensationInsuranceFile { get; set; }

        public string LiabilityInsuranceFile { get; set; }

        public string BusinessOrTradeLicenseFiles { get; set; }
        public int VerificationStatus { get; set; }
        public string Email { get; set; }
        //  public DocumentsVerificationStatus DocumentsVerificationStatus { get; set; }

        //public BusinessProfileDataModel ContractorsBusinessProfile { get; set; }
        //public BusinessProfileSubContractor OtherContractorsBusinessProfile { get; set; }
    }
}
