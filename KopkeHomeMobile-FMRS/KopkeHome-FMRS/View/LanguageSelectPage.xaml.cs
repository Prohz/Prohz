
using KopkeHome_FMRS.Control;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Resources;
using Microsoft.AppCenter.Crashes;
using System.Globalization;

namespace KopkeHome_FMRS.View;

public partial class LanguageSelectPage : ContentPage
{
    bool Initializelanguage=false;
    bool result=false;
    public LocalizationResourceManager LocalizationResourceManager
           => LocalizationResourceManager.Instance;
    public LanguageSelectPage()
	{
		InitializeComponent();
	     BindingContext=this;
       
    }
    /// <summary>
    /// Method used to change the language to English
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void englickCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Initializelanguage)
        {
            if (englickCheckbox.IsChecked)
            {
                await UserConfirmLanguageChange();
                if (result)
                {
                    Preferences.Set(Constants.CurrentLanguage, Constants.ENLanguage);
                    englickCheckbox.IsEnabled = false;
                    SpanishChecbox.IsEnabled = true;
                    SpanishChecbox.IsChecked = false;
                    try
                    {
                        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName.Equals(Constants.enLanguageCode, StringComparison.InvariantCultureIgnoreCase) ?
                        new CultureInfo(Constants.enUSCode) : new CultureInfo(Constants.enUSCode);
                        LocalizationResourceManager.Instance.SetCulture(switchToCulture);
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                }
                else
                {
                    englickCheckbox.IsChecked = false;
                }
            }            
        }     
    }
    /// <summary>
    /// Method used to change language to spanish
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SpanishChecbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            if (Initializelanguage)
            {
                if (SpanishChecbox.IsChecked)
                {
                    await UserConfirmLanguageChange();
                    if (result)
                    {
                        Preferences.Set(Constants.CurrentLanguage, Constants.ESLanguage);
                        SpanishChecbox.IsEnabled = false;
                        englickCheckbox.IsEnabled = true;
                        englickCheckbox.IsChecked = false;
                        try
                        {
                            var switchtoculture = AppResources.Culture.TwoLetterISOLanguageName.Equals(Constants.enLanguageCode, StringComparison.InvariantCultureIgnoreCase) ?
                            new CultureInfo(Constants.esSVCode) : new CultureInfo(Constants.esSVCode);
                            LocalizationResourceManager.Instance.SetCulture(switchtoculture);
                        }
                        catch (Exception ex)
                        {
                            Crashes.TrackError(ex);
                        }
                    }
                    else
                    {
                        SpanishChecbox.IsChecked = false;
                    }
                }
            }
           
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
       
    }
    /// <summary>
    /// Method used to get user confirmation for language change
    /// </summary>
    /// <returns></returns>
    async Task UserConfirmLanguageChange()
    {
        bool Res = await Application.Current.MainPage.DisplayAlert(AppResources.Alert, AppResources.txtDoYouWantChangeLanguage, AppResources.Yes, AppResources.Cancel);
        result= Res;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var langVal = Preferences.Get(Constants.CurrentLanguage, string.Empty);
            if (langVal == string.Empty || langVal == Constants.ENLanguage?.ToString())
            {

                englickCheckbox.IsChecked = true;
                Initializelanguage = true;
            }
            else
            {
                SpanishChecbox.IsChecked = true;
                Initializelanguage = true;
            }
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }
    }
}
