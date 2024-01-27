using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class BusinessProfileViewModel: CommonMethdViewModel
    {
        public BusinessProfileViewModel()
	   {
		  try
		  {
                _ = GetContractorDetails();
            }
		  catch (Exception ex)
		  {
			Crashes.TrackError(ex);
		  }
            
        }
    }
}
