
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.ViewModel;
using System.Collections.ObjectModel;

namespace KopkeHome_FMRS;
public partial class AppShell : Shell
{
    public AppShell()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }     
        var vesrion = VersionTracking.CurrentVersion;
        lblVersionNumber.Text = vesrion;     
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.GetUserByEmail();
        await VM.SetStatus();

    }
    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        VM.Logout();
    }

    private void MenuItem_Clicked_1(object sender, EventArgs e)
    {

    }
}
