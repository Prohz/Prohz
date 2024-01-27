
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Exception = System.Exception;

namespace KopkeHome_FMRS.ViewModel;

public partial class ForgotPasswordViewModel : BaseViewModel
{

    public IServices aPIServices;
    [ObservableProperty]
    bool isNewPassword;
    [ObservableProperty]
    string newEyeImage = Constants.Close_Eye_Icon;
    [ObservableProperty]
    bool isConfirmPassword;
    [ObservableProperty]
    string confirmEyeImage = Constants.Close_Eye_Icon;
    #region Constructor
    public ForgotPasswordViewModel()
    {
        aPIServices = new Services();
        IsVisibleEmailEntry=true;
        IsNewPassword=true;
        IsConfirmPassword=true;      
    }

    #endregion

    #region  Full Property
    [ObservableProperty]
    string alertText;
    [ObservableProperty]
    string alertNewText;
    private string email;
    public string Email
    {
        get { return email; }
        set
        {
            email = value;
            OnPropertyChanged(nameof(Email));
            if (!string.IsNullOrEmpty(Email))
            {              
                IsEmailErrorVisible = false;
            }
        }
    }
    
    private bool isEmailErrorVisible;
    public bool IsEmailErrorVisible
    {
        get { return isEmailErrorVisible; }
        set
        {
            isEmailErrorVisible = value;
            OnPropertyChanged(nameof(IsEmailErrorVisible)); 
        }
    }
    
    bool isVisibleEmailEntry;
    public bool IsVisibleEmailEntry
    {
        get { return isVisibleEmailEntry; }
        set
        {
            isVisibleEmailEntry = value;
            OnPropertyChanged(nameof(IsVisibleEmailEntry));
        }
    }
    bool isNewPasswordErrorVisible;
    public bool IsNewPasswordErrorVisible
    {
        get { return isNewPasswordErrorVisible; }
        set
        {
            isNewPasswordErrorVisible = value;
            OnPropertyChanged(nameof(IsNewPasswordErrorVisible));
        }
    }
    private bool isVisibleNewPasswordEntry;
    public bool IsVisibleNewPasswordEntrys
    {
        get { return isVisibleNewPasswordEntry; }
        set
        {
            isVisibleNewPasswordEntry = value;
            OnPropertyChanged(nameof(IsVisibleNewPasswordEntrys));
        }
    }
    private string newPassword;

    public string NewPassword
    {
        get { return newPassword; }
        set { newPassword = value;
            OnPropertyChanged(nameof(newPassword));
            if (!string.IsNullOrEmpty(NewPassword))
            {
                // ValidateEmail();
                IsNewPasswordErrorVisible = false;
            }
            else
            {
                IsNewPasswordErrorVisible = true;
                AlertNewText = AppResources.AlertEnterNewPassword;
            }
        }
    }
    private string confirmPassword;

    public string ConfirmPassword
    {
        get { return confirmPassword; }
        set
        {
            confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
            if (!string.IsNullOrEmpty(ConfirmPassword))
            {
                // ValidateEmail();
                IsEmailErrorVisible = false;
            }
            else
            {
                IsEmailErrorVisible = true;
                AlertNewText = AppResources.AlertEnterConfirmPassword;
            }
        }
    }
    #endregion
   
