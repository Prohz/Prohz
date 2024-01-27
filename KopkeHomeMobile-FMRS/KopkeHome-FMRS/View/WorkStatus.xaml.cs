using KopkeHome_FMRS.ViewModel;

namespace KopkeHome_FMRS.View;

public partial class WorkStatus : ContentPage
{
	public WorkStatus()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {       
        vm.RefreshWorkStatusPage();    
    }

}