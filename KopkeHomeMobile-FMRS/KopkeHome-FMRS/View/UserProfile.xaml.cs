namespace KopkeHome_FMRS.View;

public partial class UserProfile : ContentPage
{
	public UserProfile()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
      base.OnAppearing();
	 await vm.GetUserProfile();
    }
}