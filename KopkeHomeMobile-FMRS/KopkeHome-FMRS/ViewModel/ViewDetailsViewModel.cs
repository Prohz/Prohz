




using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.DependancyService;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.ServiceHelper;
using KopkeHome_FMRS.View;
using Microsoft.AppCenter.Crashes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;

using NetworkAccess = Microsoft.Maui.Networking.NetworkAccess;

namespace KopkeHome_FMRS.ViewModel;

public partial class ViewDetailsViewModel : CommonMethdViewModel
{    
   
    #region  Full Constructor
    public ViewDetailsViewModel()
    {          
        _ = GetContractorDetails();     
    }
    #endregion
}