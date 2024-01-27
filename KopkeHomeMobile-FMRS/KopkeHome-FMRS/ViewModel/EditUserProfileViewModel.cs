
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
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace KopkeHome_FMRS.ViewModel
{
    public partial class EditUserProfileViewModel : BaseViewModel
    {
        string ProfileImageBase64;
        private IServices aPIServices;
        public  delegate void UpdateProfileonMenuDelegate(string imagebase64, string firstname, string lastaname);
        public static event UpdateProfileonMenuDelegate updateprofilePicEvent;
        private ObservableCollection<StateListModel> statList;
        public ObservableCollection<StateListModel> StateList
        {
            get { return statList; }
            set
            {
                statList = value;
                OnPropertyChanged(nameof(StateList));
            }
        }
        public EditUserProfileViewModel()
        {          
            aPIServices = new Services();
            StateList = new ObservableCollection<StateListModel>();
            //GetUserProfile();           
        }

        [ObservableProperty]
        ImageSource profile_Image;
        [ObservableProperty]
        string firstNameProfile;
        [ObservableProperty]
        string laststNameProfile;
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
        string stateName = Constants.StateDefaultEntry;
        [ObservableProperty]
        string zipCode;
        [ObservableProperty]
        string accountType;
        [ObservableProperty]
        bool isVisibleStateList;
        [ObservableProperty]
        bool cameraGallaryPopup;     


        /// <summary>
        /// Method to get user profile detail
        /// </summary>
        public async void GetUserProfile()
        {
            try
            {
                IsLoading = true;
                GetUserByEmailRequestModel getUserByEmailRequestModel = new()
                {
                    Email = Preferences.Get(Constants.Email, string.Empty)
                };
                var response = await aPIServices.GetUserbyEmail(getUserByEmailRequestModel.Email);
                if (response != null)
                {
                    Firstname = response.FirstName?.ToString();
                    LastName = response.LastName;
                    FirstNameProfile = Firstname;
                    LaststNameProfile = LastName;
                    UniqueMeberId = response.UniqueMemberId.ToString();
                    UserEmail = response.Email;
                    MobileNumber = response.PhoneNumber == null ? String.Empty : response.PhoneNumber.ToString();
                    OfficeNumber = response.PhoneNumberOffice?.ToString();
                    BusinessName = response.BusinessName?.ToString();
                    BusinessAdress = response.BusinessAddress?.ToString();
                    CityName = response.City?.ToString();
                    StateName = response.State?.ToString();
                    ZipCode = response.ZipCode?.ToString();
                    AccountType = response.BusinessName?.ToString();
                    //if (response.ProfilePicture != null && response.ProfilePicture.ToString().Contains("png"))
                    //{
                    //    string url = Constants.ImagesBaseUrl + response.ProfilePicture.ToString();

                    //    string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));
                    //    string encodedFileAsBase64;
                    //    using (var client = new WebClient())
                    //    {
                    //        byte[] dataBytes = client.DownloadData(new Uri(url));
                    //        encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    //    }
                    //    MemoryStream stream = new MemoryStream(Convert.FromBase64String(encodedFileAsBase64));
                    //    ImageSource image = ImageSource.FromStream(() => stream);
                    //    ProfileImage = image;
                    //}
                    //else if (response.ProfilePicture != null)
                    //{
                    //    MemoryStream stream = new(Convert.FromBase64String(response.ProfilePicture.ToString()));
                    //    ImageSource image = ImageSource.FromStream(() => stream);
                    //    ProfileImage = image;
                    //}
                    if(response.ProfilePicture != null) 
                    {
                        MemoryStream stream = new(Convert.FromBase64String(response.ProfilePicture.ToString()));
                        ImageSource image = ImageSource.FromStream(() => stream);
                        ProfileImage = image;
                    }                  
                    IsLoading = true;
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }

        }
      
        /// <summary>
        /// method used to get state list
        /// </summary>
        [RelayCommand]
        async void GetStateList()
        {
            IsLoading = true;
            if (IsVisibleStateList)
            {
              
                IsVisibleStateList = false;
            }
            else
            {              
                var response = await aPIServices.GetStateList();
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var postDetails = JsonConvert.DeserializeObject<List<StateListModel>>(result);
                    if (postDetails != null)
                    {
                        StateList = new ObservableCollection<StateListModel>();

                        foreach (var item in postDetails)
                        {                                                                 
                            item.StateSelectedCommand = StateSelected;
                            StateList.Add(item);
                        }
                        IsVisibleStateList = true;
                    }
                }
            }
            IsLoading = false;

        }

        /// <summary>
        /// Method use to show selected state name
        /// </summary>
        private Command stateSelected;
        public Command StateSelected
        {
            get
            {
                return stateSelected = new Command(async (param) =>
                {
                    var selectedItem = param as StateListModel;
                    IsVisibleStateList = false;
                    StateName = selectedItem.StateName;

                });
            }
        }


        [RelayCommand]
        public void Cancelupdate()
        {           
            App.Current.MainPage = new AppShell();
        }
        [RelayCommand]
        async void UpdateProfile()
        {

            try
            {
                IsLoading = true;
                UpdateBasicInfoRequestModel requestModel = new UpdateBasicInfoRequestModel()
                {

                    Id = Convert.ToInt32(Preferences.Get(Constants.ID, null)),
                    RoleId = Convert.ToInt32(Preferences.Get(Constants.RoleID, null)),
                    FirstName = Firstname,
                    LastName = LastName,
                    PhoneNumber = MobileNumber,
                    PhoneNumberOffice = OfficeNumber,
                    BusinessName = BusinessName,
                    BusinessAddress = BusinessAdress,
                    State = StateName,
                    ZipCode = ZipCode,
                    City = CityName,
                    ProfilePicture = ProfileImageBase64!=null? ProfileImageBase64:string.Empty,
                };
                var response = await aPIServices.UpdateAccountBasicInfo(requestModel);
                if (response.IsSuccessStatusCode)
                {
                    string result =  response.Content.ReadAsStringAsync().Result;
                    var postDetails = JsonConvert.DeserializeObject<UpdateBasicInfoResoponseModel>(result);
                    if (postDetails.Statuscode == 200)
                    {                       
                        IsLoading = false;
                        await Application.Current.MainPage.DisplayAlert(AppResources.Success, AppResources.txtProfileSuccessfullyUpdated, AppResources.OK);
                        updateprofilePicEvent?.Invoke(ProfileImageBase64, Firstname, LastName);                       
                    }
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }           
        }

        [RelayCommand]
        async void ChooseImage()
        {
            CameraGallaryPopup = true;          
        }
        [RelayCommand]
        async Task CameraClick()
        {
            CameraGallaryPopup = false;
            await CapturePhoto();
        }
        [RelayCommand]
        async Task GallaryClick()
        {
            CameraGallaryPopup = false;
            await LoadPhotoAsync();

        }
        [RelayCommand]
         void CloseMediaOption() 
        {
            CameraGallaryPopup = false;
        }

        /// <summary>
        /// Method used to load photo from device storage
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        async Task<bool> LoadPhotoAsync()
        {
            try
            {
                MediaFile emage = await CrossMedia.Current.PickPhotoAsync();
                if (emage == null)
                {
                    return false;
                }
                var photopath = emage.Path;
                using (MemoryStream memory = new MemoryStream())
                {
                    var stream = emage.GetStream();
                    stream.CopyTo(memory);
                    byte[] FileAttachements = memory.ToArray();
                    string temp_inBase64 = Convert.ToBase64String(FileAttachements);
                    ProfileImageBase64 = temp_inBase64;
                    ProfileImage = photopath;
                    Profile_Image = photopath;
                    FileInfo fileInfo = new FileInfo(photopath);
                    memory.Dispose();
                }                 
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return true;
        }

        /// <summary>
        /// Method used to capture picture from camera
        /// </summary>
        /// <returns></returns>
        public async Task CapturePhoto()
        {
            try
            {              
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    Name = DateTime.Now.Ticks + Constants.ImageFormat,
                }).ConfigureAwait(true);
                if (file != null)
                {
                    string FilePath = file.Path;
                    FileInfo fileInfo = new FileInfo(FilePath);
                    var FileName = fileInfo.Name;
                    using (MemoryStream memory = new MemoryStream())
                    {
                        Stream stream = file.GetStream();
                        stream.CopyTo(memory);
                        byte[] FileAttachements = memory.ToArray();
                        string temp_inBase64 = Convert.ToBase64String(FileAttachements).ToString();
                        ProfileImageBase64 = temp_inBase64;
                        memory.Dispose();
                    }
                    ProfileImage = FilePath;
                    Profile_Image = FilePath;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
    }
       
}
