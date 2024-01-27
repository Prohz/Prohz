

using CommunityToolkit.Mvvm.ComponentModel;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class AppShellVM : BaseViewModel
    {
        private IServices aPIServices;
     
        public  AppShellVM()
        {               
            EditUserProfileViewModel.updateprofilePicEvent -= EditUserProfileViewModel_updateprofilePicEvent;
            EditUserProfileViewModel.updateprofilePicEvent += EditUserProfileViewModel_updateprofilePicEvent;
            aPIServices = new Services();           
            MessagingCenter.Subscribe<WorkStatusViewModel>(this, Constants.WorkStatusChanged, (sender) =>
            {
                try
                {                   
                    Status = Preferences.Get(Constants.WorkStatus, "");
                    UserID = Preferences.Get(Constants.UserID, "");
                    if (Status == ResourceFileEnglish.DigitZero)
                    {
                        Current = Constants.Available;
                    }
                    if (Status == ResourceFileEnglish.DisitOne)
                    {
                        Current = Constants.Appoint;
                    }
                    if (Status == ResourceFileEnglish.DigitTwo)
                    {
                        Current = Constants.Booked;
                    }                   
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }

        private void EditUserProfileViewModel_updateprofilePicEvent(string _imagebase64, string _firstname, string _lastname)
        {          
            MemoryStream stream = new(Convert.FromBase64String(_imagebase64));
            ImageSource image = ImageSource.FromStream(() => stream);
            ProfileImage = image;
            FirstName = _firstname;
            LastName = _lastname;
        }

        public async Task GetUserByEmail()
        {
            try
            {
                var response1 = await aPIServices.GetUserbyEmail(Preferences.Get(Constants.Email, string.Empty).ToString());
                if (response1 != null)
                {

                    FirstName = response1.FirstName == null ? null : response1.FirstName;
                    LastName = response1.LastName == null ? null : response1.LastName;
                    Email = response1.LastName == null ? null : response1.Email;
                    if (response1.IsDocumentsVerified)
                    {
                         AccountStatus= AppResources.txtVerified;
                         AccountStatusIcon = "verifiedicon";
                    }
                    else
                    {
                        AccountStatus = AppResources.txtNotVerified;
                        AccountStatusIcon = "";
                    }
                    MemberId = response1.UniqueMemberId.ToString();                   
                    Preferences.Set(Constants.UserID, response1.Id.ToString());
                    Preferences.Set(Constants.ContractorId, response1.Id.ToString());
                    Preferences.Set(Constants.Email, response1.Email);                  
                    Preferences.Set(Constants.WorkStatus, response1.WorkStatus);
                    Status = response1.WorkStatus.ToString();
                    Preferences.Set(Constants.UserProfilePic, Constants.ImagesBaseUrl + response1.ProfilePicture);                               
                    if (response1.ProfilePicture == null)
                    {
                       // ProfileImage = "usericon.png";
                    }
                    //else
                    //{
                    //    string url = Constants.ImagesBaseUrl + response1.ProfilePicture;

                    //    //string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));
                    //    //string encodedFileAsBase64;
                    //    //using (var client = new WebClient())
                    //    //{
                    //    //    byte[] dataBytes = client.DownloadData(new Uri(url));
                    //    //    encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    //    //}
                    //    //MemoryStream stream = new MemoryStream(Convert.FromBase64String(encodedFileAsBase64));
                    //    //ImageSource image = ImageSource.FromStream(() => stream);
                    //    ProfileImage = url;
                    //    //if (response1.ProfilePicture != null)
                    //    //{
                    //    //    MemoryStream stream = new(Convert.FromBase64String(response1.ProfilePicture.ToString()));
                    //    //    ImageSource image = ImageSource.FromStream(() => stream);
                    //    //    ProfileImage = image;
                    //    //}
                    //}
                    if (response1.ProfilePicture != null)
                    {
                        MemoryStream stream = new(Convert.FromBase64String(response1.ProfilePicture.ToString()));
                        ImageSource image = ImageSource.FromStream(() => stream);
                        ProfileImage = image;
                    }

                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            
        }
        public async Task SetStatus()
        {
            try
            {
                if (Status == ResourceFileEnglish.DigitZero)
                {
                    Current = Constants.Available;
                }
                if (Status == ResourceFileEnglish.DisitOne)
                {
                    Current = Constants.Appoint;
                }
                if (Status == ResourceFileEnglish.DigitTwo)
                {
                    Current = Constants.Booked;
                }
            }
            catch (Exception ex)
            {
               Crashes.TrackError(ex);
            }
        }
        public async void Logout()
        {
            try
            {
                bool Res = await Application.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.DoYouWantlogOut, AppResources.Yes, AppResources.Cancel);
                if (Res)
                {
                    Constants.token = String.Empty;                                   
                    Preferences.Set(Constants.UserProfilePic, String.Empty);
                    Preferences.Set(Constants.UserID, String.Empty);
                    Preferences.Set(Constants.WorkStatus, String.Empty);                 
                    App.Current.MainPage = new Login();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }        
        }
        #region
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        ///// <summary>
        ///// first name
        ///// </summary>
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
        /// last name
        /// </summary>
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        /// <summary>
        /// email
        /// </summary>

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }
        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; OnPropertyChanged(nameof(UserID)); }
        }
        private string memberId;
        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; OnPropertyChanged(nameof(MemberId)); }
        }
        private ImageSource profileImage;
        public ImageSource ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value; OnPropertyChanged(nameof(ProfileImage));
            }

        }
        private ImageSource current;
        public ImageSource Current
        {
            get { return current; }
            set
            {
                current = value; OnPropertyChanged(nameof(Current));
            }
        }
        [ObservableProperty]
        string accountStatus;
        [ObservableProperty]
        ImageSource accountStatusIcon;
        #endregion 
    }
}