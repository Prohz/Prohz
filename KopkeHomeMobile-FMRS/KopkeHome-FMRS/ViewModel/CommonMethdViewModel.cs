using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class CommonMethdViewModel:BaseViewModel
    {
        private readonly IServices aPIServices;
        public CommonMethdViewModel()
        {
            aPIServices = new Services();
        }

        #region  Full Property

        /// <summary>
        /// 
        /// </summary>
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool isVisivleUSerDetais;

        public bool IsVisivleUSerDetais
        {
            get { return isVisivleUSerDetais; }
            set { isVisivleUSerDetais = value; OnPropertyChanged(nameof(IsVisivleUSerDetais)); }
        }

        private bool isVisivleServiceOffer;

        public bool IsVisivleServiceOffer
        {
            get { return isVisivleServiceOffer; }
            set { isVisivleServiceOffer = value; OnPropertyChanged(nameof(IsVisivleServiceOffer)); }
        }
        private bool isVisivleServiceCredentials;

        public bool IsVisivleServiceCredentials
        {
            get { return isVisivleServiceCredentials; }
            set { isVisivleServiceCredentials = value; OnPropertyChanged(nameof(IsVisivleServiceCredentials)); }
        }

        private bool isVisivleServiceZip;

        public bool IsVisivleServiceZip
        {
            get { return isVisivleServiceZip; }
            set { isVisivleServiceZip = value; OnPropertyChanged(nameof(IsVisivleServiceZip)); }
        }

        private bool isVisivleServiceBusiness;

        public bool IsVisivleServiceBusiness
        {
            get { return isVisivleServiceBusiness; }
            set { isVisivleServiceBusiness = value; OnPropertyChanged(nameof(IsVisivleServiceBusiness)); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged(nameof(City)); }
        }
        private string state;
        public string State
        {
            get { return state; }
            set { state = value; OnPropertyChanged(nameof(State)); }
        }
        private string homePhone;
        public string HomePhone
        {
            get { return homePhone; }
            set { homePhone = value; OnPropertyChanged(nameof(HomePhone)); }
        }


        private string officePhone;
        public string OfficePhone
        {
            get { return officePhone; }
            set { officePhone = value; OnPropertyChanged(nameof(OfficePhone)); }
        }

        private string facebook = Constants.NotAvailable;
        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; OnPropertyChanged(nameof(Facebook)); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }
        private string website = Constants.NotAvailable;
        public string Website
        {
            get { return website; }
            set { website = value; OnPropertyChanged(nameof(Website)); }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(nameof(Time)); }
        }

        /// <summary>
        /// This property is used for Service Offered List
        /// </summary>
        private ObservableCollection<ViewDetailsModel> serviceOffered;
        public ObservableCollection<ViewDetailsModel> ServiceOffered
        {
            get { return serviceOffered; }
            set { serviceOffered = value; OnPropertyChanged(nameof(ServiceOffered)); }
        }

        /// <summary>
        /// This property is used for Service Offered List
        /// </summary>
        private ObservableCollection<ViewDetailsModel> zipCodeList;
        public ObservableCollection<ViewDetailsModel> ZipCodeList
        {
            get { return zipCodeList; }
            set { zipCodeList = value; OnPropertyChanged(nameof(ZipCodeList)); }
        }

        private string tradelicense;
        public string Tradelicense
        {
            get { return tradelicense; }
            set { tradelicense = value; OnPropertyChanged(nameof(Tradelicense)); }
        }
        private string insurance;
        public string Insurance
        {
            get { return insurance; }
            set { insurance = value; OnPropertyChanged(nameof(Insurance)); }
        }
        private string compensationInsurance;
        public string CompensationInsurance
        {
            get { return compensationInsurance; }
            set { compensationInsurance = value; OnPropertyChanged(nameof(CompensationInsurance)); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string estimates;
        public string Estimates
        {
            get { return estimates; }
            set { estimates = value; OnPropertyChanged(nameof(Estimates)); }
        }

        private string estimateFee;
        public string EstimateFee
        {
            get { return estimateFee; }
            set { estimateFee = value; OnPropertyChanged(nameof(EstimateFee)); }
        }

        private string commercialLocation;
        public string CommercialLocation
        {
            get { return commercialLocation; }
            set { commercialLocation = value; OnPropertyChanged(nameof(CommercialLocation)); }
        }
        private string directlybyHomeowners;
        public string DirectlybyHomeowners
        {
            get { return directlybyHomeowners; }
            set { directlybyHomeowners = value; OnPropertyChanged(nameof(DirectlybyHomeowners)); }
        }

        private string offerDesignServices;
        public string OfferDesignServices
        {
            get { return offerDesignServices; }
            set { offerDesignServices = value; OnPropertyChanged(nameof(OfferDesignServices)); }
        }
        private string designServicesFee;
        public string DesignServicesFee
        {
            get { return designServicesFee; }
            set { designServicesFee = value; OnPropertyChanged(nameof(DesignServicesFee)); }
        }
        private string bySubContractors;
        public string BySubContractors
        {
            get { return bySubContractors; }
            set { bySubContractors = value; OnPropertyChanged(nameof(BySubContractors)); }
        }

        private string accountType;
        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; OnPropertyChanged(nameof(AccountType)); }
        }

        private string yearsBusiness;
        public string YearsBusiness
        {
            get { return yearsBusiness; }
            set { yearsBusiness = value; OnPropertyChanged(nameof(YearsBusiness)); }
        }
        private string numberofEmployees;
        public string NumberofEmployees
        {
            get { return numberofEmployees; }
            set { numberofEmployees = value; OnPropertyChanged(nameof(NumberofEmployees)); }
        }
        private string numberofCrews;
        public string NumberofCrews
        {
            get { return numberofCrews; }
            set { numberofCrews = value; OnPropertyChanged(nameof(NumberofCrews)); }
        }

        private string showroom;
        public string Showroom
        {
            get { return showroom; }
            set { showroom = value; OnPropertyChanged(nameof(Showroom)); }
        }
        private string cash;
        public string Cash
        {
            get { return cash; }
            set { cash = value; OnPropertyChanged(nameof(Cash)); }
        }

        private string personalCheck;
        public string PersonalCheck
        {
            get { return personalCheck; }
            set { personalCheck = value; OnPropertyChanged(nameof(PersonalCheck)); }
        }

        private string isPaymentApps;
        public string IsPaymentApps
        {
            get { return isPaymentApps; }
            set { isPaymentApps = value; OnPropertyChanged(nameof(IsPaymentApps)); }
        }

        private string paymentApps;
        public string PaymentApps
        {
            get { return paymentApps; }
            set { paymentApps = value; OnPropertyChanged(nameof(PaymentApps)); }
        }


        private string emergencyServices;
        public string EmergencyServices
        {
            get { return emergencyServices; }
            set { emergencyServices = value; OnPropertyChanged(nameof(EmergencyServices)); }
        }
        
        [ObservableProperty]
        string creditscards;
        [ObservableProperty]
        string businessDescription;
        [ObservableProperty]
        string twentyfourHoursPhoneAnswering;
        [ObservableProperty]
        string isPhoneCallSupport;
        [ObservableProperty]
        string memberId;
        [ObservableProperty]
        string tradelicenseDocs;
        [ObservableProperty]
        string liabilityInsuranceDocs;
        [ObservableProperty]
        string compensationInsuranceDocs;
        [ObservableProperty]
        bool isBusinessLicence;
        [ObservableProperty]
        bool isInsuranceFile;
        [ObservableProperty]
        bool isCompensationInsuranceFile;
        [ObservableProperty]
        ImageSource tradelicenseImage;
        [ObservableProperty]
        ImageSource liabilityInsuranceImage;
        [ObservableProperty]
        ImageSource compensationInsuranceImage;
        [ObservableProperty]
        string businessName;
        private string profileImage;

        public string ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value;

                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        #endregion
        #region  Full Command
        private Command tapOnUserDetails;
        /// <summary>
        /// 
        /// </summary>
        public Command TapOnUserDetails
        {
            get
            {
                if (tapOnUserDetails == null)
                {
                    tapOnUserDetails = new Command(() =>
                    {
                        try
                        {
                            // To check network connectivity
                            var current = Connectivity.NetworkAccess;
                            if (current == NetworkAccess.Internet)
                            {
                                if (IsVisivleUSerDetais == false)
                                {
                                    IsVisivleUSerDetais = true;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                                else
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                            }
                            ContractorProfileDetailsRequest contractorProfileDetailsRequest = new ContractorProfileDetailsRequest()
                            {
                                //id = 1305,
                                //BusinessAddress = BusinessAddress,

                            };

                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
                return tapOnUserDetails;
            }
        }

        private Command tapOnServiceOffer;
        /// <summary>
        /// 
        /// </summary>
        public Command TapOnServiceOffer
        {
            get
            {
                if (tapOnServiceOffer == null)
                {
                    tapOnServiceOffer = new Command(() =>
                    {
                        try
                        {
                            // To check network connectivity
                            var current = Connectivity.NetworkAccess;
                            if (current == NetworkAccess.Internet)
                            {
                                if (IsVisivleServiceOffer == false)
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = true;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                                else
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
                return tapOnServiceOffer;
            }
        }
        private Command tapOnServiceZip;
        /// <summary>
        /// 
        /// </summary>
        public Command TapOnServiceZip
        {
            get
            {
                if (tapOnServiceZip == null)
                {
                    tapOnServiceZip = new Command(() =>
                    {
                        try
                        {
                            // To check network connectivity
                            var current = Connectivity.NetworkAccess;
                            if (current == NetworkAccess.Internet)
                            {
                                if (IsVisivleServiceZip == false)
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = true;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                                else
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
                return tapOnServiceZip;
            }
        }
        private Command tapOnServiceBusiness;
        /// <summary>
        /// 
        /// </summary>
        public Command TapOnServiceBusiness
        {
            get
            {
                if (tapOnServiceBusiness == null)
                {
                    tapOnServiceBusiness = new Command(() =>
                    {
                        try
                        {
                            // To check network connectivity
                            var current = Connectivity.NetworkAccess;
                            if (current == NetworkAccess.Internet)
                            {
                                if (IsVisivleServiceBusiness == false)
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = true;
                                    IsVisivleServiceCredentials = false;
                                }
                                else
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
                return tapOnServiceBusiness;
            }
        }
        private Command tapOnServiceCredentails;
        /// <summary>
        /// 
        /// </summary>
        public Command TapOnServiceCredentails
        {
            get
            {
                if (tapOnServiceCredentails == null)
                {
                    tapOnServiceCredentails = new Command(() =>
                    {
                        try
                        {
                            // To check network connectivity
                            var current = Connectivity.NetworkAccess;
                            if (current == NetworkAccess.Internet)
                            {
                                if (IsVisivleServiceCredentials == false)
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = true;
                                }
                                else
                                {
                                    IsVisivleUSerDetais = false;
                                    IsVisivleServiceOffer = false;
                                    IsVisivleServiceZip = false;
                                    IsVisivleServiceBusiness = false;
                                    IsVisivleServiceCredentials = false;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }
                return tapOnServiceCredentails;
            }
        }

        #endregion

        public async Task GetContractorDetails()
        {
            try
            {
                IsLoading = true;
                ContractorProfileDetailsRequest contractorProfileDetailsRequest = new ContractorProfileDetailsRequest()
                {
                    Id = Preferences.Get(Constants.ContractorId, String.Empty)
                };
                var response = await aPIServices.ContractorProfile(contractorProfileDetailsRequest);
                if (response != null)
                {
                    FirstName = response.User.FirstName;
                    BusinessName=response.User.BusinessName;
                    Address = response.User.BusinessAddress;
                    City = response.User.City;
                    State = response.User.State;
                    HomePhone = response.User.PhoneNumber == null ? Constants.NotAvailable : response.User.PhoneNumber;
                    OfficePhone = response.User.PhoneNumberOffice == null ? Constants.NotAvailable : response.User.PhoneNumberOffice;
                    Email = response.User.Email == null ? Constants.NotAvailable : response.User.Email;
                    MemberId = response.User.UniqueMemberId.ToString();
                    if (response.BusinessProfileContractor != null)
                    {
                        TwentyfourHoursPhoneAnswering = response.BusinessProfileContractor.Is24HoursPhoneAnswering? AppResources.Yes : AppResources.No;
                        BusinessDescription = string.IsNullOrEmpty(response.BusinessProfileContractor.BusinessDescription) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.BusinessDescription;
                        IsPhoneCallSupport=response.BusinessProfileContractor.IsPhoneCallSupport ? AppResources.Yes : AppResources.No;
                        Creditscards = response.BusinessProfileContractor.OtherCreditCard.ToString();
                        PersonalCheck =response.BusinessProfileContractor.PersonalChecks? AppResources.Yes : AppResources.No;
                        Facebook = string.IsNullOrEmpty(response.BusinessProfileContractor.FacebookPageURL) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.FacebookPageURL;
                        Website = string.IsNullOrEmpty(response.BusinessProfileContractor.CompanyWebsiteURL) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.CompanyWebsiteURL;
                        Time = response.BusinessProfileContractor.NormalBusinessHours;
                        YearsBusiness = response.BusinessProfileContractor.YearsInBusiness;
                        Insurance = response.BusinessProfileContractor.IsLiabilityInsurance ? AppResources.Yes : AppResources.No;
                        AccountType = Preferences.Get(Constants.ContractorAccountType, String.Empty);
                        Showroom = response.BusinessProfileContractor.CommercialLocation ? AppResources.Yes : AppResources.No;
                        NumberofEmployees = response.BusinessProfileContractor.NumberOfEmployees;
                        NumberofCrews = response.BusinessProfileContractor.JobSiteCrews;
                        OfferDesignServices = response.BusinessProfileContractor.IsDesignServices ? AppResources.Yes : AppResources.No;
                        DesignServicesFee = response.BusinessProfileContractor.DesignServices;
                        IsPaymentApps = response.BusinessProfileContractor.IsPaymentApps ? AppResources.Yes : AppResources.No;
                        PaymentApps = response.BusinessProfileContractor.WhichPaymentApps;
                        PersonalCheck = response.BusinessProfileContractor.PersonalChecks ? AppResources.Yes : AppResources.No;
                        Estimates = response.BusinessProfileContractor.IsEstimateCharge ? AppResources.Yes : AppResources.No;
                        EstimateFee = response.BusinessProfileContractor.EstimateCharge;
                        if (response.BusinessProfileContractor.ProfilePicture != null)
                        {
                            ProfileImage = Constants.ImagesBaseUrl + response.BusinessProfileContractor.ProfilePicture;
                        }
                        Cash = response.BusinessProfileContractor.IsCash ? AppResources.Yes : AppResources.No;
                        BySubContractors = response.BusinessProfileContractor.IsContactedByHomeowners ? AppResources.Yes : AppResources.No;
                        DirectlybyHomeowners = response.BusinessProfileContractor.IsContactedByHomeowners ? AppResources.Yes : AppResources.No;
                        EmergencyServices = response.BusinessProfileContractor.IsOfferEmergencyServices ? AppResources.Yes : AppResources.No;
                        Tradelicense = response.BusinessProfileContractor.IsBusinessOrTradeLicense ? AppResources.Yes : AppResources.No;
                        CompensationInsurance = response.BusinessProfileContractor.IsWorkmanCompensationInsurance ? AppResources.Yes : AppResources.No;
                        TradelicenseDocs = response.BusinessProfileContractor.BusinessOrTradeLicenseFiles;
                        if (String.IsNullOrEmpty(TradelicenseDocs))
                        {
                            IsBusinessLicence = true;
                        }
                        else
                        {
                            TradelicenseImage = Constants.DockIcon;
                        }

                        LiabilityInsuranceDocs = response.BusinessProfileContractor.LiabilityInsuranceFile;
                        if (String.IsNullOrEmpty(LiabilityInsuranceDocs))
                        {
                            IsInsuranceFile = true;
                        }
                        else
                        {
                            LiabilityInsuranceImage = Constants.DockIcon;
                        }
                        CompensationInsuranceDocs = response.BusinessProfileContractor.WorkmanCompensationInsuranceFile;
                        if (String.IsNullOrEmpty(CompensationInsuranceDocs))
                        {
                            IsCompensationInsuranceFile = true;
                        }
                        else
                        {
                            CompensationInsuranceImage = Constants.DockIcon;
                        }
                    }
                    if (response.SubContractorBusinessProfile != null)
                    {
                        TwentyfourHoursPhoneAnswering = response.SubContractorBusinessProfile.Is24HoursPhoneAnswering ? AppResources.Yes : AppResources.No;
                        BusinessDescription = string.IsNullOrEmpty(response.SubContractorBusinessProfile.BusinessDescription) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.BusinessDescription;
                        IsPhoneCallSupport = response.SubContractorBusinessProfile.IsPhoneCallSupport ? AppResources.Yes : AppResources.No;
                        Creditscards = response.BusinessProfileContractor.OtherCreditCard.ToString();
                        Facebook = string.IsNullOrEmpty(response.SubContractorBusinessProfile.FacebookPageURL) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.FacebookPageURL;
                        Website = string.IsNullOrEmpty(response.SubContractorBusinessProfile.CompanyWebsiteURL) ? AppResources.txtNotAvailable : response.BusinessProfileContractor.CompanyWebsiteURL;
                        Time = response.SubContractorBusinessProfile.NormalBusinessHours;
                        YearsBusiness = response.SubContractorBusinessProfile.YearsInBusiness;
                        Insurance = response.SubContractorBusinessProfile.IsLiabilityInsurance ? AppResources.Yes : AppResources.No;
                        AccountType = Preferences.Get(Constants.ContractorAccountType, String.Empty);
                        Showroom = (response.SubContractorBusinessProfile.CommercialLocationContractor) ? AppResources.Yes : AppResources.No;
                        NumberofEmployees = response.SubContractorBusinessProfile.NumberOfEmployees;
                        NumberofCrews = response.SubContractorBusinessProfile.JobSiteCrews;
                        OfferDesignServices = response.SubContractorBusinessProfile.IsDesignServices ? AppResources.Yes : AppResources.No;
                        DesignServicesFee = response.SubContractorBusinessProfile.DesignServices;
                        IsPaymentApps = response.SubContractorBusinessProfile.IsPaymentApps ? AppResources.Yes : AppResources.No;
                        PaymentApps = response.SubContractorBusinessProfile.WhichPaymentApps;
                        PersonalCheck = response.SubContractorBusinessProfile.PersonalChecks ? AppResources.Yes : AppResources.No;
                        Estimates = response.SubContractorBusinessProfile.IsEstimateCharge ? AppResources.Yes : AppResources.No;
                        EstimateFee = response.SubContractorBusinessProfile.EstimateCharge != null ? response.SubContractorBusinessProfile.EstimateCharge.ToString() : null;
                        if (response.SubContractorBusinessProfile.ProfilePicture != null)
                        {
                            ProfileImage = Constants.ImagesBaseUrl + response.SubContractorBusinessProfile.ProfilePicture;
                        }                       
                        Cash = response.SubContractorBusinessProfile.IsCash ? AppResources.Yes : AppResources.No;
                        BySubContractors = response.SubContractorBusinessProfile.IsContactedByHomeowners ? AppResources.Yes : AppResources.No;
                        DirectlybyHomeowners = response.SubContractorBusinessProfile.IsContactedByHomeowners ? AppResources.Yes : AppResources.No;
                        EmergencyServices = response.SubContractorBusinessProfile.IsOfferEmergencyServices ? AppResources.Yes : AppResources.No;
                        Tradelicense = response.SubContractorBusinessProfile.IsBusinessOrTradeLicense ? AppResources.Yes : AppResources.No;
                        CompensationInsurance = response.SubContractorBusinessProfile.IsWorkmanCompensationInsurance ? AppResources.Yes : AppResources.No;
                        TradelicenseDocs = response.SubContractorBusinessProfile.BusinessOrTradeLicenseFiles;
                        if (String.IsNullOrEmpty(TradelicenseDocs))
                        {
                            IsBusinessLicence = true;
                        }
                        else
                        {
                            TradelicenseImage = Constants.DockIcon;
                        }

                        LiabilityInsuranceDocs = response.SubContractorBusinessProfile.LiabilityInsuranceFile;
                        if (String.IsNullOrEmpty(LiabilityInsuranceDocs))
                        {
                            IsInsuranceFile = true;
                        }
                        else
                        {
                            LiabilityInsuranceImage = Constants.DockIcon;
                        }
                        CompensationInsuranceDocs = response.SubContractorBusinessProfile.WorkmanCompensationInsuranceFile;
                        if (String.IsNullOrEmpty(CompensationInsuranceDocs))
                        {
                            IsCompensationInsuranceFile = true;
                        }
                        else
                        {
                            CompensationInsuranceImage = Constants.DockIcon;
                        }
                    }
                    ServiceOffered = new ObservableCollection<ViewDetailsModel>();
                    foreach (var items in response.Categories)
                    {
                        var viewDetailsModel = new ViewDetailsModel();
                        viewDetailsModel.ServiceName = items.Name;
                        ServiceOffered.Add(viewDetailsModel);
                    }

                    ZipCodeList = new ObservableCollection<ViewDetailsModel>();
                    foreach (var items in response.Zipcodes)
                    {
                        var viewDetailsModel = new ViewDetailsModel();
                        viewDetailsModel.ZipCode = items.zipcode;
                        ZipCodeList.Add(viewDetailsModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsLoading = false;
            }
            IsLoading = false;
        }
        [RelayCommand]
        async void OpenTradLicense(string TradelicenseDocs)
        {
            await OpenFile(TradelicenseDocs);
        }
        [RelayCommand]
        async void OpenLibility(string LiabilityInsuranceDocs)
        {
            await OpenFile(LiabilityInsuranceDocs);
        }
        [RelayCommand]
        async void OpenCompensation(string CompensationInsuranceDocs)
        {
            await OpenFile(CompensationInsuranceDocs);
        }

        public static async Task OpenFile(string strDataLink)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FileWebView(strDataLink));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
