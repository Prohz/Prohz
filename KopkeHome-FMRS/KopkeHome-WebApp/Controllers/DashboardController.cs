using AutoMapper;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels.BusinessProfileModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels.MembershipBenifitsAndSubcriptionPlan;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels.UsersServicesModels;
using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.Session;
using KopkeHome_WebApp.WebUtility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
namespace KopkeHome_WebApp.Controllers
{
#nullable disable

    [ValidateSession]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class DashboardController : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly IMapper _Mapper;
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _configuration;

        public DashboardController(IWebHostEnvironment _environment, ILogger<DashboardController> logger, IConfiguration iConfig, IMapper mapper)
        {
            Environment = _environment;
            _logger = logger;
            _Mapper = mapper;
            _configuration = iConfig;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            Dashboard dashboard = new Dashboard();
            try
            {
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                    var httpResponse = await client.GetAsync("GetContractors");
                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {

                        return Json("UnAuthorized");
                    }
                    var content = await httpResponse.Content.ReadAsStringAsync();

                    if (httpResponse.IsSuccessStatusCode)
                    {

                        dashboard.CurrentUser = HttpContext.Session.Get<User>("CurrentUser");
                        if (dashboard.CurrentUser != null)
                        {
                            if (dashboard.CurrentUser.RoleId == Constant.HomeOwner)
                            {
                                ViewBag.ZipCode = dashboard.CurrentUser.ZipCode;
                            }

                            dashboard.LoggedInUser = dashboard.CurrentUser;
                        }
                        //dashboard.Users = JsonConvert.DeserializeObject<User>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


            return View(dashboard);
        }







        [HttpGet]
        public async Task<IActionResult> SearchContractors(SearchContractorsViewModel model)
        {
            try
            {
                Dashboard dashboard = new Dashboard();

                var DoesUserHaveThisZip = await ZipcodeList(model.ZipCode.ToString(), model.UserId.ToString());
                if (!DoesUserHaveThisZip.Any())
                {
                    dashboard.CurrentUser = HttpContext.Session.Get<User>("CurrentUser");
                    if (dashboard.CurrentUser != null && dashboard.CurrentUser.RoleId != Constant.HomeOwner)
                    {
                        dashboard.LoggedInUser = dashboard.CurrentUser;
                        return View("index", dashboard);

                    }
                    if (dashboard.CurrentUser.RoleId == Constant.HomeOwner)
                    {
                        model.ZipCode = dashboard.CurrentUser.ZipCode;
                    }

                }


                if (model.Categories != null && model.ZipCode != null)
                {
                    ViewBag.Categories = model.Categories;
                    ViewBag.ZipCode = model.ZipCode;




                    using (var client = new HttpClient())
                    {
                        var accessToken = HttpContext.Request.Cookies["accessToken"];
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");


                        var httpResponse = await client.PostAsJsonAsync<SearchContractorsViewModel>("SearchContractorsList", model);
                        var content = httpResponse.Content.ReadAsStringAsync().Result;

                        var deserialized = JsonConvert.DeserializeObject<List<ContractorListViewModel>>(content);
                        if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {

                            return Json("UnAuthorized");
                        }
                        else if (deserialized != null)
                        {
                            dashboard.CurrentUser = HttpContext.Session.Get<User>("CurrentUser");
                            if (dashboard.CurrentUser != null)
                            {
                                dashboard.LoggedInUser = dashboard.CurrentUser;
                            }
                            dashboard.ContractorList = deserialized;
                            return View("Index", dashboard);

                        }
                    }

                }
                dashboard.CurrentUser = HttpContext.Session.Get<User>("CurrentUser");
                if (dashboard.CurrentUser != null)
                {
                    dashboard.LoggedInUser = dashboard.CurrentUser;
                }
                return View("index", dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("signin", "user");
        }

        [HttpGet]
        public async Task<IActionResult> UsersServices()
        {
            UsersServicesViewModel model = new UsersServicesViewModel();
            var user = HttpContext.Session.Get<User>("CurrentUser");
            if (user != null)
            {

                model.LoggedInUser = user;

            }

            using (var client = new HttpClient())
            {
                var accessToken = HttpContext.Request.Cookies["accessToken"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Email", user.Email.ToString())
                    });
                var httpResponse = await client.PostAsync("GetZipcodesStatesAndCategories", ContentsToSend);

                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<ZipcodesAndCategoriesViewModel>(content);
                    if (deserializedResultList != null)
                    {
                        model.ZipsAndCats = deserializedResultList;
                    }
                }




            }
            return View("UsersServices", model);
        }

        [HttpGet]
        public async Task<IActionResult> UsersServicesEdit()
        {


            return RedirectToAction("UsersServices");
        }



        public IActionResult UserProfileDetails()
        {

            BaseViewModel dashboard = new BaseViewModel();
            User _user = new User();
            var user = HttpContext.Session.Get<User>("CurrentUser");
            if (user != null)
            {

                dashboard.LoggedInUser = user;

            }


            return View("UserProfileDetails", dashboard);
        }


        //edit user basic info
        public async Task<IActionResult> UserProfileBasicEdit()
        {
            BasicInfoViewModel basicInfoViewModel = new BasicInfoViewModel();
            basicInfoViewModel.User = HttpContext.Session.Get<User>("CurrentUser");
            if (basicInfoViewModel.User != null)
            {
                basicInfoViewModel.LoggedInUser = basicInfoViewModel.User;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var httpResponse = await client.GetAsync("GetStates");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<List<State>>(content);
                        basicInfoViewModel.States = deserializedResultList;
                    }
                    else
                    {
                        basicInfoViewModel.States.Add(new State
                        {

                            StateName = "No states found",
                            StateId = 0

                        });

                    }
                }

                return View("UserProfileBasicEdit", basicInfoViewModel);
            }
            else
            {
                return RedirectToAction("signin", "user");
            }


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserBasicInfoHomeOwner(BasicInfoViewModel model)
        {

           
            Common cm = new Common(Environment);
            UpdateBasicInfoHomeOwner basicInfo = new UpdateBasicInfoHomeOwner();
            basicInfo.FirstName = model.FirstName;
            basicInfo.LastName = model.LastName;
            basicInfo.PhoneNumberOffice = model.PhoneNumberOffice;
            basicInfo.PhoneNumber = model.PhoneNumber;
            basicInfo.Id = model.Id;
            basicInfo.City = model.City;
            basicInfo.ProfilePicture = cm.SaveFile(model.ProfilePicture);
            basicInfo.State = model.State;
            basicInfo.RoleId = model.RoleId;
            basicInfo.ZipCode = model.ZipCode;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var httpResponse = await client.PostAsJsonAsync<UpdateBasicInfoHomeOwner>("UpdateBasicInfoHomeOwner", basicInfo);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    HttpContext.Session.Remove("CurrentUser");
                    var currentuser = JsonConvert.DeserializeObject<Response>(content);
                    User user = new User();
                    user = JsonConvert.DeserializeObject<User>(currentuser.Data.ToString());
                    HttpContext.Session.Set<User>("CurrentUser", user);
                    // return Json(content);
                    return RedirectToAction("UserProfileDetails", "Dashboard");
                }
                else
                {
                    return RedirectToAction("UserProfileBasicEdit", "Dashboard");
                }

            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserBasicInfo(BasicInfoViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("UserProfileBasicEdit");
            //}

            Common cm = new Common(Environment);
            UpdateBasicInfo basicInfo = new UpdateBasicInfo();
            basicInfo.FirstName = model.FirstName;
            basicInfo.LastName = model.LastName;
            basicInfo.PhoneNumberOffice = model.PhoneNumberOffice;
            basicInfo.PhoneNumber = model.PhoneNumber;
            basicInfo.Id = model.Id;
            basicInfo.City = model.City;
            basicInfo.BusinessName = model.BusinessName;
            basicInfo.BusinessAddress = model.BusinessAddress;
            basicInfo.ProfilePicture = cm.SaveFile(model.ProfilePicture);
            basicInfo.State = model.State;
            basicInfo.RoleId = model.RoleId;
            basicInfo.ZipCode = model.ZipCode;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var httpResponse = await client.PostAsJsonAsync<UpdateBasicInfo>("UpdateBasicInfo", basicInfo);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    HttpContext.Session.Remove("CurrentUser");
                    var currentuser = JsonConvert.DeserializeObject<Response>(content);
                    User user = new User();
                    user = JsonConvert.DeserializeObject<User>(currentuser.Data.ToString());
                    HttpContext.Session.Set<User>("CurrentUser", user);
                    // return Json(content);
                    return RedirectToAction("UserProfileDetails", "Dashboard");
                }
                else
                {
                    return RedirectToAction("UserProfileBasicEdit", "Dashboard");
                }

            }


        }

        /// <summary>
        ///  This method gets contractors business profile into View page.
        /// </summary>
        public async Task<IActionResult> businessprofiledetail()
        {
            try
            {
                ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();
                User _user = new User();
                var user = HttpContext.Session.Get<User>("CurrentUser");
                if (user != null)
                {

                    model.LoggedInUser = user;

                }
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Id", user.Id.ToString())
                    });
                    var httpResponse = await client.PostAsync("GetContractorProfileDetails", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<ContractorProfileDetailsViewModel>(content);
                        if (deserializedResultList != null)
                        {
                            model = deserializedResultList;
                            HttpContext.Session.Set<ContractorProfileDetailsViewModel>("CurrentUserBusinessProfile", deserializedResultList);
                        }
                    }
                    model.LoggedInUser = user;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<IActionResult> BusinessProfileDetailOnAdmin(int id)
        {
            try
            {
                ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();
                User _user = new User();


                var user = HttpContext.Session.Get<User>("CurrentUser");
                if (user != null)
                {

                    model.LoggedInUser = user;

                }
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
              
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Id", id.ToString())
                    });
                    var httpResponse = await client.PostAsync("GetContractorProfileDetails", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<ContractorProfileDetailsViewModel>(content);
                        if (deserializedResultList != null)
                        {
                            model = deserializedResultList;
                            HttpContext.Session.Set<ContractorProfileDetailsViewModel>("CurrentUserBusinessProfile", deserializedResultList);
                        }
                    }

                    
                   
                }

                User _user1 = new User();
                var user1 = HttpContext.Session.Get<User>("CurrentUser");
                if (user != null)
                {

                    model.LoggedInUser = user;

                }
                using (var clientSub = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    clientSub.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    clientSub.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    //var ContentsToSend1 = new FormUrlEncodedContent(new[]
                    //{
                    //    new KeyValuePair<string, string>("UserId", id.ToString())
                    //});

                    string UserId = id.ToString();
                    var httpResponseSub = await clientSub.GetAsync("GetSubscriptionDetailByUserIdApp" +"?UserId="+ UserId);

                    var contentSub = await httpResponseSub.Content.ReadAsStringAsync();
                    if (httpResponseSub.IsSuccessStatusCode)
                    {
                        var deserializedResultListSub = JsonConvert.DeserializeObject<MembershipPlanViewmodelApp>(contentSub);
                        if (deserializedResultListSub != null)
                        {
                            model.SubscriptinDetails = deserializedResultListSub;
                            HttpContext.Session.Set<MembershipPlanViewmodelApp>("GetSubscriptionDetailByUserIdApp", deserializedResultListSub);
                        }
                    }


                    model.LoggedInUser = user;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }



        /// <summary>
        ///  This method Gets contractors business profile into edit View page.
        /// </summary>
        public IActionResult contractorbusinessprofile()
        {
            try
            {
                EditBusinessProfileOfContractor model = new EditBusinessProfileOfContractor();
                var contractor = HttpContext.Session.Get<ContractorProfileDetailsViewModel>("CurrentUserBusinessProfile");


                if (contractor.BusinessProfileContractor != null)
                {
                    model.AmEx = contractor.BusinessProfileContractor.AmEx;
                    model.Id = contractor.BusinessProfileContractor.Id;
                    model.BusinessDescription = contractor.BusinessProfileContractor.BusinessDescription;
                    model.CommercialLocation = contractor.BusinessProfileContractor.CommercialLocation;
                    model.CompanyWebsiteURL = contractor.BusinessProfileContractor.CompanyWebsiteURL;
                    model.DesignServices = contractor.BusinessProfileContractor.DesignServices;
                    model.ExistingBusinessOrTradeLicenseFiles = contractor.BusinessProfileContractor.BusinessOrTradeLicenseFiles;
                    model.EstimateCharge = contractor.BusinessProfileContractor.EstimateCharge;
                    model.FacebookPageURL = contractor.BusinessProfileContractor.FacebookPageURL;

                    model.Is24HoursPhoneAnswering = contractor.BusinessProfileContractor.Is24HoursPhoneAnswering;
                    model.IsBusinessOrTradeLicense = contractor.BusinessProfileContractor.IsBusinessOrTradeLicense;
                    model.IsCash = contractor.BusinessProfileContractor.IsCash;
                    model.IsCompanyWebsite = contractor.BusinessProfileContractor.IsCompanyWebsite;
                    model.IsContactedBySubcontractors = contractor.BusinessProfileContractor.IsContactedBySubcontractors;
                    model.IsContactedByHomeowners = contractor.BusinessProfileContractor.IsContactedByHomeowners;
                    model.IsDesignServices = contractor.BusinessProfileContractor.IsDesignServices;
                    model.IsLiabilityInsurance = contractor.BusinessProfileContractor.IsLiabilityInsurance;
                    model.IsOfferEmergencyServices = contractor.BusinessProfileContractor.IsOfferEmergencyServices;
                    model.IsPaymentApps = contractor.BusinessProfileContractor.IsPaymentApps;
                    model.IsPhoneCallSupport = contractor.BusinessProfileContractor.IsPhoneCallSupport;
                    model.IsWorkmanCompensationInsurance = contractor.BusinessProfileContractor.IsWorkmanCompensationInsurance;
                    model.JobSiteCrews = contractor.BusinessProfileContractor.JobSiteCrews;
                    model.ExistingLiabilityInsuranceFile = contractor.BusinessProfileContractor.LiabilityInsuranceFile;
                    model.MC = contractor.BusinessProfileContractor.MC;
                    // model.MyPrIsFreeEstimatesoperty = contractor.BusinessProfileContractor.MyPrIsFreeEstimatesoperty;
                    model.NormalBusinessHours = contractor.BusinessProfileContractor.NormalBusinessHours;
                    model.NumberOfEmployees = contractor.BusinessProfileContractor.NumberOfEmployees;
                    model.OtherCreditCard = contractor.BusinessProfileContractor.OtherCreditCard;
                    model.PersonalChecks = contractor.BusinessProfileContractor.PersonalChecks;
                    model.ExistingProfilePicture = contractor.BusinessProfileContractor.ProfilePicture;
                    model.IsEstimateCharge = contractor.BusinessProfileContractor.IsEstimateCharge;
                    model.UserId = contractor.BusinessProfileContractor.UserId;
                    model.Visa = contractor.BusinessProfileContractor.Visa;
                    model.WhichPaymentApps = contractor.BusinessProfileContractor.WhichPaymentApps;
                    model.ExistingWorkmanCompensationInsuranceFile = contractor.BusinessProfileContractor.WorkmanCompensationInsuranceFile; ;
                    model.YearsInBusiness = contractor.BusinessProfileContractor.YearsInBusiness;






                }


                model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        /// <summary>
        ///  This method updates contractors business profile.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContractorBusinessProfile(EditBusinessProfileOfContractor model)
        {
            try
            {
                Common cm = new Common(Environment);
                BusinessProfileDataModel APIModel = new BusinessProfileDataModel();
                APIModel.Id = model.Id;
                APIModel.UserId = model.UserId;
                APIModel.BusinessDescription = model.BusinessDescription;
                APIModel.IsCompanyWebsite = model.IsCompanyWebsite;
                APIModel.CompanyWebsiteURL = cm.CheckHttp(model.CompanyWebsiteURL);
                APIModel.IsFacebookPage = model.IsFacebookPage;
                APIModel.FacebookPageURL = cm.CheckHttp(model.FacebookPageURL);
                APIModel.YearsInBusiness = model.YearsInBusiness;
                APIModel.CommercialLocation = model.CommercialLocation;
                APIModel.NumberOfEmployees = model.NumberOfEmployees;
                APIModel.JobSiteCrews = model.JobSiteCrews;
                APIModel.IsPhoneCallSupport = model.IsPhoneCallSupport;
                APIModel.NormalBusinessHours = model.NormalBusinessHours;
                APIModel.Is24HoursPhoneAnswering = model.Is24HoursPhoneAnswering;
                APIModel.IsOfferEmergencyServices = model.IsOfferEmergencyServices;

                APIModel.IsBusinessOrTradeLicense = model.IsBusinessOrTradeLicense;
                if (model.BusinessOrTradeLicenseFiles != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingBusinessOrTradeLicenseFiles);
                    if (IsDeleted)
                    {
                        APIModel.BusinessOrTradeLicenseFiles = cm.MultiplFiles(model.BusinessOrTradeLicenseFiles);
                    }
                }
                else
                {
                    APIModel.BusinessOrTradeLicenseFiles = model.ExistingBusinessOrTradeLicenseFiles;
                }

                //LIABILITY  INSURANCE
                APIModel.IsLiabilityInsurance = model.IsLiabilityInsurance;
                if (model.LiabilityInsuranceFile != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingLiabilityInsuranceFile);

                    APIModel.LiabilityInsuranceFile = cm.SaveFile(model.LiabilityInsuranceFile);

                }
                else
                {
                    APIModel.LiabilityInsuranceFile = model.ExistingLiabilityInsuranceFile;
                }

                //WORKMEN COMPENSATION
                APIModel.IsWorkmanCompensationInsurance = model.IsWorkmanCompensationInsurance;
                if (model.WorkmanCompensationInsuranceFile != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingWorkmanCompensationInsuranceFile);

                    APIModel.WorkmanCompensationInsuranceFile = cm.SaveFile(model.WorkmanCompensationInsuranceFile);

                }
                else
                {
                    APIModel.WorkmanCompensationInsuranceFile = model.ExistingWorkmanCompensationInsuranceFile;
                }

                APIModel.IsEstimateCharge = model.IsEstimateCharge;
                APIModel.EstimateCharge = model.EstimateCharge;
                APIModel.IsDesignServices = model.IsDesignServices;
                APIModel.DesignServices = model.DesignServices;
                APIModel.IsContactedByHomeowners = model.IsContactedByHomeowners;
                APIModel.IsContactedBySubcontractors = model.IsContactedBySubcontractors;


                //PAYMENT SECTION
                APIModel.IsCash = model.IsCash;
                APIModel.Visa = model.Visa;
                APIModel.MC = model.MC;
                APIModel.AmEx = model.AmEx;
                APIModel.PersonalChecks = model.PersonalChecks;
                APIModel.IsPaymentApps = model.IsPaymentApps;
                APIModel.WhichPaymentApps = model.WhichPaymentApps;

                //COMPANY PROFILE PICTURE
                if (model.ProfilePicture != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingProfilePicture);
                    if (IsDeleted)
                    {
                        APIModel.ProfilePicture = cm.SaveFile(model.ProfilePicture);
                    }

                }
                else
                {
                    APIModel.ProfilePicture = model.ExistingProfilePicture;
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/BusinessProfile/");
                    var httpResponse = await client.PostAsJsonAsync<BusinessProfileDataModel>("UpdateBusinessProfileForContractor", APIModel);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //HttpContext.Session.Remove("CurrentUser");
                        //var currentuser = JsonConvert.DeserializeObject<Response>(content);
                        //User user = new User();
                        //user = JsonConvert.DeserializeObject<User>(currentuser.Data.ToString());
                        //HttpContext.Session.Set<User>("CurrentUser", user);
                        //// return Json(content);
                        return RedirectToAction("businessprofiledetail", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("businessprofiledetail", "Dashboard");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }




        }
        /// <summary>
        ///  This method Gets Other contractors business profile into edit View page.`
        /// </summary>
        public IActionResult OtherContractorBusinessProfile()
        {
            try
            {

                EditBusinessProfileOfOtherContractors model = new EditBusinessProfileOfOtherContractors();
                var contractor = HttpContext.Session.Get<ContractorProfileDetailsViewModel>("CurrentUserBusinessProfile");


                if (contractor.SubContractorBusinessProfile != null)
                {
                    model.AmEx = contractor.SubContractorBusinessProfile.AmEx;
                    model.Id = contractor.SubContractorBusinessProfile.Id;
                    model.BusinessDescription = contractor.SubContractorBusinessProfile.BusinessDescription;
                    //model.CommercialLocation = contractor.SubContractorBusinessProfile.CommercialLocation;
                    model.CompanyWebsiteURL = contractor.SubContractorBusinessProfile.CompanyWebsiteURL;
                    model.DesignServices = contractor.SubContractorBusinessProfile.DesignServices;
                    model.ExistingBusinessOrTradeLicenseFiles = contractor.SubContractorBusinessProfile.BusinessOrTradeLicenseFiles;
                    model.EstimateCharge = contractor.SubContractorBusinessProfile.EstimateCharge;
                    model.FacebookPageURL = contractor.SubContractorBusinessProfile.FacebookPageURL;

                    model.Is24HoursPhoneAnswering = contractor.SubContractorBusinessProfile.Is24HoursPhoneAnswering;
                    model.IsBusinessOrTradeLicense = contractor.SubContractorBusinessProfile.IsBusinessOrTradeLicense;
                    model.IsCash = contractor.SubContractorBusinessProfile.IsCash;
                    model.IsCompanyWebsite = contractor.SubContractorBusinessProfile.IsCompanyWebsite;
                    //  model.IsContactedBySubcontractors = contractor.SubContractorBusinessProfile.IsContactedBySubcontractors;
                    model.IsContactedByHomeowners = contractor.SubContractorBusinessProfile.IsContactedByHomeowners;
                    model.IsContactedByContractors = contractor.SubContractorBusinessProfile.IsContactedByContractors;
                    model.IsDesignServices = contractor.SubContractorBusinessProfile.IsDesignServices;
                    model.IsLiabilityInsurance = contractor.SubContractorBusinessProfile.IsLiabilityInsurance;
                    model.IsOfferEmergencyServices = contractor.SubContractorBusinessProfile.IsOfferEmergencyServices;
                    model.IsEstimateCharge = contractor.SubContractorBusinessProfile.IsEstimateCharge;
                    model.ServiceCallCharge= contractor.SubContractorBusinessProfile.ServiceCallCharge;
                    model.IsPaymentApps = contractor.SubContractorBusinessProfile.IsPaymentApps;
                    model.IsPhoneCallSupport = contractor.SubContractorBusinessProfile.IsPhoneCallSupport;
                    model.IsWorkmanCompensationInsurance = contractor.SubContractorBusinessProfile.IsWorkmanCompensationInsurance;
                    model.JobSiteCrews = contractor.SubContractorBusinessProfile.JobSiteCrews;
                    model.ExistingLiabilityInsuranceFile = contractor.SubContractorBusinessProfile.LiabilityInsuranceFile;
                    model.MC = contractor.SubContractorBusinessProfile.MC;
                    // model.MyPrIsFreeEstimatesoperty = contractor.SubContractorBusinessProfile.MyPrIsFreeEstimatesoperty;
                    model.NormalBusinessHours = contractor.SubContractorBusinessProfile.NormalBusinessHours;
                    model.NumberOfEmployees = contractor.SubContractorBusinessProfile.NumberOfEmployees;
                    model.OtherCreditCard = contractor.SubContractorBusinessProfile.OtherCreditCard;
                    model.PersonalChecks = contractor.SubContractorBusinessProfile.PersonalChecks;
                    model.ExistingProfilePicture = contractor.SubContractorBusinessProfile.ProfilePicture;

                    model.UserId = contractor.SubContractorBusinessProfile.UserId;
                    model.Visa = contractor.SubContractorBusinessProfile.Visa;
                    model.WhichPaymentApps = contractor.SubContractorBusinessProfile.WhichPaymentApps;
                    model.ExistingWorkmanCompensationInsuranceFile = contractor.SubContractorBusinessProfile.WorkmanCompensationInsuranceFile; ;
                    model.YearsInBusiness = contractor.SubContractorBusinessProfile.YearsInBusiness;
                    model.ServiceCallCharge = contractor.SubContractorBusinessProfile.ServiceCallCharge;
                }

                model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }




        }
        /// <summary>
        ///  This method updates contractors business profile.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOtherContractorBusinessProfile(EditBusinessProfileOfOtherContractors model)
        {
            try
            {

                Common cm = new Common(Environment);
                BusinessProfileSubContractor APIModel = new BusinessProfileSubContractor();
                APIModel.Id = model.Id;
                APIModel.UserId = model.UserId;
                APIModel.BusinessDescription = model.BusinessDescription;
                APIModel.IsCompanyWebsite = model.IsCompanyWebsite;
                APIModel.CompanyWebsiteURL = cm.CheckHttp(model.CompanyWebsiteURL);
                APIModel.IsFacebookPage = model.IsFacebookPage;
                APIModel.FacebookPageURL = cm.CheckHttp(model.FacebookPageURL);
                APIModel.YearsInBusiness = model.YearsInBusiness;
                //APIModel.CommercialLocation = model.CommercialLocation;
                APIModel.NumberOfEmployees = model.NumberOfEmployees;
                APIModel.JobSiteCrews = model.JobSiteCrews;
                APIModel.IsPhoneCallSupport = model.IsPhoneCallSupport;
                APIModel.NormalBusinessHours = model.NormalBusinessHours;
                APIModel.Is24HoursPhoneAnswering = model.Is24HoursPhoneAnswering;
                APIModel.IsOfferEmergencyServices = model.IsOfferEmergencyServices;
                APIModel.ServiceCallCharge = model.ServiceCallCharge;
                APIModel.IsBusinessOrTradeLicense = model.IsBusinessOrTradeLicense;
                if (model.BusinessOrTradeLicenseFiles != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingBusinessOrTradeLicenseFiles);
                    if (IsDeleted)
                    {
                        APIModel.BusinessOrTradeLicenseFiles = cm.MultiplFiles(model.BusinessOrTradeLicenseFiles);
                    }
                }
                else
                {
                    APIModel.BusinessOrTradeLicenseFiles = model.ExistingBusinessOrTradeLicenseFiles;
                }

                //LIABILITY  INSURANCE
                APIModel.IsLiabilityInsurance = model.IsLiabilityInsurance;
                if (model.LiabilityInsuranceFile != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingLiabilityInsuranceFile);

                    APIModel.LiabilityInsuranceFile = cm.SaveFile(model.LiabilityInsuranceFile);

                }
                else
                {
                    APIModel.LiabilityInsuranceFile = model.ExistingLiabilityInsuranceFile;
                }

                //WORKMEN COMPENSATION
                APIModel.IsWorkmanCompensationInsurance = model.IsWorkmanCompensationInsurance;
                if (model.WorkmanCompensationInsuranceFile != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingWorkmanCompensationInsuranceFile);

                    APIModel.WorkmanCompensationInsuranceFile = cm.SaveFile(model.WorkmanCompensationInsuranceFile);

                }
                else
                {
                    APIModel.WorkmanCompensationInsuranceFile = model.ExistingWorkmanCompensationInsuranceFile;
                }

                APIModel.IsEstimateCharge = model.IsEstimateCharge;
                APIModel.EstimateCharge = model.EstimateCharge;
                APIModel.IsDesignServices = model.IsDesignServices;
                APIModel.DesignServices = model.DesignServices;
                APIModel.IsContactedByHomeowners = model.IsContactedByHomeowners;
                APIModel.IsContactedByContractors = model.IsContactedByContractors;


                //PAYMENT SECTION
                APIModel.IsCash = model.IsCash;
                APIModel.Visa = model.Visa;
                APIModel.MC = model.MC;
                APIModel.AmEx = model.AmEx;
                APIModel.PersonalChecks = model.PersonalChecks;
                APIModel.IsPaymentApps = model.IsPaymentApps;
                APIModel.WhichPaymentApps = model.WhichPaymentApps;

                //COMPANY PROFILE PICTURE
                if (model.ProfilePicture != null)
                {
                    bool IsDeleted = cm.DeleteFile(model.ExistingProfilePicture);
                    if (IsDeleted)
                    {
                        APIModel.ProfilePicture = cm.SaveFile(model.ProfilePicture);
                    }

                }
                else
                {
                    APIModel.ProfilePicture = model.ExistingProfilePicture;
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/BusinessProfile/");
                    var httpResponse = await client.PostAsJsonAsync<BusinessProfileSubContractor>("UpdateBusinessProfileOfOtherContractors", APIModel);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //HttpContext.Session.Remove("CurrentUser");
                        //var currentuser = JsonConvert.DeserializeObject<Response>(content);
                        //User user = new User();
                        //user = JsonConvert.DeserializeObject<User>(currentuser.Data.ToString());
                        //HttpContext.Session.Set<User>("CurrentUser", user);
                        //// return Json(content);
                        return RedirectToAction("businessprofiledetail", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("businessprofiledetail", "Dashboard");
                    }

                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }


        }



        //on click from dashboard 'view detl' 
        [HttpGet]
        public async Task<IActionResult> GetContractorDetails(string edit)
        {
            ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();

            try
            {

                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Id", edit)
                    });
                    var httpResponse = await client.PostAsync("GetContractorProfileDetails", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<ContractorProfileDetailsViewModel>(content);

                        model = deserializedResultList;
                        HttpContext.Session.Remove("ContractorProfileDetails");
                        HttpContext.Session.Set<ContractorProfileDetailsViewModel>("ContractorProfileDetails", deserializedResultList);
                        return Json(1);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


            return View(model);
        }

        public IActionResult ContractorDetails()
        {
            try
            {
                ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();
                model = HttpContext.Session.Get<ContractorProfileDetailsViewModel>("ContractorProfileDetails");
                model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public IActionResult contractorsdetails()
        {
            try
            {
                ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();
                model = HttpContext.Session.Get<ContractorProfileDetailsViewModel>("ContractorProfileDetails");
                model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                return View("contractorsdetails", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> ContractorsReviewAsync([FromBody] ContractorsReviewViewModel model)
        {
            using (var client = new HttpClient())
            {
                var accessToken = HttpContext.Request.Cookies["accessToken"];

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                var httpResponse = await client.PostAsJsonAsync<ContractorsReviewViewModel>("ContractorsReview", model);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    //HttpContext.Session.Remove("CurrentUser");
                    //var currentuser = JsonConvert.DeserializeObject<Response>(content);
                    //User user = new User();
                    //user = JsonConvert.DeserializeObject<User>(currentuser.Data.ToString());
                    //HttpContext.Session.Set<User>("CurrentUser", user);
                    //// return Json(content);
                    return Json(1);
                }
                else
                {
                    return Json(2);
                }

            }


        }
        public IActionResult ContractorList()
        {

            return View();
        }


        //payment page  should be on layout.cshtml
        public IActionResult payment()
        {

            return View();
        }




        public IActionResult pdfviewer()
        {

            return View();
        }




        public IActionResult selectzipcode()
        {

            return View();
        }

        [HttpGet]
        public async Task<List<State>> StateList()
        {
            List<State> states = new List<State>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetStates");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<State>>(content);


                        return deserializedResultList;
                    }
                    else
                    {
                        states.Add(new State
                        {

                            StateName = "No states found",
                            StateId = 0

                        });
                        return states;
                    }
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<List<Categories>> CategoriesList(string Prefix, string zipcode)
        {
            List<Categories> CategoriesList = new List<Categories>();

            try
            {
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Prefix", Prefix),
                        new KeyValuePair<string, string>("zipcode", zipcode)
                    });
                    //HTTP POST
                    var httpResponse = await client.PostAsync("GetCategoriesList", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<Categories>>(content);


                        return deserializedResultList;
                    }
                    else
                    {
                        CategoriesList.Add(new Categories
                        {
                            Id = 0,
                            Name = "No Categories found",


                        });
                        return CategoriesList;
                    }
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<List<ZipCode>> ZipcodeList(string Prefix, string UserID)
        {
            List<ZipCode> ZipCodeList = new List<ZipCode>();

            try
            {
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Dashboard/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Prefix", Prefix),
                        new KeyValuePair<string, string>("UserID", UserID)
                    });
                    var httpResponse = await client.PostAsync("GetZipCodeList", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<ZipCode>>(content);


                        return deserializedResultList;
                    }
                    else
                    {

                        ZipCodeList.Add(new ZipCode
                        {
                            Id = 0,
                            Zipcode = "00000",


                        });
                        return ZipCodeList;
                    }
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public IActionResult Resetpassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();

            model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
            return View(model);
        }
        public async Task<IActionResult> ViewMembershipPlan()
        {

            MembershipAndSubscriptionPlanViewModel model = new MembershipAndSubscriptionPlanViewModel();
            try
            {
                var _User = HttpContext.Session.Get<User>("CurrentUser");


                model.LoggedInUser = _User;
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("UserId", _User.Id.ToString())
                    });
                    var httpResponse = await client.PostAsync("GetSubscriptionDetailByUserId", ContentsToSend);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        model.SubscriptionsStripeData = JsonConvert.DeserializeObject<UserMembershipSubscriptions>(content);
                    }


                }



                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("UserId", _User.Id.ToString())
                    });

                    var customMemberResponse = await client.PostAsync("GetCustomPlanDetailsByUserId", ContentsToSend);
                    var customMemberResponsecontent = await customMemberResponse.Content.ReadAsStringAsync();
                    if (customMemberResponse.IsSuccessStatusCode)
                    {
                        model.CustomPlan = JsonConvert.DeserializeObject<CustomZipcodesRequest>(customMemberResponsecontent);
                    }


                }
                //getting custom zipcode data

                if (model.SubscriptionsStripeData == null)
                {
                    return Json("No data found.");
                }
                //Fetching  MembershipPlans
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetMembershipPlans");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var res = JsonConvert.DeserializeObject<List<MembershipPlanViewmodel>>(content);
                        model.Plans = res;
                        foreach (var item in model.Plans)
                        {
                            if (item.Title == "Bronze" && item.RoleId == Constant.Contractor)
                            {//price_1LL6DNKoRZM2RthNZvGLTqEx
                                model.PriceMonthlyBronzeID = item.MonthlyStripePriceId;
                                model.PriceYearlyBronzeID = item.AnnuallyStripePriceId;
                            }
                            else if (item.Title == "Silver" && item.RoleId == Constant.Contractor)
                            {
                                model.PriceMonthlySilverID = item.MonthlyStripePriceId;
                                model.PriceYearlySilverID = item.AnnuallyStripePriceId;
                            }
                            else if (item.Title == "Gold" && item.RoleId == Constant.Contractor)
                            {
                                model.PriceMonthlyGoldID = item.MonthlyStripePriceId;
                                model.PriceYearlyGoldID = item.AnnuallyStripePriceId;
                            }


                        }
                    }
                }

                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }


        public async Task<IActionResult> ViewMembershipPlanHomeOwnerAsync()
        {
            MembershipAndSubscriptionPlanViewModel model = new MembershipAndSubscriptionPlanViewModel();
            try
            {
                var _User = HttpContext.Session.Get<User>("CurrentUser");
                //checking if current user already have subscription plan or not

                model.LoggedInUser = _User;
                using (var client = new HttpClient())
                {
                    var accessToken = HttpContext.Request.Cookies["accessToken"];
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("UserId", _User.Id.ToString())
                    });
                    var httpResponse = await client.PostAsync("GetSubscriptionDetailByUserId", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        model.SubscriptionsStripeData = JsonConvert.DeserializeObject<UserMembershipSubscriptions>(content);



                    }

                }
                if (model.SubscriptionsStripeData == null)
                {
                    return Json("No data found.");
                }
                //Fetching  MembershipPlans
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetMembershipPlans");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var res = JsonConvert.DeserializeObject<List<MembershipPlanViewmodel>>(content);
                        model.Plans = res;
                        foreach (var item in model.Plans)
                        {
                            if (item.Title == "Gold" && item.RoleId == Constant.HomeOwner)
                            {
                                model.PriceMonthlyGoldID = item.MonthlyStripePriceId;
                                model.PriceYearlyGoldID = item.AnnuallyStripePriceId;
                            }
                            else if (item.Title == "Silver" && item.RoleId == Constant.HomeOwner)
                            {
                                model.PriceMonthlySilverID = item.MonthlyStripePriceId;
                                model.PriceYearlySilverID = item.AnnuallyStripePriceId;
                            }

                        }
                    }
                }

                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }


        public async Task<IActionResult> UpdateZipcodeAndCategories()
        {


            ZipcodesAndCategoriesViewModel model = new ZipcodesAndCategoriesViewModel();

            model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var ContentsToSend = new FormUrlEncodedContent(new[]
                  {
                        new KeyValuePair<string, string>("Email",model.LoggedInUser.Email)
                        });
                //HTTP POST


                var httpResponse = await client.PostAsync("GetZipcodesStatesAndCategories", ContentsToSend);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {

                    model = JsonConvert.DeserializeObject<ZipcodesAndCategoriesViewModel>(content);
                    model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                }

            }
            return View(model);
        }

    }
}
