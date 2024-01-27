using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using Microsoft.Maui;
using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Windows.Input;

namespace KopkeHome_FMRS.ViewModel;

public partial class DashboardViewModel : BaseViewModel
{
    #region  Full Constructor
    public DashboardViewModel()
    {
        RefreshData();
    }

    public void RefreshData()
    {
        aPIServices = new Services();
        ZipCodeList = new ObservableCollection<ZipCodeListResponseModel>();
        CategoriesList = new ObservableCollection<CategoriesListResponseModel>();
        ContractorList = new ObservableCollection<SearchContractorsListResponse>();
        WorkStatus = new ObservableCollection<WorkStatusModel>();       
        GetWorkStatus();
        if (Utilities.IsFromStatusChanged)
        {
            var workId = Preferences.Get("WorkStatusId", 0);
            Utilities.WorkStatusId = workId;
            if (Utilities.WorkStatusId == 0)
            {
                UserProperties.StatusImage = Constants.Available;
            }
            else if (Utilities.WorkStatusId == 1)
            {
                UserProperties.StatusImage = Constants.Appoint;
            }
            else if (Utilities.WorkStatusId == 2)
            {
                UserProperties.StatusImage = Constants.Booked;
            }
        }
    }
    #endregion
    #region  Full Property
    public int statusId = 0;
    private IServices aPIServices;
    /// <summary>
    /// This property is used to check IsZipErrorVisible
    /// </summary>
    private bool isZipErrorVisible = false;
    public bool IsZipErrorVisible
    {
        get { return isZipErrorVisible; }
        set
        {
            isZipErrorVisible = value;
            OnPropertyChanged(nameof(IsZipErrorVisible));
        }
    }
    /// <summary>
    /// This property is used to check ZipErrorText
    /// </summary>
    private string zipErrorText = "Please enter zip code";
    public string ZipErrorText
    {
        get { return zipErrorText; }
        set
        {
            zipErrorText = value;
            OnPropertyChanged(nameof(ZipErrorText));
        }
    }
    /// <summary>
    /// This property is used to ZipCodeFrameVisible
    /// </summary>
    private bool zipCodeFrameVisible = false;
    public bool ZipCodeFrameVisible
    {
        get { return zipCodeFrameVisible; }
        set
        {
            zipCodeFrameVisible = value;
            OnPropertyChanged(nameof(ZipCodeFrameVisible));
        }
    }
    /// <summary>
    /// This property is used to CategoryeFrameVisible
    /// </summary>
    private bool categoryeFrameVisible = false;
    public bool CategoryeFrameVisible
    {
        get { return categoryeFrameVisible; }
        set
        {
            categoryeFrameVisible = value;
            OnPropertyChanged(nameof(CategoryeFrameVisible));
        }
    }
    /// <summary>
    /// This property is used for Category
    /// </summary>
    private string category;
    public string Category
    {
        get { return category; }
        set
        {
            category = value;
            OnPropertyChanged(nameof(Category));
        }
    }
    /// <summary>
    /// This property is used to IsCategoryErrorVisible
    private bool isCategoryErrorVisible;
    public bool IsCategoryErrorVisible
    {
        get { return isCategoryErrorVisible; }
        set
        {
            isCategoryErrorVisible = value;
            OnPropertyChanged(nameof(IsCategoryErrorVisible));
        }
    }

    /// <summary>
    /// This property is used for Prefix
    /// </summary>
    private string prefix;
    public string Prefix
    {
        get { return prefix; }
        set
        {
            prefix = value;
            OnPropertyChanged(nameof(Prefix));
        }
    }

    /// <summary>
    /// This property is used for UserID
    /// </summary>
    private string userID;
    public string UserID
    {
        get { return userID; }
        set
        {
            userID = value;
            OnPropertyChanged(nameof(UserID));
        }
    }

