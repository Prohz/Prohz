

namespace KopkeHome_FMRS.View;

public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();
    }

    private async void ZipcodeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            vm.ZipCodeTextChanged();
            
        }
        catch (Exception ex)
        {

        }
    }

    private async void CategoryEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {            
           await vm.CategoryTextChanged();
        }
        catch (Exception ex)
        {

        }
    }

    private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
    {
        try
        {
            vm.IsLoading = true;
           
        }
        catch (Exception)
        {

            throw;
        }
        vm.IsLoading = false;
    }
    protected override void OnAppearing()
    {
      // vm.RefreshData();        
    }
}