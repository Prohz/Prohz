

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
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.RegularExpressions;

namespace KopkeHome_FMRS.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IServices aPIServices;
    #region Constructor
    public LoginViewModel()
    {
        aPIServices = new Services();
        IsEmailErrorVisible = false;
        IsPasswordErrorVisible = false;
         IsPassword = true;    
        if (Preferences.Get(Constants.IsToggled, string.Empty) == Constants.True)
        {
            IsRememeberMe = true;
            Email = Preferences.Get(Constants.Email, string.Empty);
            Password = Preferences.Get(Constants.Password, string.Empty);
        }
        else if(Preferences.Get(Constants.IsToggled, string.Empty) == Constants.False)
        {
            IsRememeberMe = false;
            Preferences.Set(Constants.Email, string.Empty);
            Preferences.Set(Constants.Password, string.Empty);
        }
        else
        {
            IsRememeberMe = false;
            Preferences.Set(Constants.Email, string.Empty);
            Preferences.Set(Constants.Password, string.Empty);
        }
    }
    #endregion 

    #region  Full Property
   
    
    string email;
    public string Email
    {
        get { return email; }
        set
        {
            email = value;
            OnPropertyChanged(nameof(Email));
            if (!string.IsNullOrEmpty(Password))
            {
                IsEmailErrorVisible = false;
            }         
        }
    }

    string password;
    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged(nameof(Password));
            if (!string.IsNullOrEmpty(Password))
            {
                IsPasswordErrorVisible = false;
            }
            else
            {
                IsPasswordErrorVisible = true;
            }
        }
    }
 
    bool isEmailErrorVisible;
    public bool IsEmailErrorVisible
    {
        get { return isEmailErrorVisible; }
        set
        {
            isEmailErrorVisible = value;
            OnPropertyChanged(nameof(IsEmailErrorVisible));
        }
    }
 
    bool isPasswordErrorVisible;
    public bool IsPasswordErrorVisible
    {
        get { return isPasswordErrorVisible; }
        set
        {
            isPasswordErrorVisible = value;
            OnPropertyChanged(nameof(IsPasswordErrorVisible));
        }
    }
    private bool isRememeberMe=false;

    public bool IsRememeberMe
    {
        get { return isRememeberMe; }
        set { isRememeberMe = value;
            OnPropertyChanged(nameof(IsRememeberMe));
            if (IsRememeberMe)
            {              
                ThumbColor = Color.FromArgb("#add136");
                Preferences.Set(Constants.IsToggled, Constants.True);
            }
            if (!IsRememeberMe)
            {               
                Preferences.Set(Constants.Email, String.Empty);
                Preferences.Set(Constants.Password, String.Empty);
                Preferences.Set(Constants.IsToggled, Constants.False);
              
                ThumbColor = Color.FromArgb("#808080");
            }
        }
    }


    [ObservableProperty]
    string emailError;
    [ObservableProperty]
    bool isPassword;
    [ObservableProperty]
    Color thumbColor;   
    [ObservableProperty]
    string eyeImage = Constants.Close_Eye_Icon;
    #endregion

    #region Full Commands   
    [RelayCommand]
    void Login()
    {
        try
        {
           LogIn();        
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex); 
        }
    }

    [RelayCommand]
    void ForgotPassword()
    {
        try
        {
            App.Current.MainPage = new ForgotPassword();          
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }   
   public void Toggled(bool reslt)
    {       
        if (!reslt)
        {
            Preferences.Set(Constants.IsToggled, Constants.False);          
        }
        else
        {
            Preferences.Set(Constants.IsToggled, Constants.True);
        }
    }
    [RelayCommand]
    void PasswordEye()
    {
        if (IsPassword)
        {
            IsPassword = false;
            EyeImage = Constants.Eye_Icon;
        }
        else
        {
            IsPassword = true;
            EyeImage = Constants.Close_Eye_Icon;
        }
    }
    #endregion

    #region Full Methods
    /// <summary>
    /// Method for LogIn
    /// </summary>
    public async void LogIn()
    {
        bool IsStatus = false;
        try
        {
            IsLoading= true;
            if (!string.IsNullOrEmpty(Email))
            {
                var IsEmailValid = Regex.IsMatch(Email.Trim(), Constants.emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                if (!IsEmailValid)
                {
                    IsStatus = false;
                    EmailError = AppResources.ValidEmailError;
                    IsEmailErrorVisible = true;
                }
                else
                {
                    IsStatus = true;
                    IsEmailErrorVisible = false;
                }
            }
            else
            {
                EmailError = AppResources.ValidEmailError;
                IsEmailErrorVisible = true;
            }
            if (string.IsNullOrEmpty(Password))
            {
                IsPasswordErrorVisible = true;
            }
            LoginRequestMode loginRequestModel = new LoginRequestMode()
            {
                Email = email.Trim(),               
                Password = password.Trim(),
                IsSuccess = true,
                IsRememberme = IsRememeberMe
            };
            ResponseModel response = await aPIServices.UserLogin(loginRequestModel);

            if (response.statuscode == 200 && response.status == "Success")
            {
                if (IsRememeberMe)
                {
                    Preferences.Set(Constants.Email, Email);
                    Preferences.Set(Constants.Password, Password);
                }
                Preferences.Set(Constants.Email, Email);
                Preferences.Set(Constants.Password, Password);
                Preferences.Set(response.data, Email);
                UserProperties.Token = response.data;
                Constants.token = response.data;
                var k = Constants.token;
                Preferences.Set(Constants.AppToken, response.data);
                Preferences.Set(Constants.token , response.data);
                LoginViewModel.GoToAppCell();

            }
            else if (response.statuscode == 404)
            {
                _ = Application.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.InvalidCredential, AppResources.OK);
            }
            else
            {
              
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
        finally { IsLoading = false; }
    }

    public static void GoToAppCell()
    {
        App.Current.MainPage = new AppShell();
    }
    /// <summary>
    /// Method for ValidateEmail
    /// </summary>
    public void ValidateEmail()
    {
        bool IsStatus = false;
        try
        {
            if (!string.IsNullOrEmpty(Email))
            {
                var IsEmailValid = Regex.IsMatch(Email.Trim(), Constants.emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                if (!IsEmailValid)
                {
                    IsStatus = false;
                    EmailError = AppResources.ValidEmailError;                  
                    IsEmailErrorVisible = true;
                }
                else
                {
                    IsStatus = true;
                    IsEmailErrorVisible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }     
    }
    #endregion

}