    /// <summary>
    /// This property is used for Zipcode
    /// </summary>
    private string zipcode;
    public string Zipcode
    {
        get { return zipcode; }
        set
        {
            zipcode = value;
            OnPropertyChanged(nameof(Zipcode));
            if (!string.IsNullOrEmpty(zipcode))
            {
              
            }
        }
    }
    private ObservableCollection<ZipCodeListResponseModel> zipCodeList;
    public ObservableCollection<ZipCodeListResponseModel> ZipCodeList
    {
        get { return zipCodeList; }
        set
        {
            zipCodeList = value;
            OnPropertyChanged(nameof(ZipCodeList));
        }
    }
    private ObservableCollection<CategoriesListResponseModel> categoriesList;
    public ObservableCollection<CategoriesListResponseModel> CategoriesList
    {
        get { return categoriesList; }
        set
        {
            categoriesList = value;
            OnPropertyChanged(nameof(CategoriesList));
        }
    }
    private ObservableCollection<SearchContractorsListResponse> contractorList;
    public ObservableCollection<SearchContractorsListResponse> ContractorList
    {
        get { return contractorList; }
        set
        {
            contractorList = value;
            OnPropertyChanged(nameof(ContractorList));
        }
    }

    /// <summary>
    /// This property is used for Now
    /// </summary>
    private string now = "blank.png";
    public string Now
    {
        get { return now; }
        set
        {
            now = value;
            OnPropertyChanged(nameof(Now));
        }
    }

    /// <summary>
    /// This property is used for Soon
    /// </summary>
    private string soon = "blank.png";
    public string Soon
    {
        get { return soon; }
        set
        {
            soon = value;
            OnPropertyChanged(nameof(Soon));
        }
    }

    /// <summary>
    /// This property is used for Later
    /// </summary>
    private string later = "blank.png";
    public string Later
    {
        get { return later; }
        set
        {
            later = value;
            OnPropertyChanged(nameof(Later));
        }
    }

    /// <summary>
    /// This property is used for Blank
    /// </summary>
    private string blank;
    public string Blank
    {
        get { return blank; }
        set
        {
            blank = value;
            OnPropertyChanged(nameof(Blank));
        }
    }

    /// <summary>
    /// This property is used for Wrong
    /// </summary>
    private string wrong;
    public string Wrong
    {
        get { return wrong; }
        set
        {
            wrong = value;
            OnPropertyChanged(nameof(Wrong));
        }
    }
    /// <summary>
    /// This property is used for IsVisibleFilterPopup
    /// </summary>
    private bool isVisibleFilterPopup;
    public bool IsVisibleFilterPopup
    {
        get { return isVisibleFilterPopup; }
        set { isVisibleFilterPopup = value; OnPropertyChanged(nameof(IsVisibleFilterPopup)); }
    }


    /// <summary>
    /// This property is used for workStatus
    /// </summary>
    private ObservableCollection<WorkStatusModel> workStatus;
    public ObservableCollection<WorkStatusModel> WorkStatus
    {
        get { return workStatus; }
        set { workStatus = value; OnPropertyChanged(nameof(WorkStatus)); }
    }

    /// <summary>
    /// This property is used for ChangeWorkStatusColor
    /// </summary>
    private Color changeWorkStatusColor;
    public Color ChangeWorkStatusColor
    {
        get { return changeWorkStatusColor; }
        set { changeWorkStatusColor = value; OnPropertyChanged(nameof(ChangeWorkStatusColor)); }
    }

