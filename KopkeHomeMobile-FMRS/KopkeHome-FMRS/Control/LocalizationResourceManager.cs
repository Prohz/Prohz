using KopkeHome_FMRS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Control
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private LocalizationResourceManager()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }
        public static LocalizationResourceManager Instance { get; } = new();
        public Object this[String resourceKey] => AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? Array.Empty<byte>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetCulture(CultureInfo culture)
        {
            AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
