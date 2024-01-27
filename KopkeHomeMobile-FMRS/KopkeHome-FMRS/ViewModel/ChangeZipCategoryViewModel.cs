using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.ServiceHelper;
using Microsoft.AppCenter.Crashes;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ViewModel
{
    public partial class ChangeZipCategoryViewModel : BaseViewModel
    {
        private IServices aPIServices;
        public ChangeZipCategoryViewModel()
        {
            aPIServices = new Services();
            StateList = new ObservableCollection<StateListModel>();
            CityList = new ObservableCollection<CityListModel>();
            ZipCodeList= new ObservableCollection<ZipListModel>();           
        }     
        [ObservableProperty]
        string stateName = Constants.StateDefaultEntry;
        [ObservableProperty]
        string stateID = Constants.StateDefaultEntry;
        [ObservableProperty]
        bool visibleStateList = false;
        [ObservableProperty]
        bool visibleCityList = false;
        [ObservableProperty]
        string cityName ;
        [ObservableProperty]
        string cityID ;
        [ObservableProperty]
        bool visibleServiceZip;
        [ObservableProperty]
        string zip_Code;
        private ObservableCollection<StateListModel> stateList;
        public ObservableCollection<StateListModel> StateList
        {
            get { return stateList; }
            set
            {
                stateList = value;
                OnPropertyChanged(nameof(StateList));
            }
        }
        private ObservableCollection<CityListModel> cityList;
        public ObservableCollection<CityListModel> CityList
        {
            get { return cityList; }
            set
            {
                cityList = value;
                OnPropertyChanged(nameof(CityList));
            }
        }
        /// <summary>
        /// This property is used for Service Offered List
        /// </summary>
        private ObservableCollection<ZipListModel> zipCodeList;
        public ObservableCollection<ZipListModel> ZipCodeList
        {
            get { return zipCodeList; }
            set { zipCodeList = value; OnPropertyChanged(nameof(ZipCodeList)); }
        }
        /// <summary>
        /// Method use to show selected state name
        /// </summary>
        private Command stateSelected;
        public Command StateSelected
        {
            get
            {
                return stateSelected = new Command(async (param) =>
                {
                    var selectedItem = param as StateListModel;
                    VisibleStateList = false;
                    VisibleCityList=false;
                    StateName = selectedItem.StateName;
                    StateID = selectedItem.StateId.ToString();
                });
            }
        }
        /// <summary>
        /// Method use to show selected city name
        /// </summary>
        private Command citySelected;
        public Command CitySelected
        {
            get
            {
                return stateSelected = new Command(async (param) =>
                {
                    var selectedItem = param as CityListModel;
                    VisibleCityList = false;
                    VisibleStateList = false;
                    CityName = selectedItem.CityName;
                    CityID = selectedItem.Id.ToString();
                   await GetZipCodeList(CityName);
                });
            }
        }
        /// <summary>
        /// Method use to show selected zip name
        /// </summary>
        private Command zipSelected;
        public Command ZipSelected
        {
            get
            {
                return zipSelected = new Command(async (param) =>
                {
                    var selectedItem = param as ZipListModel;
                    VisibleCityList = false;
                    VisibleStateList = false;
                    Zip_Code = selectedItem.Zipcode;
                    CityName = selectedItem.CityName.ToString();
                    //await GetZipCodeList(CityName);
                });
            }
        }

        [RelayCommand]
        async void FindStateList()
        {

            await GetState();
        }
        async  Task GetState()
        {
            IsLoading = true;
            if (VisibleStateList)
            {
                VisibleStateList = false;
            }
            else
            {
                VisibleStateList = true;
                try
                {
                    var response = await aPIServices.GetStateList();
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var postDetails = JsonConvert.DeserializeObject<List<StateListModel>>(result);
                        if (postDetails != null)
                        {
                            StateList = new ObservableCollection<StateListModel>();

                            foreach (var item in postDetails)
                            {
                                item.StateSelectedCommand = StateSelected;
                                StateList.Add(item);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    IsLoading = false;
                }
            }
            IsLoading = false;
        }
        [RelayCommand]
        async void GetCityList()
        {
            IsLoading = true;
            if (VisibleCityList)
            {
                VisibleCityList=false;
            }
            else
            {
                try
                {
                    GetCityListRequestModel getCityListRequestModel = new GetCityListRequestModel()
                    {
                        Id = StateID
                    };
                    var response = await aPIServices.GetCityList(getCityListRequestModel);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var postDetails = JsonConvert.DeserializeObject<List<CityListModel>>(result);
                        if (postDetails != null)
                        {
                            CityList = new ObservableCollection<CityListModel>();

                            foreach (var item in postDetails)
                            {
                                item.CitySelectedCommand = CitySelected;
                                CityList.Add(item);
                            }
                            VisibleCityList = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                IsLoading = false;
            }
        }
        public async Task GetZipCodeList(string cityname)
        {
            try
            {
                IsLoading = true;
                GetZipListByCitNameRequestModel getZipListByCitNameRequestModel = new GetZipListByCitNameRequestModel()
                {
                    CityName = cityname
                };
                var response = await aPIServices.GetZipListByCityName(getZipListByCitNameRequestModel);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var postDetails = JsonConvert.DeserializeObject<List<ZipListModel>>(result);
                    if (postDetails != null)
                    {
                        ZipListModel zipList = new ZipListModel();
                        foreach (var item in postDetails)
                        {
                            zipList.Zipcode = item.Zipcode;
                            zipList.ZipcodeSelectedCommand = item.ZipcodeSelectedCommand;
                            ZipCodeList.Add(item);
                        }
                        VisibleServiceZip = true;
                    }
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
    }      
}
