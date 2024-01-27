using Android;
using Android.App;
using Android.Content.PM;

using Android.OS;
using Android.Runtime;
using Android.Webkit;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;

namespace KopkeHome_FMRS;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    const int RequestLocationId = 0;
    const int StoragePermission = 3;
    
    readonly string[] LocationPermissions =
      {
        Manifest.Permission.AccessCoarseLocation,
        Manifest.Permission.AccessFineLocation
        };

    readonly string[] WriteStoragePermission =
     {
        Manifest.Permission.WriteExternalStorage,          
     };
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
      
        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
        Plugin.Media.CrossMedia.Current.Initialize();
        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.RecordAudio, Manifest.Permission.Camera }, 1);
        ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ManageExternalStorage }, 3);
        //CrossCurrentActivity.Current.Activity = this;
        //AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
    protected override void OnStart()
    {
        base.OnStart();

        if ((int)Build.VERSION.SdkInt >= 23)
        {
           
            
            if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                RequestPermissions(LocationPermissions, RequestLocationId);
            }
            else
            {
                return;
            }
            if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                RequestPermissions(WriteStoragePermission, StoragePermission);
            }
            else
            {
                return;
            }
        }
      
    }
}