    #region Full Commands
    /// <summary>
    /// Navigate to OTP Command
    /// </summary>
    private Command oTPCommand;
    public Command OTPCommand
    {
        get
        {
            return oTPCommand = new Command(async () =>
            {
                try
                {
                    if (ValidateEmail())
                    {
                      await SendOtpRequest();
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            });
        }
    }
    [RelayCommand]
     void NewEyeClick()
    {
        try
        {
            if (IsNewPassword)
            {
                IsNewPassword = false;
                NewEyeImage = Constants.Eye_Icon;
            }
            else
            {
                IsNewPassword = true;
                NewEyeImage = Constants.Close_Eye_Icon;
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
    [RelayCommand]
    void ConfirmEyeClick()
    {
        try
        {
            if (IsConfirmPassword)
            {
                IsConfirmPassword = false;
                ConfirmEyeImage = Constants.Eye_Icon;
            }
            else
            {
                IsConfirmPassword = true;
                ConfirmEyeImage = Constants.Close_Eye_Icon;   
            }
        }
        catch (Exception ex)
        {

            Crashes.TrackError(ex);
        }
    }

    #endregion

    /// <summary>
    /// Method used to validate email 
    /// </summary>
    /// <returns></returns>
    #region Full Methods
    public bool ValidateEmail()
    {
        bool isStatus = false;
        try
        {
            if (!string.IsNullOrEmpty(Email))
            {

                var IsEmailValid = Regex.IsMatch(Email.Trim(), Constants.emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                if (!IsEmailValid)
                {
                    isStatus = false;                
                    IsEmailErrorVisible = true;
                    AlertText = AppResources.ValidEmailError;
                }
                else
                {
                    isStatus = true;                
                }
            }
            else
            {                            
                AlertText = AppResources.PleaseEnterEmailId;
                IsEmailErrorVisible = true;
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
        return isStatus;
    }
    /// <summary>
    /// Method is used send forgot request
    /// </summary>
    public async Task SendOtpRequest()
    {
        try
        {
            IsLoading = true;
            ForgotPasswordRequestModel forgotPasswordRequestModel = new ForgotPasswordRequestModel()
            {
                Email = Email.Trim(),
            };
            var response = await aPIServices.ForgotPassword(forgotPasswordRequestModel);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var postDetails = JsonConvert.DeserializeObject<ResponseModel>(result);
                if (postDetails.statuscode == 200)
                {
                    Preferences.Set(Constants.ResetPasswordToken, postDetails.data);
                    IsVisibleEmailEntry = false;
                    IsVisibleNewPasswordEntrys = true;
                }
                else
                {
                    AlertText = postDetails.message;
                    IsEmailErrorVisible = true;
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
    /// <summary>
    /// Method used to reset password
    /// </summary>
    /// <returns></returns>
    public async Task Resetpassword()
    {
        try
        {
            IsLoading = true;

            ResetPasswordRequestModel resetPasswordRequestModel = new ResetPasswordRequestModel()
            {
                Email = Email.Trim(),
                Token = Preferences.Get(Constants.ResetPasswordToken, string.Empty),
                Password = NewPassword.Trim(),
                ConfirmPassword = ConfirmPassword.Trim(),
            };
            var response = await aPIServices.ResetPassword(resetPasswordRequestModel);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var postDetails = JsonConvert.DeserializeObject<ResponseModel>(result);
                if (postDetails.statuscode == 200)
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.Success, AppResources.PasswordChangeSuccesfulMessage, AppResources.OK);
                    App.Current.MainPage = new Login();
                }
            }
            IsLoading = false;
        }
        catch (Exception ex)
        {
            IsLoading = false;
            Crashes.TrackError(ex);
        }
       
    }

    /// <summary>
    /// Method used to validate password
    /// </summary>
    /// <returns></returns>
    async Task<bool> ValidatePassword()
    {
        var result=false;
        try
        {
           
            if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
            {
                var validPassword = Regex.IsMatch(NewPassword.Trim(), Constants.passwordRegex);
                if (validPassword)
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        result= true;
                    }
                    else
                    {
                        AlertText = AppResources.AlertNewPasswordConfirmSame;
                        IsEmailErrorVisible = true;
                        result= false;
                    }
                }
                else
                {
                    AlertNewText = AppResources.AlertEnterValidPassword;
                    IsNewPasswordErrorVisible = true;
                    result =false;
                }

            }
            else if (string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(ConfirmPassword))
            {
                AlertText = AppResources.AlertEnterConfirmPassword;
                IsEmailErrorVisible = true;
                AlertNewText = AppResources.AlertEnterNewPassword;
                IsNewPasswordErrorVisible = true;
                result =false;
            }
            else if (string.IsNullOrEmpty(NewPassword))
            {
                AlertNewText = AppResources.AlertEnterNewPassword;
                IsNewPasswordErrorVisible = true;
                result= false;
            }
            else if (string.IsNullOrEmpty(ConfirmPassword))
            {
                AlertText = AppResources.AlertEnterConfirmPassword;
                IsEmailErrorVisible = true;
                result= false;
            }
            else
            {
                result= false;
            }        

        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
        return result;
    }
    /// <summary>
    /// Teturn on back page
    /// </summary>
    [RelayCommand]
    static void BackClick()
    {
        App.Current.MainPage = new Login();
    }
    /// <summary>
    /// Method used to reset the password
    /// </summary>
    [RelayCommand]
    async void SubmitReset()
    {
       var result= await ValidatePassword();
        if (result)
        {
           await Resetpassword();
        }       
    }
}  
    #endregion