    #endregion
    #region  Full Command
    private Command viewDetailsCommand;
    public Command ViewDetailsCommand
    {
        get
        {
            return viewDetailsCommand = new Command(async(param) => 
            {
                try
                {
                    var selectedItem = param as SearchContractorsListResponse;
                    Preferences.Set(Constants.ContractorId, selectedItem.Id.ToString());
                    Preferences.Set(Constants.ContractorAccountType, selectedItem.ContractorType);
                     await Application.Current.MainPage.Navigation.PushAsync(new ViewDetails(selectedItem));
                     //await Application.Current.MainPage.Navigation.PushAsync(new BusinessProfile(selectedItem));                    
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }  
    

    private Command likeCommand;
    public Command LikeCommand
    {
        get
        {
            return likeCommand = new Command( (param) =>
            {
                try
                {
                    var selectedItem = param as SearchContractorsListResponse;
                    selectedItem.IsLikeOption = false;                                     
                    selectedItem.IsDisLiked = !selectedItem.IsLiked;
                    if(selectedItem.LikeImage == Constants.LikeImageGreen)
                    {
                        selectedItem.LikeImage = Constants.LikeImageGray;
                         SubmitLikeDisLike(selectedItem.Id, false, false);
                    }
                    else
                    {
                        selectedItem.LikeImage = Constants.LikeImageGreen;
                        SubmitLikeDisLike(selectedItem.Id, true, false);
                    }                    
                    selectedItem.DislikeImage = Constants.DisLikeImageGray;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
    private Command dislikeCommand;
    public Command DislikeCommand
    {
        get
        {
            return dislikeCommand = new Command((param) =>
            {
                try
                {
                    var selectedItem = param as SearchContractorsListResponse;

                    selectedItem.IsLikeOption = false;                                   
                    selectedItem.IsDisLiked = !selectedItem.IsLiked;
                    if(selectedItem.DislikeImage == Constants.DisLikeImageRed)
                    {
                        selectedItem.DislikeImage = Constants.DisLikeImageGray;
                         SubmitLikeDisLike(selectedItem.Id, false, false);
                    }
                    else
                    {
                        selectedItem.DislikeImage = Constants.DisLikeImageRed;
                         SubmitLikeDisLike(selectedItem.Id, false, true);
                    }                   
                    selectedItem.LikeImage = Constants.LikeImageGray;
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }
    /// <summary>
    /// Method to submit LikeDislike
    /// </summary>
    /// <param name="id"></param>
    /// <param name="like"></param>
    /// <param name="dislike"></param>
    async void SubmitLikeDisLike(int id, bool like, bool dislike)
    {
        try
        {
            UpdateContractorImageRequestModel updateContractorImageRequestModel = new UpdateContractorImageRequestModel()
            {
                isLiked = like,
                isDisLiked = dislike,
                userId = Convert.ToInt32(Preferences.Get(Constants.UserID, string.Empty)),
                contractorId = id,
            };
            UpdateContractorImageResponseModel response = await aPIServices.UpdateImage(updateContractorImageRequestModel);            
            if (response != null)
            {
                
            }   
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
      
    }
    /// <summary>
    /// This command is used for the search command
    /// </summary>
    private Command searchCommand;
    public Command SearchCommand
    {
        get
        {
            return searchCommand = new Command(async () =>
            {
                try
                {
                    IsLoading= true;
                   

                    if (string.IsNullOrEmpty(Zipcode))
                    {
                        IsZipErrorVisible = true;
                    }
                    else string.IsNullOrEmpty(Category);
                    {
                        IsCategoryErrorVisible = true;                        
                    }

                    if (!string.IsNullOrEmpty(Zipcode) && !string.IsNullOrEmpty(Category))
                    {
                        IsZipErrorVisible = false;
                        IsCategoryErrorVisible = false;                      
                        await SearchWithZip();                                         
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                finally
                {
                    IsLoading= false;
                }
            });
        }
    }

    /////
    /// Method used to search item  
    /// 
     async Task  SearchWithZip()
    {     
        try
        {
            ContractorList = new ObservableCollection<SearchContractorsListResponse>();
            SearchContractorsListRequest searchContractorsListRequest = new SearchContractorsListRequest()
            {
                UserId = Convert.ToInt32(Preferences.Get(Constants.UserID, string.Empty)),
                ZipCode = Zipcode,
                Categories = Category,
            };          
            var response = await aPIServices.SearchContractors(searchContractorsListRequest);
            if (response != null)
            {
                foreach (var items in response)
                {                 
                    if (items.Status == 0)
                    {
                        items.WorkStatusImage = Constants.Available;
                    }
                    else if (items.Status == 1)
                    {
                        items.WorkStatusImage = Constants.Appoint;
                    }
                    else
                    {
                        if (items.Status == 2)
                        {
                            items.WorkStatusImage = Constants.Booked;
                        }
                    }
                    if (!items.IsDisLiked && !items.IsLiked)
                    {
                        items.LikeImage = Constants.LikeImageGray;
                        items.DislikeImage = Constants.DisLikeImageGray;
                    }
                    else if (items.IsDisLiked && !items.IsLiked)
                    {
                        items.DislikeImage = Constants.DisLikeImageRed;
                        items.LikeImage = Constants.LikeImageGray;
                    }
                    else if (items.IsLiked && !items.IsDisLiked)
                    {
                        items.LikeImage = Constants.LikeImageGreen;
                        items.DislikeImage = Constants.DisLikeImageGray;
                    }                   
                    items.LikeCommand = LikeCommand;
                    items.DislikeCommand = DislikeCommand;
                    items.ViewDetailsCommand = ViewDetailsCommand;                   
                    ContractorList.Add(items);
                }
            }
        }
        catch(Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }

    /// <summary>
    /// This command is used for the Filter
    /// </summary>
    private Command commandForFilter;
    public Command CommandForFilter
    {
        get
        {
            return commandForFilter = new Command(() =>
            {
                try
                {
                   
                    if (IsVisibleFilterPopup == false)
                    {
                        IsVisibleFilterPopup = true;
                    }
                    else
                    {
                        IsVisibleFilterPopup = false;
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }
    #endregion

    #region  Full Methods
    public async Task GetZipCodeList(string zipcode, string userid)
    {
        try
        {
            List<ZipCodeListResponseModel> response = await aPIServices.ZipCodeList(zipcode, userid);
            if (response != null && response.Count > 0)
            {
                ZipCodeList = new ObservableCollection<ZipCodeListResponseModel>();
                foreach (var items in response)
                {
                    ZipCodeList.Add(items);
                }
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
    public async Task GetCategoryList(string prefix, string zipcode)
    {
        try
        {
            List<CategoriesListResponseModel> response1 = await aPIServices.CategoryList(prefix, Convert.ToInt32(zipcode));
            if (response1 != null && response1.Count > 0)
            {
                foreach (var items in response1)
                {
                    CategoriesList.Add(items);
                }
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
    public string ZipSelected { get; set; }
    public ICommand ZipCodeSelectedCommand
    {
        get
        {
            return new Command((param) =>
            {
                var selectedItem = param as ZipCodeListResponseModel;
                Zipcode = selectedItem.Zipcode;
                ZipSelected = selectedItem.Zipcode;
                ZipCodeFrameVisible = false;
            });
        }
    }
    public string CategorySelected { get; set; }
    public ICommand CategorySelectedCommand
    {
        get
        {
            return new Command((param) =>
            {
                var selectedItem = param as CategoriesListResponseModel;
                Category = selectedItem.Name;
                CategorySelected = selectedItem.Name;
                CategoryeFrameVisible = false;
            });
        }
    }

    public async Task ZipCodeTextChanged()
    {
        try
        {
            if (!string.IsNullOrEmpty(Zipcode))
            {
                ZipCodeList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.ZipCodeListResponseModel>(); ;
                if (!string.IsNullOrEmpty(Zipcode))
                {
                    await GetZipCodeList(Zipcode, Preferences.Get(Constants.UserID, string.Empty));
                    if (string.IsNullOrEmpty(ZipSelected))
                    {
                        if (ZipCodeList.Count > 0)
                        {
                            if (string.IsNullOrEmpty(Zipcode))
                            {
                                ZipCodeList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.ZipCodeListResponseModel>(); ;
                                ZipCodeFrameVisible = false;
                            }
                            else
                            {
                                IsZipErrorVisible = false;
                                ZipCodeFrameVisible = true;
                            }
                        }
                        else
                        {
                            ZipCodeList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.ZipCodeListResponseModel>(); ;
                            ZipCodeFrameVisible = false;
                            ZipErrorText = AppResources.AlertNoZipCodeFound;
                            IsZipErrorVisible = true;
                        }
                    }
                }
                else
                {
                    ZipCodeList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.ZipCodeListResponseModel>(); ;
                    ZipCodeFrameVisible = false;
                    ZipSelected = string.Empty;
                }
            }
            else
            {
                ZipCodeList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.ZipCodeListResponseModel>(); ;
                ZipCodeFrameVisible = false;
                ZipSelected = string.Empty;
                ZipErrorText = AppResources.AlertEnterZip;
                IsZipErrorVisible = true;
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
    public async Task CategoryTextChanged()
    {
        try
        {
            if (!string.IsNullOrEmpty(Category) && !string.IsNullOrEmpty(Zipcode))
            {
                CategoriesList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.CategoriesListResponseModel>();
                await GetCategoryList(Category, Zipcode);
                if (string.IsNullOrEmpty(CategorySelected))
                {
                    if (CategoriesList.Count > 0)
                    {
                        if (string.IsNullOrEmpty(Category))
                        {
                            CategoriesList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.CategoriesListResponseModel>();
                            CategoryeFrameVisible = false;
                        }
                        else
                        {
                            IsCategoryErrorVisible = false;
                            CategoryeFrameVisible = true;
                        }
                    }
                    else
                    {
                        CategoriesList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.CategoriesListResponseModel>();
                        CategoryeFrameVisible = false;
                        await App.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.txtMessageNowServiceNotAvailble + Zipcode, AppResources.OK);
                        IsCategoryErrorVisible = false;
                    }
                }
                else
                {
                    CategoriesList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.CategoriesListResponseModel>(); ;
                    CategoryeFrameVisible = false;
                    CategorySelected = string.Empty;
                }
            }
            else
            {
                if (Category.Length > 0)
                {
                    CategoriesList = new System.Collections.ObjectModel.ObservableCollection<Models.ResponseModels.CategoriesListResponseModel>();
                    CategoryeFrameVisible = false;
                    await App.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.txtSelectZipCode, AppResources.OK);
                    Category = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }


    /// <summary>
    /// Method for GetWorkStatus
    /// </summary>
    public void GetWorkStatus()
    {
        try
        {
            WorkStatus = new ObservableCollection<WorkStatusModel>(new[]
            {
                    new WorkStatusModel {BackgroundColor = "Green", StatusName="Booking for Now(0-4 weeks)"},
                     new WorkStatusModel {BackgroundColor = "Yellow", StatusName="Booking for Soon(5-16 weeks)"},
                      new WorkStatusModel {BackgroundColor = "Red", StatusName="Booking for Later(17-52 weeks)"},
            });
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
    public int SavedStatusId = 0;
    public ICommand SaveStatusCommand
    {
        get
        {
            return new Command(async () =>
            {
                UserWorkStatusRequestModel userworkstatusrequestmodel = new UserWorkStatusRequestModel()
                {
                    UserId = 1305,
                    RadioWorkStatus = statusId
                };
                UserWorkStatusResponseModel response = await aPIServices.UserWorkStatus(userworkstatusrequestmodel);
                if (response != null)
                {
                    SavedStatusId = response.WorkStatus;
                    Utilities.WorkStatusId = response.WorkStatus;
                    if (response.WorkStatus == 0)
                    {
                        Current = "available.png";
                        Now = "available.png";
                        Soon = "blank.png";
                        Later = "blank.png";
                        IsVisibleFilterPopup = false;
                        //ChangeWorkStatusColor= 
                    }

                    else if (response.WorkStatus == 1)
                    {
                        Current = "appoint.png";
                        Now = "blank.png";
                        Soon = "appoint.png";
                        Later = "blank.png";
                        IsVisibleFilterPopup = false;
                    }
                    else
                    {
                        Current = "booked.png";
                        Now = "blank.png";
                        Soon = "blank.png";
                        Later = "booked.png";
                        IsVisibleFilterPopup = false;
                    }
                }
            });
        }
    }

    /// <summary>
    /// This command is used for the command Now
    /// </summary>
    private Command commandNow;
    public Command CommandNow
    {
        get
        {
            return commandNow = new Command(async () =>
            {
                try
                {

                    statusId = 0;
                    Now = "available.png";
                    Soon = "blank.png";
                    Later = "blank.png";

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }

    /// <summary>
    /// This command is used for the command Now
    /// </summary>
    private Command commandSoon;
    public Command CommandSoon
    {
        get
        {
            return commandSoon = new Command(async () =>
            {
                try
                {
                    statusId = 1;
                    Now = "blank.png";
                    Soon = "appoint.png";
                    Later = "blank.png";

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }

    /// <summary>
    /// This command is used for the command Now
    /// </summary>
    private Command commandLater;
    public Command CommandLater
    {
        get
        {
            return commandLater = new Command(async () =>
            {
                try
                {
                    statusId = 2;
                    Now = "blank.png";
                    Soon = "blank.png";
                    Later = "booked.png";

                }
                catch (Exception ex)
                { 
                    Crashes.TrackError(ex);
                }
            });
        }
    }

    /// <summary>
    /// This command is used for the command Wrong
    /// </summary>
    private Command commandWrong;
    public Command CommandWrong
    {
        get
        {
            return commandWrong = new Command(() =>
            {
                try
                {
                    IsVisibleFilterPopup = false;
                }
                catch (Exception ex)
                {
                }
            });
        }
    }

    #endregion


}