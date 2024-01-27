using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_UtilityLayer.Session;
using KopkeHome_WebApp.WebUtility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KopkeHome_WebApp.Controllers
{
#nullable disable
    public class BusinessProfileController : Controller

    {
        private IWebHostEnvironment Environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BusinessProfileController> _logger;
        public BusinessProfileController(IWebHostEnvironment _environment, ILogger<BusinessProfileController> logger, IConfiguration iConfig)
        {
            Environment = _environment;
            _configuration = iConfig;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult BusinessProfile()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusinessProfileSave(BusinessProfileViewModel VM)
        {
            try
            {
                int userid = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                    });
                    var httpResponse = await client.PostAsync("GetUserByEmail", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);
                        HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                        userid = deserializedResultList.Id;

                    }
                }
                Common common = new Common(Environment);
                //if (ModelState.IsValid)
                //{

                BusinessProfileDataModel ContractorData = new BusinessProfileDataModel()
                {
                    UserId = userid,
                    BusinessDescription = VM.BusinessDescription,
                    AmEx = VM.AmEx,
                    Is24HoursPhoneAnswering = VM.Is24HoursPhoneAnswering,
                    Visa = VM.Visa,
                    BusinessOrTradeLicenseFiles = common.MultiplFiles(VM.BusinessOrTradeLicenseFiles),
                    ProfilePicture = common.SaveFile(VM.ProfilePicture),
                    LiabilityInsuranceFile = common.SaveFile(VM.LiabilityInsuranceFile),
                    WorkmanCompensationInsuranceFile = common.SaveFile(VM.WorkmanCompensationInsuranceFile),
                    CommercialLocation = VM.CommercialLocation,
                    DesignServices = VM.DesignServices,
                    CompanyWebsiteURL = common.CheckHttp(VM.CompanyWebsiteURL),
                    IsContactedBySubcontractors = VM.IsContactedBySubcontractors,
                    EstimateCharge = VM.EstimateCharge,
                    IsOfferEmergencyServices = VM.IsOfferEmergencyServices,
                    IsPhoneCallSupport = VM.IsPhoneCallSupport,
                    JobSiteCrews = VM.JobSiteCrews,
                    FacebookPageURL = common.CheckHttp(VM.FacebookPageURL),
                    IsBusinessOrTradeLicense = VM.IsBusinessOrTradeLicense,
                    IsFacebookPage = VM.IsFacebookPage,
                    IsCash = VM.IsCash,
                    IsCompanyWebsite = VM.IsCompanyWebsite,
                    IsContactedByHomeowners = VM.IsContactedByHomeowners,
                    IsLiabilityInsurance = VM.IsLiabilityInsurance,
                    IsEstimateCharge = VM.IsEstimateCharge,
                    IsDesignServices = VM.IsDesignServices,
                    IsWorkmanCompensationInsurance = VM.IsWorkmanCompensationInsurance,
                    MC = VM.MC,
                    NormalBusinessHours = VM.NormalBusinessHours,
                    NumberOfEmployees = VM.NumberOfEmployees,
                    OtherCreditCard = VM.OtherCreditCard,
                    YearsInBusiness = VM.YearsInBusiness,
                    WhichPaymentApps = VM.WhichPaymentApps,
                    PersonalChecks = VM.PersonalChecks,
                    IsPaymentApps = VM.IsPaymentApps,
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/BusinessProfile/");
                    var httpResponse = await client.PostAsJsonAsync<BusinessProfileDataModel>("BusinessProfileForContractor", ContractorData);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ContractorsPlan", "Membership");
                    }
                    else
                    {
                        return View("ContractorProfile");
                    }
                }

                //}

                // return View("ContractorProfile");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubContractorProfile(SubContractorBusinessProfileViewModel VM)
        {
            try
            {
                int userid = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                    });

                    var httpResponse = await client.PostAsync("GetUserByEmail", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);

                        userid = deserializedResultList.Id;


                    }

                }

                Common common = new Common(Environment);
                //if (ModelState.IsValid)
                //{

                BusinessProfileSubContractor model = new BusinessProfileSubContractor()
                {
                    UserId = userid,
                    BusinessDescription = VM.BusinessDescription,
                    AmEx = VM.AmEx,
                    Is24HoursPhoneAnswering = VM.Is24HoursPhoneAnswering,

                    Visa = VM.Visa,
                    BusinessOrTradeLicenseFiles = common.MultiplFiles(VM.BusinessOrTradeLicenseFiles),
                    ProfilePicture = common.SaveFile(VM.ProfilePicture),
                    LiabilityInsuranceFile = common.SaveFile(VM.LiabilityInsuranceFile),
                    WorkmanCompensationInsuranceFile = common.SaveFile(VM.WorkmanCompensationInsuranceFile),
                    CommercialLocationContractor = VM.CommercialLocation,//putting commerical location valu into comerical location contractor field
                    ServiceCallCharge = VM.ServiceCallCharge,
                    DesignServices = VM.DesignServices,
                    CompanyWebsiteURL = common.CheckHttp(VM.CompanyWebsiteURL),
                    //IsContactedBySubcontractors = VM.IsContactedBySubcontractors,
                    EstimateCharge = VM.EstimateCharge,
                    IsOfferEmergencyServices = VM.IsOfferEmergencyServices,
                    IsPhoneCallSupport = VM.IsPhoneCallSupport,
                    JobSiteCrews = VM.JobSiteCrews,
                    FacebookPageURL = common.CheckHttp(VM.FacebookPageURL),
                    IsBusinessOrTradeLicense = VM.IsBusinessOrTradeLicense,
                    IsFacebookPage = VM.IsFacebookPage,
                    IsCash = VM.IsCash,
                    IsCompanyWebsite = VM.IsCompanyWebsite,
                    IsContactedByContractors = VM.IsContactedByContractors,
                    IsContactedByHomeowners = VM.IsContactedByHomeowners,
                    IsLiabilityInsurance = VM.IsLiabilityInsurance,
                    IsEstimateCharge = VM.IsEstimateCharge,
                    IsDesignServices = VM.IsDesignServices,
                    IsWorkmanCompensationInsurance = VM.IsWorkmanCompensationInsurance,
                    MC = VM.MC,
                    NormalBusinessHours = VM.NormalBusinessHours,
                    NumberOfEmployees = VM.NumberOfEmployees,
                    OtherCreditCard = VM.OtherCreditCard,
                    YearsInBusiness = VM.YearsInBusiness,
                    WhichPaymentApps = VM.WhichPaymentApps,
                    PersonalChecks = VM.PersonalChecks,
                    IsPaymentApps = VM.IsPaymentApps,

                };


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/BusinessProfile/");
                    var httpResponse = await client.PostAsJsonAsync<BusinessProfileSubContractor>("BusinessProfileSubContractor", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ContractorsPlan", "Membership");
                    }
                    else
                    {
                        return View("OtherContractorsProfile");
                    }
                }

                //}



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> ContractorProfile()
        {
            BusinessProfileViewModel model = new BusinessProfileViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                var httpResponse = await client.PostAsync("GetUserByEmail", content);
                string resultContent = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);

                    if (deserializedResultList == null)
                    {
                        return RedirectToAction("SignIn", "user");
                    }
                    HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                    model.LoggedInUser = deserializedResultList;
                }
            }
            return View(model);
        }
        public async Task<IActionResult> SubContractorProfileAsync()
        {
            SubContractorBusinessProfileViewModel model = new SubContractorBusinessProfileViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                var httpResponse = await client.PostAsync("GetUserByEmail", content);
                string resultContent = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);

                    if (deserializedResultList == null)
                    {
                        return RedirectToAction("SignIn", "user");
                    }
                    HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                    model.LoggedInUser = deserializedResultList;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> OtherContractorsProfile()
        {
            SubContractorBusinessProfileViewModel model = new SubContractorBusinessProfileViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                var httpResponse = await client.PostAsync("GetUserByEmail", content);
                string resultContent = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);

                    if (deserializedResultList == null)
                    {
                        return RedirectToAction("SignIn", "user");
                    }
                    HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                    model.LoggedInUser = deserializedResultList;
                }
            }
            return View(model);
        }
        public IActionResult IndiContractorProfile()
        {
            return View();
        }

        public IActionResult HomeOwnerMembershipPlan()
        {
            return View();
        }
        public IActionResult HomeOwner()
        {
            return View();
        }
        public async Task<User> GetUserByEmail(string Email)
        {
            User user = new User();
            if (string.IsNullOrEmpty(Email))
            {
                return user;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", Email)
                });

                var httpResponse = await client.PostAsync("GetUserByEmail", content);
                string resultContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    user = JsonConvert.DeserializeObject<User>(resultContent);
                }

            }
            return user;
        }
    }
}
