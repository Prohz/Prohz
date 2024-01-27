using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KopkeHome_FMRS.ViewModel;

public partial class WorkStatusViewModel : BaseViewModel
{
    #region  Full Constructor
    

    public void RefreshWorkStatusPage()
    {
        aPIServices = new Services();
        WorkStatus = new ObservableCollection<WorkStatusModel>();
        GetWorkStatus();
        Now = Constants.NotCheckBoxImage;
        Soon = Constants.NotCheckBoxImage;
        Later = Constants.NotCheckBoxImage;
    }
    #endregion

    #region  Full Property
    public int statusId = 0;
    private IServices aPIServices;

    /// <summary>
    /// This property is used for Now
    /// </summary>
    private string now = Constants.NotCheckBoxImage;
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
    private string soon = Constants.NotCheckBoxImage;
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
    private string later = Constants.NotCheckBoxImage;
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
    /// Propert for workStatus
    /// </summary>
    private ObservableCollection<WorkStatusModel> workStatus;
    public ObservableCollection<WorkStatusModel> WorkStatus
    {
        get { return workStatus; }
        set { workStatus = value; OnPropertyChanged(nameof(WorkStatus)); }
    }
    /// <summary>
    /// Propert for ChangeWorkStatusColor
    /// </summary>
    private Color changeWorkStatusColor;
    public Color ChangeWorkStatusColor
    {
        get { return changeWorkStatusColor; }
        set { changeWorkStatusColor = value; OnPropertyChanged(nameof(ChangeWorkStatusColor)); }
    }

    /// <summary>
    /// Propert for IsVisibleFilterPopup
    /// </summary>
    private bool isVisibleFilterPopup;
    public bool IsVisibleFilterPopup
    {
        get { return isVisibleFilterPopup; }
        set { isVisibleFilterPopup = value; OnPropertyChanged(nameof(IsVisibleFilterPopup)); }
    }
    /// <summary>
    /// Propert for GreenCheckBoxImg
    /// </summary>
    private string greenCheckBoxImg = Constants.NotCheckBoxImage;
    public string GreenCheckBoxImg
    {
        get { return greenCheckBoxImg; }
        set { greenCheckBoxImg = value; OnPropertyChanged(nameof(GreenCheckBoxImg)); }
    }
    /// <summary>
    /// Propert for YellowCheckBoxImg
    /// </summary>
    private string yellowCheckBoxImg = Constants.NotCheckBoxImage;
    public string YellowCheckBoxImg
    {
        get { return yellowCheckBoxImg; }
        set { yellowCheckBoxImg = value; OnPropertyChanged(nameof(YellowCheckBoxImg)); }
    }
    /// <summary>
    /// Propert for RedCheckBoxImg
    /// </summary>
    private string redCheckBoxImg = Constants.NotCheckBoxImage;
    public string RedCheckBoxImg
    {
        get { return redCheckBoxImg; }
        set { redCheckBoxImg = value; OnPropertyChanged(nameof(RedCheckBoxImg)); }
    }
    #endregion


    public WorkStatusViewModel()
    {
        RefreshWorkStatusPage();
    }
    #region  Full Methods

