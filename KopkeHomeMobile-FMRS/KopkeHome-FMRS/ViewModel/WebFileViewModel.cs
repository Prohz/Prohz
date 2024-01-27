using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class WebFileViewModel:BaseViewModel
    {
        public WebFileViewModel()
        {

        }
        private string fileName;

        public String FileName
        {
            get { return fileName; }
            set { fileName = value;

                OnPropertyChanged(nameof(FileName));
            }
        }

        [RelayCommand]
        async void BackClickWeb()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new AppShell();
            });
        }

    }
}
