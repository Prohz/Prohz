using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.Control;
using KopkeHome_FMRS.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
   public partial class SelectLanguageViewModel:BaseViewModel
    {
        //public LocalizationResourceManager LocalizationResourceManager
        //    => LocalizationResourceManager.Instance    ;
        //[ObservableProperty]
        //bool isEnglishChecked;
        //[ObservableProperty]
        //bool isSpanisChecked;
        private bool isEnglishChecked;

        public bool IsEnglishChecked
        {
            get { return isEnglishChecked; }
            set { isEnglishChecked = value;
            OnPropertyChanged(nameof(IsEnglishChecked));
          
                if (IsEnglishChecked)
                {
                    if (IsSpanisChecked)
                    {
                        IsSpanisChecked = false;
                        ChangeLanguage();
                    }
                }              
            }
        }
        private bool isSpanisChecked;

        public bool IsSpanisChecked
        {
            get { return isSpanisChecked; }
            set
            {
                isSpanisChecked = value;
                OnPropertyChanged(nameof(IsSpanisChecked));
                if (IsSpanisChecked)
                {
                    if (IsEnglishChecked)
                    {
                        ChangeLanguage();
                        //CultureInfo language = new CultureInfo("sp");
                        //Thread.CurrentThread.CurrentUICulture = language;
                        //AppResources.Culture = language;
                        IsEnglishChecked = false;
                    }
                }
            }
        }

        public SelectLanguageViewModel()
        {
           
        }    
        
        public void ChangeLanguage()
        {
            //try
            //{
            //    var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName.Equals("sp", StringComparison.InvariantCultureIgnoreCase) ?
            //    new CultureInfo("sp") : new CultureInfo("en");
            //    LocalizationResourceManager.Instance.SetCulture(switchToCulture);
            //}
            //catch (Exception ex)
            //{
            //    Crashes.TrackError(ex);
            //}           
        }
    }
}
