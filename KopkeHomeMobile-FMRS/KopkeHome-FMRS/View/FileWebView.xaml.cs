using KopkeHome_FMRS.Helper;

namespace KopkeHome_FMRS.View;

public partial class FileWebView : ContentPage
{
	public FileWebView(string value)
	{
        InitializeComponent();
       // Webview.Source = Constants.ImagesBaseUrl + value;
        // Webview.Source=  "https://upload.wikimedia.org/wikipedia/commons/9/9a/Gull_portrait_ca_usa.jpg";
        Webview.Source = "https://docs.google.com/viewer?url="+ Constants.ImagesBaseUrl + value;
        vm.FileName = value;
    }

	private void webview_Navigating(object sender, WebNavigatingEventArgs e)
	{
        //loader.IsVisible=true;

     }

	private void webview_Navigated(object sender, WebNavigatedEventArgs e)
	{
       // loader.IsVisible = false;
    }
}