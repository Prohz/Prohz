using Android.Webkit;
using KopkeHome_FMRS.DependancyService;
using KopkeHome_FMRS.Platforms.Android.DependancyService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AndroidX.Concurrent.Futures.CallbackToFutureAdapter;
[assembly: Dependency(typeof(AndroidDownLoader))]
namespace KopkeHome_FMRS.Platforms.Android.DependancyService
{
    public class AndroidDownLoader : IDownload
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder)
        {
            //string pathToNewFolder = Path.Combine(Android.Os.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            //Directory.CreateDirectory(pathToNewFolder);

            try
            {
                //WebClient webClient = new WebClient();
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                //string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                //webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                //if (OnFileDownloaded != null)
                //    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
        }
    }
}
