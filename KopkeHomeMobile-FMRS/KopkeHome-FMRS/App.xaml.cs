using KopkeHome_FMRS.Control;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Resources;
using KopkeHome_FMRS.View;
using System.Globalization;

namespace KopkeHome_FMRS;

public partial class App : Application
{
    public LocalizationResourceManager LocalizationResourceManager
          => LocalizationResourceManager.Instance;
    public App()
	{
		InitializeComponent();
		Application.Current.UserAppTheme=AppTheme.Light;        
          MainPage = new Login();
          Setlanguage();
     }
    /// <summary>
    /// Method used to set current language
    /// </summary>
    public void Setlanguage()
    {
        var langVal = Preferences.Get(Constants.CurrentLanguage, string.Empty);

        if (langVal == string.Empty || langVal == Constants.ENLanguage)
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName.Equals(Constants.enLanguageCode, StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo(Constants.enUSCode) : new CultureInfo(Constants.enUSCode);
            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
        }
        else
        {
            var switchtoculture = AppResources.Culture.TwoLetterISOLanguageName.Equals(Constants.enLanguageCode, StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo(Constants.esSVCode) : new CultureInfo(Constants.esSVCode);
            LocalizationResourceManager.Instance.SetCulture(switchtoculture);
        }
    }
}
