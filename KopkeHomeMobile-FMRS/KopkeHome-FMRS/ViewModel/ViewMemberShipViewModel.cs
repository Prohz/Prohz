using CommunityToolkit.Mvvm.ComponentModel;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.ServiceHelper;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KopkeHome_FMRS.ViewModel
{
   public partial class ViewMemberShipViewModel: BaseViewModel
   {
        private readonly IServices aPIServices;
        public ViewMemberShipViewModel()
	   {
            aPIServices = new Services();
            _ = GetPlanDetail();
        }
      
        [ObservableProperty]
        string monthalyOrAnual;
        [ObservableProperty]
        string planDuration;
        [ObservableProperty]
        int? numberOfZipCode;
        [ObservableProperty]
        int? numberOfCategories;
        [ObservableProperty]
        int? priceMonthly;
        [ObservableProperty]
        int? priceYearly;
        [ObservableProperty]
        bool? mobileApp;
        [ObservableProperty]
        bool? webApp;
        [ObservableProperty]
        bool isYearly;
        [ObservableProperty]
       string? stripePriceMonthly;
       [ObservableProperty]
       string? stripePriceYearly;
        [ObservableProperty]
        string? descrption;

        public async Task GetPlanDetail()
        {
            try
            {
                GetActivePlanRequestModel requestModel = new GetActivePlanRequestModel()
                {
                    UserId = Preferences.Get(Constants.UserID.ToString(), string.Empty)
                };
                var response = await aPIServices.GetActivePlanDetail(requestModel);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var planDetails = JsonConvert.DeserializeObject<ActivePlanDetailResponseModel>(result);
                    if (planDetails != null)
                    {
                        if (planDetails.IsYearly)
                        {
                            PlanDuration = "Price(365 days)";
                            MonthalyOrAnual = "$100/365";
                        }
                        else 
                        {
                            PlanDuration = "Price(90 days)";
                            MonthalyOrAnual = "$ 50/90";
                        }
                        NumberOfZipCode = planDetails.NumberOfZipcodes;
                        NumberOfCategories = planDetails.NumberOfCategories;
                        PriceMonthly = planDetails.PriceMonthly;
                        PriceYearly = planDetails.PriceYearly;
                        MobileApp = planDetails.MobileApp;
                        WebApp = planDetails.WebApp;
                        IsYearly = planDetails.IsYearly;
                        Descrption = planDetails.Descrption;
                        StripePriceMonthly = planDetails.StripePriceMonthly;
                        StripePriceYearly = planDetails.StripePriceYearly;
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }   
          
        }
    }
}
