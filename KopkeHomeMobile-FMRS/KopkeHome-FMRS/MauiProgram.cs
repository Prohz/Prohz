using KopkeHome_FMRS.View;
using KopkeHome_FMRS.ViewModel;
using CommunityToolkit.Maui;
using KopkeHome_FMRS.Resources;

namespace KopkeHome_FMRS;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
      
       

        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
        builder.Services.AddTransient<Login>();
        builder.Services.AddTransient<LoginViewModel>();           
        return builder.Build();
    }
}