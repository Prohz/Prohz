using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class UserProfileViewModel : BaseViewModel
    {
        private IServices aPIServices;
        UserProfileModel profileModel = new UserProfileModel();
        /// <summary>
        /// Class Constructor
        /// </summary>
        public UserProfileViewModel()
        {
            aPIServices = new Services();          
        }
        [RelayCommand]
        async void EditClick()
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditUserProfile(profileModel));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
           
        }
        [RelayCommand]
        async void DeleteAccount()
        {
            await Application.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.DoYouWantToDeleteAccount, AppResources.Yes, AppResources.Cancel);
        }
        [ObservableProperty]
        ImageSource profileImage;
        [ObservableProperty]
        string firstname;
        [ObservableProperty]
        string lastName;
        [ObservableProperty]
        string uniqueMeberId;
        [ObservableProperty]
        string userEmail;
        [ObservableProperty]
        string mobileNumber;
        [ObservableProperty]
        string officeNumber;
        [ObservableProperty]
        string businessName;
        [ObservableProperty]
        string businessAdress;
        [ObservableProperty]
        string cityName;
        [ObservableProperty]
        string stateName;
        [ObservableProperty]
        string zipCode;
        [ObservableProperty]
        string accountType;      
        /// <summary>
        /// Method to get user profile detail
        /// </summary>
        public async Task GetUserProfile()
        {
            try
            {
                IsLoading = true;
                GetUserByEmailRequestModel getUserByEmailRequestModel = new GetUserByEmailRequestModel()
                {
                    Email = Preferences.Get(Constants.Email, string.Empty)
                };
                var response = await aPIServices.GetUserbyEmail(getUserByEmailRequestModel.Email);
                if (response != null)
                {
                    Preferences.Set(Constants.ID, response.Id.ToString());
                    Preferences.Set(Constants.RoleID, response.RoleId.ToString());
                    Firstname = response.FirstName;
                    LastName = response.LastName;
                    UniqueMeberId = response.UniqueMemberId.ToString();
                    UserEmail = response.Email;
                    MobileNumber = response.PhoneNumber == null ? AppResources.txtNotAvailable : response.PhoneNumber.ToString();
                    OfficeNumber = response.PhoneNumberOffice == null ? AppResources.txtNotAvailable : response.PhoneNumberOffice.ToString();
                    BusinessName = response.BusinessName == null ? AppResources.txtNotAvailable : response.BusinessName.ToString();
                    BusinessAdress = response.BusinessAddress == null ? AppResources.txtNotAvailable : response.BusinessAddress.ToString();
                    CityName = response.City == null ? AppResources.txtNotAvailable : response.City.ToString();
                    StateName = response.State == null ? AppResources.txtNotAvailable : response.State.ToString();
                    ZipCode = response.ZipCode == null ? AppResources.txtNotAvailable : response.ZipCode.ToString(); ; ;
                    AccountType = response.BusinessName == null ? AppResources.txtNotAvailable : response.BusinessName.ToString();
                    if (response.ProfilePicture != null)
                    {
                        string url = Constants.ImagesBaseUrl + response.ProfilePicture.ToString();
                        //string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));
                        //string encodedFileAsBase64;
                        //using (var client = new WebClient())
                        //{
                        //    byte[] dataBytes = client.DownloadData(new Uri(url));
                        //    encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                        //}
                        //MemoryStream stream = new MemoryStream(Convert.FromBase64String(encodedFileAsBase64));
                        //ImageSource image = ImageSource.FromStream(() => stream);
                        ProfileImage = url;
                        if (response.ProfilePicture != null)
                        {
                            MemoryStream stream = new(Convert.FromBase64String(response.ProfilePicture.ToString()));
                            ImageSource image = ImageSource.FromStream(() => stream);
                            ProfileImage = image;
                        }
                    }                   
                    profileModel.First_Name = Firstname;
                    profileModel.Last_Name = LastName;
                    profileModel.UniqueMemberId = UniqueMeberId;
                    profileModel.Email = UserEmail;
                    profileModel.PhoneNumber = MobileNumber;
                    profileModel.PhoneNumberOffice = MobileNumber;
                    profileModel.Business_Name = BusinessName;
                    profileModel.Business_Address = BusinessAdress;
                    profileModel._City = CityName;
                    profileModel.State = StateName;
                    profileModel.Zip_Code = ZipCode;
                    profileModel.Profile_Picture = response.ProfilePicture?.ToString();
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }

        }
       
    }
}
