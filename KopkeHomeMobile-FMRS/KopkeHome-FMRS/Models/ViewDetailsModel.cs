using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Models
{
    public class ViewDetailsModel : BindableObject
    {
        private string serviceName;
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; OnPropertyChanged(nameof(ServiceName)); }
        }

        private string zipCode;
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; OnPropertyChanged(nameof(ZipCode)); }
        }

    }
}
