using KopkeHome_FMRS.View;

namespace KopkeHome_FMRS.ViewModel;

public class VerifyOTPpageViewModel : ContentPage
{
    #region Constructor
    public VerifyOTPpageViewModel()
    {

    }
    #endregion

    #region  Full Property
    #endregion

    #region Full Commands
    /// <summary>
    /// Navigate to Reset Command
    /// </summary>
    private Command resetCommand;
    public Command ResetCommand
    {
        get
        {
            return resetCommand = new Command(async () =>
            {
                try
                {
                  //  await App.Current.MainPage.Navigation.PushModalAsync(new ResetPassword());
                }
                catch (Exception ex)
                {
                }
            });
        }
    }


    /// <summary>
    /// Navigate to Reset Command
    /// </summary>
    private Command resetOTP;
    public Command ResetOTP
    {
        get
        {
            return resetOTP = new Command(async () =>
            {
                try
                {
                   // await App.Current.MainPage.Navigation.PushModalAsync(new ResetPassword());
                }
                catch (Exception ex)
                {
                }
            });
        }
    }



    #endregion

    #region Full Methods

    #endregion
}