    /// <summary>
    /// Method for GetWorkStatus
    /// </summary>
    public void GetWorkStatus()
    {
        try
        {
            WorkStatus = new ObservableCollection<WorkStatusModel>(new[]
            {
                    new WorkStatusModel {BackgroundColor = "Green", StatusName=Constants.Green},
                     new WorkStatusModel {BackgroundColor = "Yellow", StatusName=Constants.Yellow},
                      new WorkStatusModel {BackgroundColor = "Red", StatusName=Constants.Red},
            });

            var workId = Preferences.Get(Constants.WorkStatus, 0);
            Utilities.WorkStatusId = workId;
            if (!string.IsNullOrEmpty(UserProperties.StatusImage))
            {
                if (UserProperties.StatusImage == Constants.Available)
                {
                    Now = Constants.Green;
                    Soon = Constants.NotCheckBoxImage;
                    Later = Constants.NotCheckBoxImage;
                }
                else if (UserProperties.StatusImage == Constants.Appoint)
                {
                    Now = Constants.NotCheckBoxImage;
                    Soon = Constants.YellowCheckBoximage;
                    Later = Constants.NotCheckBoxImage;
                }
                else if (UserProperties.StatusImage == Constants.Booked)
                {
                    Now = Constants.Green;
                    Soon = Constants.NotCheckBoxImage;
                    Later = Constants.Red;
                }
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex); 
        }
    }
    public int SavedStatusId = 0;
    /// <summary>
    /// This command is used for the SaveStatusCommand
    /// </summary>
    public ICommand SaveStatusCommand
    {
        get
        {
            return new Command(async () =>
            {
                await SaveStatus();
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

                    Now = Constants.GreenCheckBoximage;
                    Soon = Constants.NotCheckBoxImage;
                    Later = Constants.NotCheckBoxImage;

                }
                catch (Exception ex)
                {

                }
            });
        }
    }

    /// <summary>
    /// This command is used for the CommandSoon
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
                    Now = Constants.NotCheckBoxImage;
                    Soon = Constants.YellowCheckBoximage;
                    Later = Constants.NotCheckBoxImage;
                }
                catch (Exception ex)
                {
                }
            });
        }
    }

    /// <summary>
    /// This command is used for the CommandLater
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
                    Now = Constants.NotCheckBoxImage;
                    Soon = Constants.NotCheckBoxImage;
                    Later = Constants.RedCheckBoximage;
                }
                catch (Exception ex)
                {
                }
            });
        }
    }



    /// <summary>
    /// This command is used for the Workstatus
    /// </summary>
    private Command workstatus;
    public Command Workstatus
    {
        get
        {
            return workstatus = new Command(() =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                }
            });
        }
    }

    #endregion

    #region Full Methods
    /// <summary>
    /// Method for SaveStatus
    /// </summary>
    public async Task SaveStatus()
    {
        try
        {    if(Now==Constants.NotCheckBoxImage && Later == Constants.NotCheckBoxImage && Soon == Constants.NotCheckBoxImage)
            {
               await Application.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.txtSelectWorkStatusOption, AppResources.OK);
            }
            else
            {
                IsLoading = true;
                UserWorkStatusRequestModel userworkstatusrequestmodel = new UserWorkStatusRequestModel()
                {
                    UserId = Convert.ToInt32(Preferences.Get(Constants.UserID, string.Empty)),
                    RadioWorkStatus = statusId,
                };
                UserWorkStatusResponseModel response = await aPIServices.UserWorkStatus(userworkstatusrequestmodel);
                if (response != null)
                {
                    SavedStatusId = response.WorkStatus;
                    Status = response.WorkStatus.ToString();
                   // Preferences.Set("WorkStatus", Status);
                    Preferences.Set(Constants.WorkStatus, Status);

                    Utilities.getStatus?.Invoke();
                    Utilities.WorkStatusId = response.WorkStatus;
                    if (response.WorkStatus == 0)
                    {
                        Current = Constants.Available;
                        Now = Constants.Green;
                        Soon = Constants.NotCheckBoxImage;
                        Later = Constants.NotCheckBoxImage;

                        IsVisibleFilterPopup = false;
                        UserProperties.StatusImage = Current;
                    }

                    else if (response.WorkStatus == 1)
                    {
                        Current = Constants.Appoint;
                        Now = Constants.NotCheckBoxImage;
                        Soon = Constants.YellowCheckBoximage;
                        Later = Constants.NotCheckBoxImage;

                        IsVisibleFilterPopup = false;
                        UserProperties.StatusImage = Current;
                    }
                    else
                    {
                        Current = Constants.Booked;
                        Now = Constants.NotCheckBoxImage;
                        Soon = Constants.NotCheckBoxImage;
                        Later = Constants.RedCheckBoximage;

                        IsVisibleFilterPopup = false;
                        UserProperties.StatusImage = Current;

                    }
                    Utilities.IsFromStatusChanged = true;
                    await Shell.Current.GoToAsync("//MainPage");
                    _ = Application.Current.MainPage.DisplayAlert(AppResources.Success, AppResources.StatusChangedSuccessfully, AppResources.OK);
                    MessagingCenter.Send<WorkStatusViewModel>(this, Constants.WorkStatusChanged);
                }
            }                  
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
        finally
        {
            IsLoading = false;
        }
    }
    #endregion

}





